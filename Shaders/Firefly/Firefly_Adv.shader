


Shader "Firefly/Firefly_Adv" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
		_SpecMap ("SpecMap", 2D) = "white" {}
		_BumpMap ("BumpMap", 2D) = "bump" {}

		_AoMap ("AoMap", 2D) = "white" {}
		_CurveMap ("CurveMap", 2D) = "white" {}

		// Used just for BEAST
        _SpecColor ("SpecColor (4LM)", Color) = (0.5,0.5,0.5,1)
        _Shininess ("Shininess (4LM)", Range(0, 1)) = 0.7

		// DEEP LIGHT PARAMETERS
		_RoomAmb ("RoomAmb", Color) = (1,1,1,1)
		_RefAmb ("ReflectionAmb", Color) = (0.0,0.0,0.0,1)

        _L1Pos ("L1Pos", Vector) = (0,0,0,0)
		_L2Pos ("L2Pos", Vector) = (0,0,0,0)

		_L1Intensity ("L1Intensity ", Float ) = 1
		_L1Color ("L1Color", Color) = (1,1,1,1)
		_L2Intensity ("L2Intensity ", Float ) = 1
		_L2Color ("L2Color", Color) = (1,1,1,1)


		
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
			// PRE-PASS BASE ->
            Name "PrePassBase"
            Tags {
                "LightMode"="PrePassBase"
            }
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_PREPASSBASE
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl

			#include "PrePassBase.cginc"

			#include "PrePassNormMapFrag.cginc"


            ENDCG
        } // PRE-PASS BASE <-



        Pass {
			// PRE-PASS FINAL ->
            Name "PrePassFinal"
            Tags {
                "LightMode"="PrePassFinal"
            }
            ZWrite Off
            
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#define UNITY_PASS_PREPASSFINAL
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#pragma multi_compile_prepassfinal
				#pragma multi_compile MARMO_SKY_BLEND_OFF MARMO_SKY_BLEND_ON
				#pragma multi_compile MARMO_BOX_PROJECTION_OFF MARMO_BOX_PROJECTION_ON
				#pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
				#pragma target 3.0
				#pragma glsl


				#include "PrepassFinalA.cginc"

				// SKYSHOP
				#include "SkyShopIBL.cginc"
				#include "SkyShopReflection.cginc"

				#include "PrepassFinalB.cginc"

				#include "PrepassFinalNorm.cginc"

				#include "PrepassFinalC.cginc"


				/////// SPECULAR:
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;

				// Full Fresnel 
				//float3 specularColor = (_SpecMap_var.rgb+(1.0-max(0,dot(normalDirection, viewDirection))));

                float specularMonochrome = dot(_SpecMap_var.rgb, float3(0.3,0.59,0.11));






				// I THINK THIS IS PHONG ?????
				//The alpha must be increased for the mirrors to produce a more heated reflection
                float specPow = max( 2, _SpecMap_var.a * 128 ) ;
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                //float3 directSpecular = (lightAccumulation.rgb * 2) * lightAccumulation.a * normTerm;
				float3 directSpecular =  lightAccumulation.a * normTerm;


				// THIS IS IMPORTANT
				// is controles the light ratio
				directSpecular *= .2;



				// Where we control the reflection by the light intensity
				float3 L1pow = _L1Color * (dot(L1Direction, viewReflectDirection) * _L1Intensity);
				float3 L2pow = _L2Color * (dot(L2Direction, viewReflectDirection) * _L2Intensity);


				

				/*LOOKUP TABLE
				//.03125 * band + .1
				float linBand = 1-(.03125 * 2 +.1);
				float L1dot =  dot(L1Direction, viewReflectDirection);
				float2 L1uv = float2(L1dot,linBand);
				float L1RemapLin = tex2D(_CurveMap,TRANSFORM_TEX(L1uv, _CurveMap)).r;
				*/


				// Reflection is multiplied by the lights/dir for face RT lighting in Ref
				//indirectSpecular *= L1pow + L2pow + _RoomAmb;

				// LIGHTMAPPING IS ON
				#ifndef LIGHTMAP_OFF 
					// THIS IS MULTIPLIED TIMES THE AMBIENT LEVEL SO REFLETIONS MATCH AMBIENT LIGHTING
					indirectSpecular += lightmapAccumulation.rgb * _RefAmb;
					//indirectSpecular *= lightmapAccumulation.rgb * _AoMap_var ;
				#else 
					// SKYSHOP REFLECTIONS
					float3 indirectSpecular =  marmoMipSpecular(viewReflectDirection, i.posWorld.rgb, _SpecMap_var.a).rgb;


					float3 indirectSpecularAmb = indirectSpecular * _AoMap_var * _RoomAmb + _RefAmb;
					float3 indirectSpecularDirect = indirectSpecular * (lightAccumulation.rgb - _RoomAmb ) * _AoMap_var;

					indirectSpecular = indirectSpecularAmb + max(0,indirectSpecularDirect);

				#endif

				// TOTAL SPECULAR
                float3 specular = (directSpecular + indirectSpecular) * _SpecMap_var.rgb ;
				


			/////// DIFFUSE:
				// ON  ->
                #ifndef LIGHTMAP_OFF 
                    float3 directDiffuse = float3(0,0,0);
                #else  // OFF ->
                    float3 directDiffuse = lightAccumulation.rgb ;
                #endif

				// ON
                #ifndef LIGHTMAP_OFF 
					 lightAccumulation.rgb + lightmapAccumulation.rgb;

					/* GI BASED ON DISTANCE (Removed in preference to location global GI)
					float clampNode = clamp((1.0 - (distance(_L1Pos.rgb,i.posWorld.rgb)/_L1Falloff)),0,1);
					directDiffuse += (lightAccumulation.rgb * lightmapAccumulation.rgb) + ( lightmapAccumulation.rgb * (clampNode * _L1Intensity)) ;
					*/

					// BASED IN SINGLE AMBIENT VALUE
					directDiffuse += lightAccumulation.rgb * lightmapAccumulation.rgb  ;	// Multi lighting by the lightmap
					directDiffuse += lightmapAccumulation.rgb * _RoomAmb * _AoMap_var ;		// Add light map the ambient amount and mult by local AOmap
				#else 
					directDiffuse += marmoDiffuse(normalDirection).rgb * _RoomAmb; // Diffuse Ambient Light
                #endif

				float3 diffuse =  directDiffuse * _MainTex_var.rgb ;
				//float3 diffuse =  directDiffuse * sqrt(_MainTex_var.rgb) ;
				//float3 diffuse =  sqrt(directDiffuse) * _MainTex_var.rgb ;
				//float3 diffuse =  sqrt(directDiffuse * _MainTex_var.rgb) ;

				// This is the function that determins how much diffuse based on reflection
                diffuse *= 1-specularMonochrome;
				//diffuse = (1,1,1,1);
			/////// Final Color:
                float3 finalColor = diffuse + (specular*specular);
                return fixed4(  finalColor, 1 );


				
            }
            ENDCG
        } 
		// PRE-PASS FINAL <-









		// FORWARD BASE ->
        Pass {
			
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile MARMO_SKY_BLEND_OFF MARMO_SKY_BLEND_ON
            #pragma multi_compile MARMO_BOX_PROJECTION_OFF MARMO_BOX_PROJECTION_ON
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl

			//INCLUDE FORWARD BASE



// FORWARD BASE

			#ifndef LIGHTMAP_OFF
                float4 unity_LightmapST;
                sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    sampler2D unity_LightmapInd;
                #endif
            #endif

            //uniform float4 _Color;
            uniform float _Shininess;
            uniform sampler2D _MainTex; 
			uniform float4 _MainTex_ST;
            #ifndef MARMO_LIGHTMAP_DEFINED
            #define MARMO_LIGHTMAP_DEFINED
            	#ifdef LIGHTMAP_OFF
            	#define lightmap float3(1.0,1.0,1.0)
            	#endif
            #endif
            
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
            
            
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };

            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                #ifndef LIGHTMAP_OFF
                    float2 uvLM : TEXCOORD7;
                #endif
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
                #ifndef LIGHTMAP_OFF
                    o.uvLM = v.texcoord1 * unity_LightmapST.xy + unity_LightmapST.zw;
                #endif
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }

            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                #ifndef LIGHTMAP_OFF
                    float4 lmtex = tex2D(unity_Lightmap,i.uvLM);
                    #ifndef DIRLIGHTMAP_OFF
                        float3 lightmap = DecodeLightmap(lmtex);
                        float3 scalePerBasisVector = DecodeLightmap(tex2D(unity_LightmapInd,i.uvLM));
                        UNITY_DIRBASIS
                        half3 normalInRnmBasis = saturate (mul (unity_DirBasis, float3(0,0,1)));
                        lightmap *= dot (normalInRnmBasis, scalePerBasisVector);
                    #else
                        float3 lightmap = DecodeLightmap(lmtex);
                    #endif
                #endif
                #ifndef LIGHTMAP_OFF
                    #ifdef DIRLIGHTMAP_OFF
                        float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                    #else
                        float3 lightDirection = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
                        lightDirection = mul(lightDirection,tangentTransform); // Tangent to world
                    #endif
                #else
                    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                #endif
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Shininess;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _SpecColor.rgb;
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                #if !defined(LIGHTMAP_OFF) && defined(DIRLIGHTMAP_OFF)
                    float3 directSpecular = float3(0,0,0);
                #else
                    float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm;
                #endif
                float3 indirectSpecular = (0 + (lightmap*marmoMipSpecular(viewReflectDirection, i.posWorld.rgb, _Shininess)).rgb);
                float3 specular = (directSpecular + indirectSpecular) * specularColor;
                #ifndef LIGHTMAP_OFF
                    #ifndef DIRLIGHTMAP_OFF
                        specular *= lightmap;
                    #else
                        specular *= (floor(attenuation) * _LightColor0.xyz);
                    #endif
                #else
                    specular *= (floor(attenuation) * _LightColor0.xyz);
                #endif
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);

                #ifndef LIGHTMAP_OFF
                    float3 directDiffuse = float3(0,0,0);
                #else
                    float3 directDiffuse = max( 0.0, NdotL) * InvPi * attenColor;
                #endif

                #ifndef LIGHTMAP_OFF
                    #ifdef SHADOWS_SCREEN
                        #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) && defined(SHADER_API_MOBILE)
                            directDiffuse += min(lightmap.rgb, attenuation);
                        #else
                            directDiffuse += max(min(lightmap.rgb,attenuation*lmtex.rgb), lightmap.rgb*attenuation*0.5);
                        #endif
                    #else
                        directDiffuse += lightmap.rgb;
                    #endif
                #endif
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                //float3 diffuse = (directDiffuse + indirectDiffuse) * lerp(_Color.rgb,_MainTex_var.rgb,0.0);
				float3 diffuse = (directDiffuse + indirectDiffuse) *_MainTex_var.rgb;
                diffuse *= 1-specularMonochrome;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor, 1);
            }
            
            ENDCG
        }

















		// FORWARD ADD -> 
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl

			//INCLUDE FORWARD ADD
			uniform float4 _Color;
            uniform float _Shininess;
            uniform sampler2D _MainTex; 
			uniform float4 _MainTex_ST;

            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
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
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
	////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
	///////// Gloss:
                float gloss = _Shininess;
                float specPow = exp2( gloss * 10.0+1.0);
	////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _SpecColor.rgb;
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm;
                float3 specular = directSpecular * specularColor;

	/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
				//float3 diffuse = directDiffuse * lerp(_Color.rgb,_MainTex_var.rgb,0.0);
                float3 diffuse = directDiffuse * _MainTex_var.rgb;
                diffuse *= 1-specularMonochrome;

	/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }

            
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
