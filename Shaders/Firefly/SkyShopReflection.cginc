

			// START SKY SHOP
            #ifndef MARMO_LIGHTMAP_DEFINED
				#define MARMO_LIGHTMAP_DEFINED
            		#ifdef LIGHTMAP_OFF
            			#define lightmap float3(1.0,1.0,1.0)
            		#endif
            #endif
            
			// SS IBL EXPOSURE
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
            