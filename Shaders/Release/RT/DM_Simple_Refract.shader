// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:True,frtr:True,vitr:True,dbil:True,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:8652,x:33205,y:32702,varname:node_8652,prsc:2|diff-8214-RGB,spec-5036-RGB,gloss-5036-A,normal-738-OUT,alpha-8214-A,refract-6986-OUT;n:type:ShaderForge.SFN_Color,id:5036,x:32758,y:32460,ptovrint:False,ptlb:SpecGloss,ptin:_SpecGloss,varname:node_5036,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_SkyshopSpec,id:4307,x:32760,y:32743,varname:node_4307,prsc:2,sprot:True,spblend:True,splmocc:False|GLOSS-5036-A;n:type:ShaderForge.SFN_Multiply,id:6986,x:32269,y:33114,varname:node_6986,prsc:2|A-5645-OUT,B-9623-OUT;n:type:ShaderForge.SFN_Multiply,id:9623,x:32011,y:33117,varname:node_9623,prsc:2|A-2836-OUT,B-1130-OUT;n:type:ShaderForge.SFN_Vector1,id:1130,x:31702,y:33181,varname:node_1130,prsc:2,v1:0.2;n:type:ShaderForge.SFN_ComponentMask,id:5645,x:32093,y:32882,varname:node_5645,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5950-RGB;n:type:ShaderForge.SFN_Tex2d,id:5950,x:31899,y:32737,ptovrint:False,ptlb:BumpMask,ptin:_BumpMask,varname:node_5950,prsc:2,tex:7a726c5bde0fc6544b3cfd01a95c8fae,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Vector3,id:3629,x:31790,y:32300,varname:node_3629,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Lerp,id:738,x:32195,y:32543,varname:node_738,prsc:2|A-3629-OUT,B-5950-RGB,T-2836-OUT;n:type:ShaderForge.SFN_Slider,id:2836,x:31623,y:33025,ptovrint:False,ptlb:RefractionIOR,ptin:_RefractionIOR,varname:node_2836,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:8214,x:32754,y:32265,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8214,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:5036-5950-2836-8214;pass:END;sub:END;*/

Shader "DM2/DM_Simple_Refract" {
    Properties {
        _SpecGloss ("SpecGloss", Color) = (0.5,0.5,0.5,1)
        _BumpMask ("BumpMask", 2D) = "bump" {}
        _RefractionIOR ("RefractionIOR", Range(0, 1)) = 0
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
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
            uniform sampler2D _GrabTexture;
            uniform float4 _SpecGloss;
            uniform sampler2D _BumpMask; uniform float4 _BumpMask_ST;
            uniform float _RefractionIOR;
            uniform float4 _Color;
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
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 _BumpMask_var = UnpackNormal(tex2D(_BumpMask,TRANSFORM_TEX(i.uv0, _BumpMask)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_BumpMask_var.rgb.rg*(_RefractionIOR*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalLocal = lerp(float3(0,0,1),_BumpMask_var.rgb,_RefractionIOR);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _SpecGloss.a;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _SpecGloss.rgb;
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm;
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb*2; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _Color.rgb;
                diffuse *= 1-specularMonochrome;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(lerp(sceneColor.rgb, finalColor,_Color.a),1);
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
            uniform sampler2D _GrabTexture;
            uniform float4 _SpecGloss;
            uniform sampler2D _BumpMask; uniform float4 _BumpMask_ST;
            uniform float _RefractionIOR;
            uniform float4 _Color;
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
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 _BumpMask_var = UnpackNormal(tex2D(_BumpMask,TRANSFORM_TEX(i.uv0, _BumpMask)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_BumpMask_var.rgb.rg*(_RefractionIOR*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalLocal = lerp(float3(0,0,1),_BumpMask_var.rgb,_RefractionIOR);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _SpecGloss.a;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _SpecGloss.rgb;
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm;
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
                float3 diffuse = directDiffuse * _Color.rgb;
                diffuse *= 1-specularMonochrome;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * _Color.a,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
