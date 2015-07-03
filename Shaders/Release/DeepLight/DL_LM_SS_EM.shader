


Shader "DeepLight/Specular/DL_LM_SS_EM" {
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
		_RoomAmb ("RoomAmb", Color) = (0.0,0.0,0.0,1)
		_RefAmb ("ReflectionAmb", Color) = (0.0,0.0,0.0,1)

        _L1Pos ("L1Pos", Vector) = (0,0,0,0)
		_L2Pos ("L2Pos", Vector) = (0,0,0,0)

		_L1Intensity ("L1Intensity ", Float ) = 1
		_L1Color ("L1Color", Color) = (0.2,0.2,0.2,1)
		_L2Intensity ("L2Intensity ", Float ) = 1
		_L2Color ("L2Color", Color) = (0.2,0.2,0.2,1)

		// EMISSION SPECIFIC
		_E1Map ("EmMap", 2D) = "white" {}
		_E1Intensity ("E1Intensity ", Float ) = 1
		_E1Color ("E1Color", Color) = (0.2,0.2,0.2,1)
		
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
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
        }
        Pass {
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

				// EMISSION SPECIFIC
				uniform float _E1Intensity;
				uniform float4 _E1Color;
				uniform sampler2D _E1Map; uniform float4 _E1Map_ST;

				#include "SkyShopReflection.cginc"
			
				#include "PrepassFinalB.cginc"

				#include "PrepassFinalNorm.cginc"

				#include "PrepassFinalC.cginc"

				#include "PrepassFinalD_LM.cginc"

				// EMISSION SPECIFIC
				float3 _E1Map_var = tex2D(_E1Map, TRANSFORM_TEX(i.uv0, _E1Map) );
				float3 emissive = _E1Map_var.rgb * fresnel * fresnel * _E1Intensity * _E1Color;

				/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }



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
			#include "ForwardBase.cginc"
            
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
			#include "ForwardAdd.cginc"

            
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}