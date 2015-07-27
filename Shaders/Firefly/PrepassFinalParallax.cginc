








				float4 _HiMap_var = tex2D(_AoMap, TRANSFORM_TEX(i.uv0, _AoMap) );
                float2 parallaxUV = (_ParalaxAmt*(_HiMap_var.a -.75)*mul(tangentTransform, viewDirection).xy + i.uv0);

				/////// ADDITIONAL MAPS
				float4 _MainTex_var = tex2D(_MainTex, TRANSFORM_TEX(parallaxUV.rg, _MainTex) );
				float4 _SpecMap_var = tex2D(_SpecMap, TRANSFORM_TEX(parallaxUV.rg, _SpecMap) );
				float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(parallaxUV.rg, _BumpMap)));
				float4 _AoLtMt_var = tex2D(_AoLtMt, TRANSFORM_TEX(parallaxUV.rg, _AoLtMt) );