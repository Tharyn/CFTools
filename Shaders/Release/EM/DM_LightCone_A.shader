// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:5030,x:33370,y:32765,varname:node_5030,prsc:2|diff-857-RGB,emission-7819-OUT,custl-857-RGB,alpha-4300-OUT;n:type:ShaderForge.SFN_Color,id:857,x:32620,y:32713,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_857,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:0.2;n:type:ShaderForge.SFN_Tex2d,id:610,x:32620,y:32926,ptovrint:False,ptlb:GradMap,ptin:_GradMap,varname:node_610,prsc:2,tex:244973a958d060e4fa058fa13b87cc3f,ntxv:0,isnm:False|UVIN-230-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:230,x:32318,y:32872,varname:node_230,prsc:2,uv:1;n:type:ShaderForge.SFN_Fresnel,id:2345,x:32637,y:33232,varname:node_2345,prsc:2|EXP-788-OUT;n:type:ShaderForge.SFN_OneMinus,id:1530,x:32827,y:33173,varname:node_1530,prsc:2|IN-2345-OUT;n:type:ShaderForge.SFN_Multiply,id:7819,x:33014,y:33048,varname:node_7819,prsc:2|A-610-R,B-1530-OUT;n:type:ShaderForge.SFN_ValueProperty,id:788,x:32307,y:33251,ptovrint:False,ptlb:SideFade,ptin:_SideFade,varname:node_788,prsc:2,glob:False,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:4300,x:33188,y:33048,varname:node_4300,prsc:2|A-857-A,B-7819-OUT;proporder:857-610-788;pass:END;sub:END;*/

Shader "DM2/DM_LightCone_A" {
    Properties {
        _Color ("Color", Color) = (1,1,1,0.2)
        _GradMap ("GradMap", 2D) = "white" {}
        _SideFade ("SideFade", Float ) = 0.05
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
            Cull Off
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
            uniform float4 _Color;
            uniform sampler2D _GradMap; uniform float4 _GradMap_ST;
            uniform float _SideFade;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv1 = v.texcoord1;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
////// Lighting:
////// Emissive:
                float4 _GradMap_var = tex2D(_GradMap,TRANSFORM_TEX(i.uv1, _GradMap));
                float node_7819 = (_GradMap_var.r*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_SideFade)));
                float3 emissive = float3(node_7819,node_7819,node_7819);
                float3 finalColor = emissive + _Color.rgb;
                return fixed4(finalColor,(_Color.a*node_7819));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
