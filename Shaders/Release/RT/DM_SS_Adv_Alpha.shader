// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3865,x:33721,y:32835,varname:node_3865,prsc:2|diff-7884-RGB,spec-3428-RGB,gloss-3428-A,normal-2290-RGB,amdfl-8714-OUT,amspl-3238-OUT,alpha-7884-A;n:type:ShaderForge.SFN_Tex2d,id:2290,x:32604,y:33305,ptovrint:False,ptlb:BumpMap,ptin:_BumpMap,varname:node_2290,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_SkyshopSpec,id:5481,x:32604,y:33075,varname:node_5481,prsc:2,sprot:True,spblend:True,splmocc:False|GLOSS-3428-A;n:type:ShaderForge.SFN_Tex2d,id:7884,x:32600,y:31860,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_7884,prsc:2,tex:5ee4f7559442ca54e87648c5067fd7b3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_SkyshopDiff,id:6106,x:32604,y:32858,varname:node_6106,prsc:2,dfrot:True,dfblend:True,dflmocc:False;n:type:ShaderForge.SFN_Slider,id:6201,x:32763,y:32997,ptovrint:False,ptlb:IBLAmt,ptin:_IBLAmt,varname:node_6201,prsc:2,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:8714,x:33004,y:32827,varname:node_8714,prsc:2|A-6106-RGB,B-6201-OUT;n:type:ShaderForge.SFN_Slider,id:4833,x:32763,y:33188,ptovrint:False,ptlb:ReflectAmt,ptin:_ReflectAmt,varname:node_4833,prsc:2,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:3238,x:33106,y:33056,varname:node_3238,prsc:2|A-5481-RGB,B-4833-OUT;n:type:ShaderForge.SFN_Color,id:3428,x:32601,y:32433,ptovrint:False,ptlb:Spec,ptin:_Spec,varname:node_3428,prsc:2,glob:False,c1:0.1691176,c2:0.1691176,c3:0.1691176,c4:0.09411765;n:type:ShaderForge.SFN_Color,id:738,x:32922,y:33106,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:node_7239,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:7884-4833-2290-6201-3428;pass:END;sub:END;*/

