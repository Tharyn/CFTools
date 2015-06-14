// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:True,frtr:True,vitr:True,dbil:True,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7438,x:34731,y:32530,varname:node_7438,prsc:2|diff-1308-RGB,spec-7418-OUT,gloss-8140-OUT,normal-4658-OUT,amspl-251-RGB,alpha-3989-OUT,refract-319-OUT;n:type:ShaderForge.SFN_Color,id:1308,x:33832,y:32054,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1308,prsc:2,glob:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:6888,x:32565,y:32721,varname:node_6888,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:8555,x:32841,y:32814,varname:node_8555,prsc:2|A-6888-UVOUT,B-9310-OUT;n:type:ShaderForge.SFN_Vector1,id:9310,x:32523,y:32936,varname:node_9310,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:1775,x:33048,y:32814,ptovrint:False,ptlb:BumpMap,ptin:_BumpMap,varname:node_1775,prsc:2,ntxv:3,isnm:True|UVIN-8555-OUT;n:type:ShaderForge.SFN_Vector3,id:3590,x:32853,y:32609,varname:node_3590,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Lerp,id:4658,x:33245,y:32742,varname:node_4658,prsc:2|A-3590-OUT,B-1775-RGB,T-4695-OUT;n:type:ShaderForge.SFN_Slider,id:4695,x:32879,y:33102,ptovrint:False,ptlb:RefractStrength,ptin:_RefractStrength,varname:node_4695,prsc:2,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:2706,x:33758,y:33201,ptovrint:False,ptlb:TransIOR,ptin:_TransIOR,varname:node_2706,prsc:2,min:1,cur:1,max:0;n:type:ShaderForge.SFN_Fresnel,id:9882,x:33397,y:32859,varname:node_9882,prsc:2|EXP-944-OUT;n:type:ShaderForge.SFN_SkyshopSpec,id:251,x:33686,y:32739,varname:node_251,prsc:2,sprot:True,spblend:True,splmocc:False|GLOSS-8140-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2562,x:33309,y:33210,varname:node_2562,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1775-RGB;n:type:ShaderForge.SFN_Multiply,id:9041,x:33045,y:33352,varname:node_9041,prsc:2|A-4695-OUT,B-6799-OUT;n:type:ShaderForge.SFN_Vector1,id:6799,x:32693,y:33418,varname:node_6799,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:319,x:33837,y:33367,varname:node_319,prsc:2|A-2562-OUT,B-9041-OUT;n:type:ShaderForge.SFN_Slider,id:493,x:33247,y:32350,ptovrint:False,ptlb:Spec,ptin:_Spec,varname:node_493,prsc:2,min:0,cur:0.7781649,max:1;n:type:ShaderForge.SFN_Slider,id:8140,x:33027,y:32453,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_8140,prsc:2,min:0,cur:0.8573582,max:1;n:type:ShaderForge.SFN_Slider,id:8311,x:33778,y:33008,ptovrint:False,ptlb:Tran,ptin:_Tran,varname:node_8311,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:3278,x:33914,y:32934,varname:node_3278,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:4635,x:33142,y:32611,ptovrint:False,ptlb:IOR,ptin:_IOR,varname:node_4635,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:7418,x:33840,y:32492,varname:node_7418,prsc:2|A-493-OUT,B-7221-OUT;n:type:ShaderForge.SFN_Multiply,id:7221,x:33575,y:32559,varname:node_7221,prsc:2|A-9882-OUT,B-4635-OUT;n:type:ShaderForge.SFN_Slider,id:944,x:32852,y:32988,ptovrint:False,ptlb:IORexp,ptin:_IORexp,varname:node_944,prsc:2,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:5220,x:33969,y:32315,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5220,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:4337,x:34197,y:32875,varname:node_4337,prsc:2|A-5220-R,B-3278-OUT,T-8311-OUT;n:type:ShaderForge.SFN_Add,id:3989,x:34377,y:32987,varname:node_3989,prsc:2|A-4337-OUT,B-2710-OUT;n:type:ShaderForge.SFN_Vector1,id:8426,x:34030,y:33270,varname:node_8426,prsc:2,v1:0;n:type:ShaderForge.SFN_Lerp,id:2710,x:34208,y:33133,varname:node_2710,prsc:2|A-9882-OUT,B-8426-OUT,T-2706-OUT;proporder:1308-5220-1775-4695-493-8140-4635-944-8311-2706;pass:END;sub:END;*/

Shader "DM2/DM_SS_Refract" {
    Properties {
        _Color ("Color", Color) = (0,0,0,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _BumpMap ("BumpMap", 2D) = "bump" {}
        _RefractStrength ("RefractStrength", Range(0, 1)) = 1
        _Spec ("Spec", Range(0, 1)) = 0.7781649
        _Gloss ("Gloss", Range(0, 1)) = 0.8573582
        _IOR ("IOR", Range(0, 1)) = 0
        _IORexp ("IORexp", Range(0, 1)) = 1
        _Tran ("Tran", Range(0, 1)) = 0
        _TransIOR ("TransIOR", Range(1, 0)) = 1
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile MARMO_SKY_BLEND_OFF MARMO_SKY_BLEND_ON
            #pragma multi_compile MARMO_BOX_PROJECTION_OFF MARMO_BOX_PROJECTION_ON
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _RefractStrength;
            uniform float _TransIOR;
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
            
            
            uniform float _Spec;
            uniform float _Gloss;
            uniform float _Tran;
            uniform float _IOR;
            uniform float _IORexp;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                VertexOutput o;
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
                float2 node_8555 = (i.uv0*1.0);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(node_8555, _BumpMap)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_BumpMap_var.rgb.rg*(_RefractStrength*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalLocal = lerp(float3(0,0,1),_BumpMap_var.rgb,_RefractStrength);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_9882 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_IORexp);
                float node_7418 = (_Spec+(node_9882*_IOR));
                float3 specularColor = float3(node_7418,node_7418,node_7418);
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm;
                float3 indirectSpecular = (0 + marmoMipSpecular(viewReflectDirection, i.posWorld.rgb, _Gloss).rgb);
                float3 specular = (directSpecular + indirectSpecular) * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb*2; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _Color.rgb;
                diffuse *= 1-specularMonochrome;
/// Final Color:
                float3 finalColor = diffuse + specular;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                return fixed4(lerp(sceneColor.rgb, finalColor,(lerp(_MainTex_var.r,0.0,_Tran)+lerp(node_9882,0.0,_TransIOR))),1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
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
            #pragma glsl
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _RefractStrength;
            uniform float _TransIOR;
            uniform float _Spec;
            uniform float _Gloss;
            uniform float _Tran;
            uniform float _IOR;
            uniform float _IORexp;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                VertexOutput o;
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
                float2 node_8555 = (i.uv0*1.0);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(node_8555, _BumpMap)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_BumpMap_var.rgb.rg*(_RefractStrength*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalLocal = lerp(float3(0,0,1),_BumpMap_var.rgb,_RefractStrength);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_9882 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_IORexp);
                float node_7418 = (_Spec+(node_9882*_IOR));
                float3 specularColor = float3(node_7418,node_7418,node_7418);
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                return fixed4(finalColor * (lerp(_MainTex_var.r,0.0,_Tran)+lerp(node_9882,0.0,_TransIOR)),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
