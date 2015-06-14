// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3835,x:33350,y:32689,varname:node_3835,prsc:2|custl-5496-OUT;n:type:ShaderForge.SFN_Color,id:7329,x:32534,y:32797,ptovrint:False,ptlb:SolidColor,ptin:_SolidColor,varname:node_7329,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:5496,x:32878,y:32909,varname:node_5496,prsc:2|A-7329-RGB,B-28-RGB,T-4020-OUT;n:type:ShaderForge.SFN_Tex2d,id:28,x:32534,y:32988,ptovrint:False,ptlb:Screen,ptin:_Screen,varname:node_28,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:4020,x:32534,y:33224,ptovrint:False,ptlb:Fade,ptin:_Fade,varname:node_4020,prsc:2,glob:False,v1:0;proporder:7329-28-4020;pass:END;sub:END;*/

Shader "DM_GUI/FadeTexture" {
    Properties {
        _SolidColor ("SolidColor", Color) = (0.5,0.5,0.5,1)
        _Screen ("Screen", 2D) = "white" {}
        _Fade ("Fade", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _SolidColor;
            uniform sampler2D _Screen; uniform float4 _Screen_ST;
            uniform float _Fade;
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
                float4 _Screen_var = tex2D(_Screen,TRANSFORM_TEX(i.uv0, _Screen));
                float3 finalColor = lerp(_SolidColor.rgb,_Screen_var.rgb,_Fade);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
