// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3094,x:33545,y:32689,varname:node_3094,prsc:2|diff-9288-RGB,emission-9288-RGB,alpha-2681-OUT;n:type:ShaderForge.SFN_Color,id:9288,x:32675,y:32612,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_9288,prsc:2,glob:False,c1:0.5,c2:0,c3:1,c4:0.5;n:type:ShaderForge.SFN_Tex2d,id:507,x:32471,y:32809,ptovrint:False,ptlb:TopDown,ptin:_TopDown,varname:node_507,prsc:2,tex:e2f19ccdb96668f42b4c9a7502f0edd6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1030,x:32405,y:33052,ptovrint:False,ptlb:LeftRight,ptin:_LeftRight,varname:node_1030,prsc:2,tex:dea48e1170f058441b554e86a766ea2e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:985,x:32885,y:32930,varname:node_985,prsc:2|A-9288-RGB,B-507-RGB,C-1030-RGB;n:type:ShaderForge.SFN_ComponentMask,id:2681,x:33197,y:32996,varname:node_2681,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-985-OUT;proporder:9288-507-1030;pass:END;sub:END;*/

Shader "DM2/DM_MoonBeams" {
    Properties {
        _Color ("Color", Color) = (0.5,0,1,0.5)
        _TopDown ("TopDown", 2D) = "white" {}
        _LeftRight ("LeftRight", 2D) = "white" {}
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
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform sampler2D _TopDown; uniform float4 _TopDown_ST;
            uniform sampler2D _LeftRight; uniform float4 _LeftRight_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _Color.rgb;
////// Emissive:
                float3 emissive = _Color.rgb;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 _TopDown_var = tex2D(_TopDown,TRANSFORM_TEX(i.uv0, _TopDown));
                float4 _LeftRight_var = tex2D(_LeftRight,TRANSFORM_TEX(i.uv0, _LeftRight));
                return fixed4(finalColor,(_Color.rgb*_TopDown_var.rgb*_LeftRight_var.rgb).r);
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform sampler2D _TopDown; uniform float4 _TopDown_ST;
            uniform sampler2D _LeftRight; uniform float4 _LeftRight_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * _Color.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                float4 _TopDown_var = tex2D(_TopDown,TRANSFORM_TEX(i.uv0, _TopDown));
                float4 _LeftRight_var = tex2D(_LeftRight,TRANSFORM_TEX(i.uv0, _LeftRight));
                return fixed4(finalColor * (_Color.rgb*_TopDown_var.rgb*_LeftRight_var.rgb).r,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
