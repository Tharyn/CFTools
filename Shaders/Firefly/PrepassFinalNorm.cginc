
				/////// ADDITIONAL MAPS
				float4 _MainTex_var = tex2D(_MainTex, TRANSFORM_TEX(i.uv0, _MainTex) );
				float4 _SpecMap_var = tex2D(_SpecMap, TRANSFORM_TEX(i.uv0, _SpecMap) );
				float3 _BumpMap_var = UnpackNormal( tex2D(_BumpMap, TRANSFORM_TEX(i.uv0, _BumpMap) ) );
				float3 _AoLtMt_var = tex2D(_AoLtMt, TRANSFORM_TEX(i.uv0, _AoLtMt) );