
		/////// SPECULAR:
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;

				// Full Fresnel 
				//float3 specularColor = (_SpecMap_var.rgb+(1.0-max(0,dot(normalDirection, viewDirection))));

                float specularMonochrome = dot(_SpecMap_var.rgb, float3(0.3,0.59,0.11));






				// I THINK THIS IS PHONG ?????
				//The alpha must be increased for the mirrors to produce a more heated reflection
                float specPow = max( 2, _SpecMap_var.a * 128 ) ;
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = (lightAccumulation.rgb * 2) * lightAccumulation.a * normTerm;


				// SKYSHOP REFLECTIONS
				float3 indirectSpecular =  marmoMipSpecular(viewReflectDirection, i.posWorld.rgb, _SpecMap_var.a).rgb;

				// THIS IS IMPORTANT
				// is controles the light ratio
				directSpecular *= .1;



				// Where we control the reflection by the light intensity
				float3 L1pow = _L1Color * (dot(L1Direction, viewReflectDirection) * _L1Intensity);
				float3 L2pow = _L2Color * (dot(L2Direction, viewReflectDirection) * _L2Intensity);

				//.03125 * band + .1
				/*Look up table
				float linBand = 1-(.03125 * 2 +.1);
				float L1dot =  dot(L1Direction, viewReflectDirection);
				float2 L1uv = float2(L1dot,linBand);
				float L1RemapLin = tex2D(_CurveMap,TRANSFORM_TEX(L1uv, _CurveMap)).r;
				*/


				// Reflection is multiplied by the lights/dir for face RT lighting in Ref
				indirectSpecular *= L1pow + L2pow + _RoomAmb;


				//indirectSpecular += _RoomAmb;

				// LIGHTMAPPING IS ON
				#ifndef LIGHTMAP_OFF 
					// THIS IS MULTIPLIED TIMES THE AMBIENT LEVEL SO REFLETIONS MATCH AMBIENT LIGHTING
					indirectSpecular += lightmapAccumulation.rgb * _RefAmb;
				#endif

				// Filter the reflections with the lightmap
				#ifndef LIGHTMAP_OFF // LIGHTMAPPING IS ON
					indirectSpecular *= lightmapAccumulation.rgb * _AoMap_var ;
				#endif


				// TOTAL SPECULAR
                float3 specular = (directSpecular + indirectSpecular) * _SpecMap_var.rgb ;

				/*
                #ifndef LIGHTMAP_OFF // LIGHTMAPPING IS ON
                    #ifndef DIRLIGHTMAP_OFF
                        specular += specColor;
                    #endif
                #endif
				*/

		/////// DIFFUSE:
                float3 indirectDiffuse = float3(0,0,0);

				// LIGHTMAPPING IS ON
                #ifndef LIGHTMAP_OFF 
                    float3 directDiffuse = float3(0,0,0);
                #else // LIGHTMAPPING IS OFF
                    float3 directDiffuse = lightAccumulation.rgb * 0.5;
                #endif

				
                #ifndef LIGHTMAP_OFF // LIGHTMAPPING IS ON
					 lightAccumulation.rgb + lightmapAccumulation.rgb;

					/* GI BASED ON DISTANCE (Removed in preference to location global GI)
					float clampNode = clamp((1.0 - (distance(_L1Pos.rgb,i.posWorld.rgb)/_L1Falloff)),0,1);
					directDiffuse += (lightAccumulation.rgb * lightmapAccumulation.rgb) + ( lightmapAccumulation.rgb * (clampNode * _L1Intensity)) ;
					*/

					// BASED IN SINGLE AMBIENT VALUE
					directDiffuse += lightAccumulation.rgb * lightmapAccumulation.rgb  ;	//Multi lighting by the lightmap
					directDiffuse += lightmapAccumulation.rgb * _RoomAmb * _AoMap_var ;		// Add light map the ambien amount and mult by local AOmap

                #endif

				float3 diffuse = directDiffuse * _MainTex_var.rgb ;

				// This is the function that determins how much diffuse based on reflection
                diffuse *= 1-specularMonochrome;