







			// Custom for Parallax based on the AO.a(height map)
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float4 _HeightMap_var = tex2D(_AoMap, TRANSFORM_TEX(i.uv0, _AoMap) );
                float2 parallaxUV = (_ParalaxAmt*(_HeightMap_var.a - .75)*mul(tangentTransform, viewDirection).xy + i.uv0);
				float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(parallaxUV.rg, _BumpMap)));

				
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _SpecMap_var = tex2D(_SpecMap,TRANSFORM_TEX(i.uv0, _SpecMap));
                return fixed4( normalDirection * 0.5 + 0.5, max(_SpecMap_var.a,0.0078125) );
            }