Shader "DM2/DM_SS_Adv_Alpha" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _ReflectAmt ("ReflectAmt", Range(0, 1)) = 0.5
        _BumpMap ("BumpMap", 2D) = "bump" {}
        _IBLAmt ("IBLAmt", Range(0, 1)) = 0.5
        _Spec ("Spec", Color) = (0.1691176,0.1691176,0.1691176,0.09411765)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile MARMO_SKY_BLEND_OFF MARMO_SKY_BLEND_ON
            #pragma multi_compile MARMO_BOX_PROJECTION_OFF MARMO_BOX_PROJECTION_ON
            #pragma exclude_renderers gles xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _LightColor0;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            #ifndef MARMO_EXPOSURE_IBL_DEFINED
            #define MARMO_EXPOSURE_IBL_DEFINED
            uniform float  _BlendWeightIBL;
            uniform float4 _ExposureIBL;
            uniform float4 _ExposureLM;
            uniform float4 _UniformOcclusion;
            uniform float4 _ExposureIBL1;
            uniform float4 _ExposureLM1;
            inline float4 marmoExposureBlended() {
            	float4 exposure = _ExposureIBL;
            	#if !LIGHTMAP_OFF
            		exposure.xy *= _ExposureLM.xy;
            	#endif
            	#if MARMO_SKY_BLEND_ON
            		float4 exposure1 = _ExposureIBL1;
            		#if !LIGHTMAP_OFF
            			exposure1.xy *= _ExposureLM1.xy;
            		#endif
            		exposure = lerp(exposure1, exposure, _BlendWeightIBL);
            	#endif
            	exposure.xy *= _UniformOcclusion.xy;
            	return exposure;
            }
            #endif
            
            #ifndef MARMO_SKY_MATRIX_DEFINED
            #define MARMO_SKY_MATRIX_DEFINED
            uniform float4x4 _SkyMatrix;
            uniform float4x4 _InvSkyMatrix;
            uniform float3   _SkySize;
            uniform float3   _SkyMin;
            uniform float3   _SkyMax;
            uniform float4x4 _SkyMatrix1;
            uniform float4x4 _InvSkyMatrix1;
            uniform float3   _SkySize1;
            uniform float3   _SkyMin1;
            uniform float3   _SkyMax1;
            inline float3 mulVec3(uniform float4x4 m, float3 v ) { return float3(dot(m[0].xyz,v.xyz), dot(m[1].xyz,v.xyz), dot(m[2].xyz,v.xyz)); }
            inline float3 transposeMulVec3(uniform float4x4 m, float3 v )   { return m[0].xyz*v.x + (m[1].xyz*v.y + (m[2].xyz*v.z)); }
            inline float3 transposeMulVec3(uniform float3x3 m, float3 v )   { return m[0].xyz*v.x + (m[1].xyz*v.y + (m[2].xyz*v.z)); }
            inline float3 transposeMulPoint3(uniform float4x4 m, float3 p ) { return m[0].xyz*p.x + (m[1].xyz*p.y + (m[2].xyz*p.z + m[3].xyz)); }
            inline float3 marmoSkyRotate (uniform float4x4 skyMatrix, float3 R) { return transposeMulVec3(skyMatrix, R); }
            #endif
            
            #ifndef MARMO_RGBM_DEFINED
            #define MARMO_RGBM_DEFINED
            #define IS_LINEAR ((-3.22581*unity_ColorSpaceGrey.r) + 1.6129)
            #define IS_GAMMA  (( 3.22581*unity_ColorSpaceGrey.r) - 0.6129)
            inline half  toLinearFast1(half c) { half c2=c*c; return dot(half2(0.7532,0.2468), half2(c2,c*c2)); }
            inline half3 fromRGBM(half4 c) { c.a*=6.0; return c.rgb*lerp(c.a, toLinearFast1(c.a), IS_LINEAR); }
            #endif
            
            #ifndef MARMO_SPECULAR_DEFINED
            #define MARMO_SPECULAR_DEFINED
            uniform samplerCUBE _SpecCubeIBL;
            uniform samplerCUBE _SpecCubeIBL1;
            float3 marmoSkyProject(uniform float4x4 skyMatrix, uniform float4x4 invSkyMatrix, uniform float3 skyMin, uniform float3 skyMax, uniform float3 worldPos, float3 R) {
            	#if MARMO_BOX_PROJECTION_ON
            		R = marmoSkyRotate(skyMatrix, R);
            		float3 invR = 1.0/R;
            		float4 P;
            		P.xyz = worldPos;
            		P.w=1.0; P.xyz = mul(invSkyMatrix,P).xyz;
            		float4 rbmax = float4(0.0,0.0,0.0,0.0);
            		float4 rbmin = float4(0.0,0.0,0.0,0.0);
            		rbmax.xyz = skyMax - P.xyz;
            		rbmin.xyz = skyMin - P.xyz;
            		float3 rbminmax = invR * lerp(rbmin.xyz, rbmax.xyz, saturate(R*1000000.0));
            		float fa = min(min(rbminmax.x, rbminmax.y), rbminmax.z);
            		return P.xyz + R*fa;
            	#else
            		R = marmoSkyRotate(skyMatrix, R);
            		return R;
            	#endif
            }
            float3 marmoSpecular(float3 dir) {
            	float4 exposure = marmoExposureBlended();
            	float3 R;
            	R = marmoSkyRotate(_SkyMatrix, dir);
            	float3 specIBL = fromRGBM(texCUBE(_SpecCubeIBL, R));
            	#if MARMO_SKY_BLEND_ON
            		R = marmoSkyRotate(_SkyMatrix1, dir);
            		float3 specIBL1 = fromRGBM(texCUBE(_SpecCubeIBL1, R));
            		specIBL = lerp(specIBL1, specIBL, _BlendWeightIBL);
            	#endif
            	return specIBL * (exposure.w * exposure.y);
            }
            
            float3 marmoMipSpecular(float3 dir, float3 worldPos, float gloss) {
            	float4 exposure = marmoExposureBlended();
            	float4 lookup;
            	lookup.xyz = marmoSkyProject(_SkyMatrix, _InvSkyMatrix, _SkyMin, _SkyMax, worldPos, dir);
            	lookup.w = (-6.0*gloss) + 6.0;
            	float3 specIBL = fromRGBM(texCUBElod(_SpecCubeIBL, lookup));
            	#if MARMO_SKY_BLEND_ON
            		lookup.xyz = marmoSkyProject(_SkyMatrix1, _InvSkyMatrix1, _SkyMin1, _SkyMax1, worldPos, dir);
            		float3 specIBL1 = fromRGBM(texCUBElod(_SpecCubeIBL1, lookup));
            		specIBL = lerp(specIBL1, specIBL, _BlendWeightIBL);
            	#endif
            	return specIBL * (exposure.w * exposure.y);
            }
            #endif
            
            
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            #ifndef MARMO_EXPOSURE_IBL_DEFINED
            #define MARMO_EXPOSURE_IBL_DEFINED
            uniform float  _BlendWeightIBL;
            uniform float4 _ExposureIBL;
            uniform float4 _ExposureLM;
            uniform float4 _UniformOcclusion;
            uniform float4 _ExposureIBL1;
            uniform float4 _ExposureLM1;
            inline float4 marmoExposureBlended() {
            	float4 exposure = _ExposureIBL;
            	#if !LIGHTMAP_OFF
            		exposure.xy *= _ExposureLM.xy;
            	#endif
            	#if MARMO_SKY_BLEND_ON
            		float4 exposure1 = _ExposureIBL1;
            		#if !LIGHTMAP_OFF
            			exposure1.xy *= _ExposureLM1.xy;
            		#endif
            		exposure = lerp(exposure1, exposure, _BlendWeightIBL);
            	#endif
            	exposure.xy *= _UniformOcclusion.xy;
            	return exposure;
            }
            #endif
            
            #ifndef MARMO_SKY_MATRIX_DEFINED
            #define MARMO_SKY_MATRIX_DEFINED
            uniform float4x4 _SkyMatrix;
            uniform float4x4 _InvSkyMatrix;
            uniform float3   _SkySize;
            uniform float3   _SkyMin;
            uniform float3   _SkyMax;
            uniform float4x4 _SkyMatrix1;
            uniform float4x4 _InvSkyMatrix1;
            uniform float3   _SkySize1;
            uniform float3   _SkyMin1;
            uniform float3   _SkyMax1;
            inline float3 mulVec3(uniform float4x4 m, float3 v ) { return float3(dot(m[0].xyz,v.xyz), dot(m[1].xyz,v.xyz), dot(m[2].xyz,v.xyz)); }
            inline float3 transposeMulVec3(uniform float4x4 m, float3 v )   { return m[0].xyz*v.x + (m[1].xyz*v.y + (m[2].xyz*v.z)); }
            inline float3 transposeMulVec3(uniform float3x3 m, float3 v )   { return m[0].xyz*v.x + (m[1].xyz*v.y + (m[2].xyz*v.z)); }
            inline float3 transposeMulPoint3(uniform float4x4 m, float3 p ) { return m[0].xyz*p.x + (m[1].xyz*p.y + (m[2].xyz*p.z + m[3].xyz)); }
            inline float3 marmoSkyRotate (uniform float4x4 skyMatrix, float3 R) { return transposeMulVec3(skyMatrix, R); }
            #endif
            
            #pragma glsl
            #ifndef MARMO_DIFFUSE_DEFINED
            #define MARMO_DIFFUSE_DEFINED
            uniform float3 _SH0;
            uniform float3 _SH1;
            uniform float3 _SH2;
            uniform float3 _SH3;
            uniform float3 _SH4;
            uniform float3 _SH5;
            uniform float3 _SH6;
            uniform float3 _SH7;
            uniform float3 _SH8;
            
            uniform float3 _SH01;
            uniform float3 _SH11;
            uniform float3 _SH21;
            uniform float3 _SH31;
            uniform float3 _SH41;
            uniform float3 _SH51;
            uniform float3 _SH61;
            uniform float3 _SH71;
            uniform float3 _SH81;
            
            float3 SHLookup(float3 dir) {
            	dir = marmoSkyRotate(_SkyMatrix, dir);
            	dir = normalize(dir);
            	float3 band0, band1, band2;
            	band0 = _SH0.xyz;
            
            	band1 =  _SH1.xyz * dir.y;
            	band1 += _SH2.xyz * dir.z;
            	band1 += _SH3.xyz * dir.x;
            
            	float3 swz = dir.yyz * dir.xzx;
            	band2 =  _SH4.xyz * swz.x;
            	band2 += _SH5.xyz * swz.y;
            	band2 += _SH7.xyz * swz.z;
            	float3 sqr = dir * dir;
            	band2 += _SH6.xyz * ( 3.0*sqr.z - 1.0 );
            	band2 += _SH8.xyz * ( sqr.x - sqr.y );
            	return band0 + band1 + band2;
            }
            float3 SHLookup1(float3 dir) {
            	dir = marmoSkyRotate(_SkyMatrix1, dir);
            	dir = normalize(dir);
            	float3 band0, band1, band2;
            	band0 = _SH01.xyz;
            
            	band1 =  _SH11.xyz * dir.y;
            	band1 += _SH21.xyz * dir.z;
            	band1 += _SH31.xyz * dir.x;
            
            	float3 swz = dir.yyz * dir.xzx;
            	band2 =  _SH41.xyz * swz.x;
            	band2 += _SH51.xyz * swz.y;
            	band2 += _SH71.xyz * swz.z;
            	//Commented coefficients because of internal Unity PropertyBlock issues.
            	//float3 sqr = dir * dir;
            	//band2 += _SH61.xyz * ( 3.0*sqr.z - 1.0 );
            	//band2 += _SH81.xyz * ( sqr.x - sqr.y );
            	return band0 + band1 + band2;
            }
            float3 marmoDiffuse(float3 dir) {
            	float4 exposure = marmoExposureBlended();
            	float3 diffIBL = SHLookup(dir);
            	#if MARMO_SKY_BLEND_ON
            		float3 diffIBL1 = SHLookup1(dir);
            		diffIBL = lerp(diffIBL1, diffIBL, _BlendWeightIBL);
            	#endif
            	return (exposure.x * exposure.w) * abs(diffIBL);
            }
            #endif
            
            
            uniform float _IBLAmt;
            uniform float _ReflectAmt;
            uniform float4 _Spec;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Spec.a;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _Spec.rgb;
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 indirectSpecular = (0 + (marmoMipSpecular(viewReflectDirection, i.posWorld.rgb, _Spec.a).rgb*_ReflectAmt));
                float3 specular = (directSpecular + indirectSpecular) * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb*2; // Ambient Light
                indirectDiffuse += (marmoDiffuse(normalDirection).rgb*_IBLAmt); // Diffuse Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _MainTex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers gles xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _LightColor0;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Spec;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Spec.a;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _Spec.rgb;
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = directDiffuse * _MainTex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * _MainTex_var.a,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
