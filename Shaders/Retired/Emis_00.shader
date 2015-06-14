// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:1,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:2928,x:33756,y:32523,varname:node_2928,prsc:2|diff-9217-RGB,emission-8658-OUT;n:type:ShaderForge.SFN_Vector4Property,id:441,x:32231,y:33062,ptovrint:False,ptlb:LightPos,ptin:_LightPos,varname:node_441,prsc:2,glob:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Color,id:8231,x:32769,y:32693,ptovrint:False,ptlb:LightColor,ptin:_LightColor,varname:node_8231,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9217,x:32493,y:32522,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_9217,prsc:2,ntxv:2,isnm:False;n:type:ShaderForge.SFN_FragmentPosition,id:6236,x:32231,y:33242,varname:node_6236,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8658,x:33332,y:32759,varname:node_8658,prsc:2|A-2590-OUT,B-3458-OUT,C-8555-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3539,x:32406,y:32893,ptovrint:False,ptlb:LightDistance,ptin:_LightDistance,varname:node_3539,prsc:2,glob:False,v1:5;n:type:ShaderForge.SFN_Distance,id:8563,x:32494,y:33062,varname:node_8563,prsc:2|A-441-XYZ,B-6236-XYZ;n:type:ShaderForge.SFN_Subtract,id:3819,x:32678,y:33014,varname:node_3819,prsc:2|A-3539-OUT,B-8563-OUT;n:type:ShaderForge.SFN_Divide,id:1762,x:32878,y:33014,varname:node_1762,prsc:2|A-3819-OUT,B-3539-OUT;n:type:ShaderForge.SFN_Dot,id:1314,x:32865,y:33410,varname:node_1314,prsc:2,dt:4|A-424-OUT,B-9669-OUT;n:type:ShaderForge.SFN_NormalVector,id:9669,x:32638,y:33504,prsc:2,pt:True;n:type:ShaderForge.SFN_Subtract,id:5257,x:32494,y:33219,varname:node_5257,prsc:2|A-441-XYZ,B-6236-XYZ;n:type:ShaderForge.SFN_Normalize,id:424,x:32694,y:33301,varname:node_424,prsc:2|IN-5257-OUT;n:type:ShaderForge.SFN_Tex2d,id:1935,x:33387,y:33415,ptovrint:False,ptlb:Grad,ptin:_Grad,varname:node_1935,prsc:2,ntxv:0,isnm:False|UVIN-5185-OUT;n:type:ShaderForge.SFN_Append,id:5185,x:33101,y:33416,varname:node_5185,prsc:2|A-1314-OUT,B-7980-OUT;n:type:ShaderForge.SFN_Vector1,id:7980,x:32923,y:33622,varname:node_7980,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:4939,x:32910,y:33167,varname:node_4939,prsc:2,v1:0;n:type:ShaderForge.SFN_Max,id:8555,x:33121,y:33138,varname:node_8555,prsc:2|A-1762-OUT,B-4939-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3458,x:32769,y:32873,ptovrint:False,ptlb:LightIntensity,ptin:_LightIntensity,varname:node_3458,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2590,x:33022,y:32662,varname:node_2590,prsc:2|A-9217-RGB,B-8231-RGB;proporder:9217-1935-8231-3539-3458-441;pass:END;sub:END;*/

Shader "DM/Emis_00" {
    Properties {
        _MainTex ("MainTex", 2D) = "black" {}
        _Grad ("Grad", 2D) = "white" {}
        _LightColor ("LightColor", Color) = (1,1,1,1)
        _LightDistance ("LightDistance", Float ) = 5
        _LightIntensity ("LightIntensity", Float ) = 1
        _LightPos ("LightPos", Vector) = (0,0,0,0)
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform fixed4 unity_Ambient;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                return fixed4( normalDirection * 0.5 + 0.5, max(0.5,0.0078125) );
            }
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
            #pragma multi_compile_prepassfinal
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _LightBuffer;
            #if defined (SHADER_API_XBOX360) && defined (HDR_LIGHT_PREPASS_ON)
                sampler2D _LightSpecBuffer;
            #endif
            uniform fixed4 unity_Ambient;
            uniform float4 _LightPos;
            uniform float4 _LightColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _LightDistance;
            uniform float _LightIntensity;
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
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
////// Lighting:
                half4 lightAccumulation = tex2Dproj(_LightBuffer, UNITY_PROJ_COORD(i.projPos));
                #if defined (SHADER_API_GLES) || defined (SHADER_API_GLES3)
                    lightAccumulation = max(lightAccumulation, half4(0.001));
                #endif
                #ifndef HDR_LIGHT_PREPASS_ON
                    lightAccumulation = -log2(lightAccumulation);
                #endif
                #if defined (SHADER_API_XBOX360) && defined (HDR_LIGHT_PREPASS_ON)
                    lightAccumulation.w = tex2Dproj (_LightSpecBuffer, UNITY_PROJ_COORD(i.projPos)).r;
                #endif
/////// Diffuse:
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = lightAccumulation.rgb * 0.5;
                indirectDiffuse += unity_Ambient.rgb*0.5; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _MainTex_var.rgb;
////// Emissive:
                float3 emissive = ((_MainTex_var.rgb*_LightColor.rgb)*_LightIntensity*max(((_LightDistance-distance(_LightPos.rgb,i.posWorld.rgb))/_LightDistance),0.0));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
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
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _LightPos;
            uniform float4 _LightColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _LightDistance;
            uniform float _LightIntensity;
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
                VertexOutput o = (VertexOutput)0;
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
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _MainTex_var.rgb;
////// Emissive:
                float3 emissive = ((_MainTex_var.rgb*_LightColor.rgb)*_LightIntensity*max(((_LightDistance-distance(_LightPos.rgb,i.posWorld.rgb))/_LightDistance),0.0));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
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
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _LightPos;
            uniform float4 _LightColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _LightDistance;
            uniform float _LightIntensity;
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
                VertexOutput o = (VertexOutput)0;
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = directDiffuse * _MainTex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
