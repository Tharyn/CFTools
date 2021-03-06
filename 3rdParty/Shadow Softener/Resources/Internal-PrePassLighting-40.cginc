#include "UnityCG.cginc"
struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f {
	float4 pos : SV_POSITION;
	float4 uv : TEXCOORD0;
	float3 ray : TEXCOORD1;
};

// If this param is changed, it must be Unity 4.3+
float _LightAsQuad = -1000;

// VERTEX SHADER
v2f vert (appdata v)
{
	v2f o;
	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	o.uv = ComputeScreenPos (o.pos);
	o.ray = mul (UNITY_MATRIX_MV, v.vertex).xyz * float3(-1,-1,1);
	
	// Old Technique:
	// v.texcoord is equal to 0 when we are drawing 3D light shapes and
	// contains a ray pointing from the camera to one of near plane's
	// corners in camera space when we are drawing a full screen quad.
	//o.ray = lerp(o.ray, v.normal, v.normal.z != 0);
	
	// New Technique:
	// v.normal contains a ray pointing from the camera to one of near plane's
	// corners in camera space when we are drawing a full screen quad.
	// Otherwise, when rendering 3D shapes, use the ray calculated here.
	//o.ray = lerp(o.ray, v.normal, _LightAsQuad);
	
	// Combined Techniques:
	o.ray = lerp(o.ray, v.normal, (_LightAsQuad > -999) ? _LightAsQuad : (v.normal.z != 0));
	
	return o;
}

sampler2D _CameraNormalsTexture;
sampler2D _CameraDepthTexture;
sampler2D _GGXlut;
float4 _LightDir;
float4 _LightPos;
float4 _LightColor;
float4 unity_LightmapFade;
CBUFFER_START(UnityPerCamera2)
float4x4 _CameraToWorld;
CBUFFER_END
float4x4 _LightMatrix0;
sampler2D _LightTextureB0;


#if defined (POINT_COOKIE)
samplerCUBE _LightTexture0;
#else
sampler2D _LightTexture0;
#endif


#if defined (SHADOWS_DEPTH) //SHADOWS_DEPTH
	#if defined (SPOT) //SPOT
		UNITY_DECLARE_SHADOWMAP(_ShadowMapTexture);
		#if defined (SHADOWS_SOFT) //SHADOWS_SOFT
				uniform float4 _ShadowOffsets[4];
		#endif //SHADOWS_SOFT

		#define SOFTENER_PREPASS
		#define SOFTENER_SPOT

		#include "ShadowSoftenerConfig.cginc"
		#include "ShadowSoftener.cginc"
		#define unitySampleShadow(coord) (_LightShadowData.r + SOFTENER_CUSTOM_FILTER((coord).xyz/(coord).w) * (1-_LightShadowData.r))

	#endif //SPOT
#endif //SHADOWS_DEPTH


#if defined (SHADOWS_CUBE)
	#if defined (POINT) || defined (POINT_COOKIE)
		samplerCUBE _ShadowMapTexture;

		inline float SampleCubeDistance (float3 vec)
		{
			float4 packDist = texCUBE (_ShadowMapTexture, vec);
			return DecodeFloatRGBA( packDist );
		}

		#define SOFTENER_PREPASS
		#define SOFTENER_POINT
		#include "ShadowSoftenerConfig.cginc"
		#include "ShadowSoftener.cginc"
		#define unitySampleShadow(vec, mydist) (_LightShadowData.r + SOFTENER_CUSTOM_FILTER(float4(vec, mydist)) * (1 - _LightShadowData.r))
	#endif //POINT || POINT_COOKIE
#endif //SHADOWS_CUBE


#if defined (SHADOWS_SCREEN)
	sampler2D _ShadowMapTexture;
#endif

float ComputeFadeDistance(float3 wpos, float z)
{
	float sphereDist = distance(wpos, unity_ShadowFadeCenterAndType.xyz);
	return lerp(z, sphereDist, unity_ShadowFadeCenterAndType.w);
}

// COMPUTE SHADOWS
half ComputeShadow(float3 vec, float fadeDist, float2 uv)
{
	// (SHADOWS_DEPTH) || defined(SHADOWS_SCREEN) || defined(SHADOWS_CUBE)
	#if defined(SHADOWS_DEPTH) || defined(SHADOWS_SCREEN) || defined(SHADOWS_CUBE)
		float fade = fadeDist * _LightShadowData.z + _LightShadowData.w;
		fade = saturate(fade);
	#endif
	
	// SPOT
	#if defined(SPOT)
		//SHADOWS_DEPTH
		#if defined(SHADOWS_DEPTH) 
			float4 shadowCoord = mul (unity_World2Shadow[0], float4(vec,1));
			return saturate(unitySampleShadow (shadowCoord) + fade);
		#endif
	#endif

	// DIRECTIONAL || DIRECTIONAL_COOKIE
	#if defined (DIRECTIONAL) || defined (DIRECTIONAL_COOKIE)
		#if defined(SHADOWS_SCREEN)
			return saturate(tex2D (_ShadowMapTexture, uv).r + fade);
		#endif
	#endif 
	
	// (POINT) || defined (POINT_COOKIE)
	#if defined (POINT) || defined (POINT_COOKIE)
		//SHADOWS_CUBE
		#if defined(SHADOWS_CUBE)
			float mydist = length(vec) * _LightPositionRange.w;
			return unitySampleShadow (vec, mydist);	
		#endif 
	#endif
	
	return 1.0;
}

