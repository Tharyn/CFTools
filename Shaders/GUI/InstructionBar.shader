// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7079,x:33342,y:32659,varname:node_7079,prsc:2|custl-6034-RGB,alpha-7658-OUT;n:type:ShaderForge.SFN_Tex2d,id:6034,x:32571,y:32770,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6034,prsc:2,tex:4d9a74ca05b3d8048bae585e907d7c02,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Vector1,id:6653,x:32639,y:32990,varname:node_6653,prsc:2,v1:0;n:type:ShaderForge.SFN_Lerp,id:7658,x:32897,y:32959,varname:node_7658,prsc:2|A-6034-A,B-6653-OUT,T-581-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2136,x:32501,y:33185,ptovrint:False,ptlb:Amount,ptin:_Amount,varname:node_2136,prsc:2,glob:False,v1:0.5;n:type:ShaderForge.SFN_RemapRange,id:581,x:32691,y:33083,varname:node_581,prsc:2,frmn:0,frmx:1,tomn:1,tomx:0|IN-2136-OUT;proporder:6034-2136;pass:END;sub:END;*/

Shader "DM_GUI/InstructionBar" {
    Properties {
        _MainTex ("MainTex", 2D) = "black" {}
        _Amount ("Amount", Float ) = 0.5
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Amount;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 finalColor = _MainTex_var.rgb;
                return fixed4(finalColor,lerp(_MainTex_var.a,0.0,(_Amount*-1.0+1.0)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
