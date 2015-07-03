 
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
				float3 indirectSpecularB = indirectSpecular;
 
 				// THIS IS IMPORTANT
				// is controles the light ratio
				directSpecular *= .5;

				// Where we control the reflection by the light intensity
				float L1dotMin = saturate ( dot(L1Direction, viewReflectDirection)* _L1Intensity );
				float L2dotMin = saturate ( dot(L2Direction, viewReflectDirection)* _L2Intensity );

				float3 L1pow = _L1Color * L1dotMin;
				float3 L2pow = _L2Color * L2dotMin;
 
				indirectSpecular *= L1pow + L2pow;
				
				float3 specular =  (directSpecular + indirectSpecular) * _SpecMap_var.rgb;
				specular += indirectSpecularB *_RoomAmb * .2;

				
/////// Diffuse: 
                float3 indirectDiffuse = float3(0,0,0);

				//The Direct Lighting needs to be dropped '* 0.5' to match the level of the Light Mapped static objects
                float3 directDiffuse = lightAccumulation.rgb*.5; 


                indirectDiffuse += (marmoDiffuse(normalDirection).rgb)*2; // Diffuse Ambient Light
				indirectDiffuse *= _RoomAmb * _AoMap_var ;

                float3 diffuse = (directDiffuse + indirectDiffuse) * _MainTex_var.rgb;
                diffuse *= 1-specularMonochrome;