/// FRAGMENT SHADER CALCULATE LIGHT
half4 CalculateLight (v2f i)
{
	i.ray = i.ray * (_ProjectionParams.z / i.ray.z);
	float2 uv = i.uv.xy / i.uv.w;
	
	half4 nspec = tex2D (_CameraNormalsTexture, uv);
	half3 normal = nspec.rgb * 2 - 1;
	normal = normalize(normal);
	
	float depth = UNITY_SAMPLE_DEPTH(tex2D (_CameraDepthTexture, uv));
	depth = Linear01Depth (depth);
	float4 vpos = float4(i.ray * depth,1);
	float3 wpos = mul (_CameraToWorld, vpos).xyz;

	float fadeDist = ComputeFadeDistance(wpos, vpos.z);
	
	//SPOT
	#if defined (SPOT)	
		float3 tolight = _LightPos.xyz - wpos;
		half3 lightDir = normalize (tolight);
	
		float4 uvCookie = mul (_LightMatrix0, float4(wpos,1));
		float atten = tex2Dproj (_LightTexture0, UNITY_PROJ_COORD(uvCookie)).w;

		atten *= uvCookie.w < 0;
		float att = dot(tolight, tolight) * _LightPos.w;

		atten *= tex2D (_LightTextureB0, att.rr).UNITY_ATTEN_CHANNEL;
	
		atten *= ComputeShadow (wpos, fadeDist, uv);
	#endif 
	
	
	 // DIRECTIONAL || DIRECTIONAL_COOKIE
	#if defined (DIRECTIONAL) || defined (DIRECTIONAL_COOKIE)
		half3 lightDir = -_LightDir.xyz;
		float atten = 1.0;
	
		atten *= ComputeShadow (wpos, fadeDist, uv);
	
		// DIRECTIONAL_COOKIE
		#if defined (DIRECTIONAL_COOKIE)
			atten *= tex2D (_LightTexture0, mul(_LightMatrix0, half4(wpos,1)).xy).w;
		#endif
	#endif 
	
	
	 //POINT || POINT_COOKIE
	#if defined (POINT) || defined (POINT_COOKIE)
		float3 tolight = wpos - _LightPos.xyz;
		half3 lightDir = -normalize (tolight);
	
		// CHANGE FOR POINT FALLOFF
		float att = dot(tolight, tolight) * _LightPos.w;
		att *= att;
		//att *= att;

		float atten = tex2D (_LightTextureB0, att.rr).UNITY_ATTEN_CHANNEL;
	
		atten *= ComputeShadow (tolight, fadeDist, uv);
	
		//POINT_COOKIE
		#if defined (POINT_COOKIE)
			atten *= texCUBE(_LightTexture0, mul(_LightMatrix0, half4(wpos,1)).xyz).w;
		#endif
	#endif
	

	 
	half3 h = normalize (lightDir - normalize(wpos-_WorldSpaceCameraPos));
	
	half dotNH = saturate(dot(normal,h));

	half dotNL = saturate( dot (lightDir, normal));

	// This msx fuinction maxes sure that the roughness can not get below .1
	// This prevents complete loss of highlight
	float fixRough = max((1-nspec.a), .08);

	float alpha = fixRough*fixRough;
	float dotLH = saturate(dot(lightDir,h));
	half diff = max (0, dotNL);	



	// D
	float F0 = .5;
	float alphaSqr = alpha * alpha;
	float pi = 3.14159;
	float denom = dotNH * dotNH *(alphaSqr - 1.0) + 1.0;
	float D = alphaSqr/(pi * denom * denom);
	
	/*
	float F0 = .05;
	float2 uvD = float2(dotNH, 1-(nspec.a));
	float4 D_helper = (tex2D (_GGXlut, uvD));
	float D = D_helper.r *.01;
	*/
	
	// F
	float dotLH5 = pow(1.0-dotLH, 5);
	float F = F0 + (1.0- F0) * (dotLH5);

	// V
	float k = alpha / 2.0;
	float k2 = k*k;
	float invK2 = 1.0 - k2;
	float vis = rcp(dotLH * dotLH * invK2 + k2);

	// The max spec value prevent the intensity from exceeding what a single pixel cna handle
	float maxSpec = 5;



	// GGX
	float spec = min(dotNL * D * F * vis, maxSpec);
	spec *= saturate(atten);


	 
	/* LOOK UP TABE FOR GGX
	float2 uvD = half2(dotNH, 1-(nspec.a));
	float4 D_helper = (tex2D (_GGXlut, uvD));
	float D;

	float2 uvFV = half2(dotLH, 1-(nspec.a));
	float4 FV_helper = (tex2D (_GGXlut, uvFV)).gb;

	float FV = 1*FV_helper.x + (1.0f-1)*FV_helper.y;
	float spec =  dotNL * D * FV;
	*/

	// Blinn-Phong
	//float spec = pow (max (0, dotNH), nspec.a*128);
	//spec *= saturate(atten);



	half4 res;
	res.xyz = _LightColor.rgb * (diff * atten);
	res.w = spec * Luminance (_LightColor.rgb);
	
	float fade = fadeDist * unity_LightmapFade.z + unity_LightmapFade.w;
	res *= saturate(1.0-fade);
	
	return res;
}