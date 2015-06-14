// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:True,lprd:False,rprd:False,enco:True,frtr:True,vitr:True,dbil:True,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:29,x:33754,y:32663,varname:node_29,prsc:2|diff-8033-OUT,emission-2562-OUT;n:type:ShaderForge.SFN_Color,id:7239,x:32333,y:32560,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7239,prsc:2,glob:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:8574,x:32535,y:33034,varname:node_8574,prsc:2|EXP-1258-OUT;n:type:ShaderForge.SFN_Slider,id:1258,x:32167,y:33089,ptovrint:False,ptlb:RimIOR,ptin:_RimIOR,varname:node_1258,prsc:2,min:4,cur:2.402287,max:0;n:type:ShaderForge.SFN_Multiply,id:5162,x:32764,y:33034,varname:node_5162,prsc:2|A-8574-OUT,B-5307-OUT;n:type:ShaderForge.SFN_Slider,id:5307,x:32258,y:33205,ptovrint:False,ptlb:RimAmt,ptin:_RimAmt,varname:node_5307,prsc:2,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:8814,x:32638,y:32897,varname:node_8814,prsc:2|A-7239-RGB,B-5162-OUT;n:type:ShaderForge.SFN_Add,id:4008,x:32936,y:32469,varname:node_4008,prsc:2|A-7239-RGB,B-2562-OUT;n:type:ShaderForge.SFN_Color,id:1757,x:32333,y:32744,ptovrint:False,ptlb:Rim,ptin:_Rim,varname:node_7239,prsc:2,glob:False,c1:0.5,c2:0.5,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:162,x:32652,y:32734,varname:node_162,prsc:2|A-1757-RGB,B-8814-OUT,C-9285-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:617,x:32846,y:32869,varname:node_617,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2562,x:33106,y:32816,varname:node_2562,prsc:2|A-162-OUT,B-617-OUT;n:type:ShaderForge.SFN_Multiply,id:8033,x:33406,y:32563,varname:node_8033,prsc:2|A-4008-OUT,B-9285-OUT;n:type:ShaderForge.SFN_Tex2d,id:5995,x:32695,y:33180,ptovrint:False,ptlb:AOMap,ptin:_AOMap,varname:node_5995,prsc:2,tex:2b68f2e4d317369448d5527a59a8c0fa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:2638,x:32595,y:33422,ptovrint:False,ptlb:AO,ptin:_AO,varname:node_2638,prsc:2,min:1,cur:0,max:0;n:type:ShaderForge.SFN_Lerp,id:9285,x:33012,y:33222,varname:node_9285,prsc:2|A-5995-RGB,B-9428-OUT,T-2638-OUT;n:type:ShaderForge.SFN_Vector1,id:9428,x:32724,y:33354,varname:node_9428,prsc:2,v1:1;proporder:7239-1757-1258-5307-5995-2638;pass:END;sub:END;*/

Shader "DM/DM_Cloth_LM" {
    Properties {
        _Color ("Color", Color) = (0,0,1,1)
        _Rim ("Rim", Color) = (0.5,0.5,1,1)
        _RimIOR ("RimIOR", Range(4, 0)) = 2.402287
        _RimAmt ("RimAmt", Range(0, 1)) = 1
        _AOMap ("AOMap", 2D) = "white" {}
        _AO ("AO", Range(1, 0)) = 0
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
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #ifndef LIGHTMAP_OFF
                float4 unity_LightmapST;
                sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform float4 _Color;
            uniform float _RimIOR;
            uniform float _RimAmt;
            uniform float4 _Rim;
            uniform sampler2D _AOMap; uniform float4 _AOMap_ST;
            uniform float _AO;
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
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                #ifndef LIGHTMAP_OFF
                    float3 directDiffuse = float3(0,0,0);
                #else
                    float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
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
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb*2; // Ambient Light
                float4 _AOMap_var = tex2D(_AOMap,TRANSFORM_TEX(i.uv0, _AOMap));
                float node_9428 = 1.0;
                float3 node_9285 = lerp(_AOMap_var.rgb,float3(node_9428,node_9428,node_9428),_AO);
                float3 node_2562 = ((_Rim.rgb*(_Color.rgb*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimIOR)*_RimAmt))*node_9285)*attenuation);
                float3 diffuse = (directDiffuse + indirectDiffuse) * ((_Color.rgb+node_2562)*node_9285);
////// Emissive:
                float3 emissive = node_2562;
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _RimIOR;
            uniform float _RimAmt;
            uniform float4 _Rim;
            uniform sampler2D _AOMap; uniform float4 _AOMap_ST;
            uniform float _AO;
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
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
                float4 _AOMap_var = tex2D(_AOMap,TRANSFORM_TEX(i.uv0, _AOMap));
                float node_9428 = 1.0;
                float3 node_9285 = lerp(_AOMap_var.rgb,float3(node_9428,node_9428,node_9428),_AO);
                float3 node_2562 = ((_Rim.rgb*(_Color.rgb*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimIOR)*_RimAmt))*node_9285)*attenuation);
                float3 diffuse = directDiffuse * ((_Color.rgb+node_2562)*node_9285);
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
