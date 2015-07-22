

            uniform sampler2D _LightBuffer;

            #if defined (SHADER_API_XBOX360) && defined (HDR_LIGHT_PREPASS_ON)
                sampler2D _LightSpecBuffer;
            #endif

            uniform fixed4 unity_Ambient;

            #ifndef LIGHTMAP_OFF
                float4 unity_LightmapST;
                sampler2D unity_Lightmap;
                sampler2D unity_LightmapInd;
                float4 unity_LightmapFade;
            #endif

			// TEXTURES
			uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
			uniform sampler2D _SpecMap; uniform float4 _SpecMap_ST;
			uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
			uniform sampler2D _AoMap;	uniform float4 _AoMap_ST;
			uniform sampler2D _CurveMap;uniform float4 _CurveMap_ST;


			// DEEP LIGHT PARAMETERS
			uniform float4 _RoomAmb;
			uniform float4 _RefAmb;

			uniform float4 _L1Pos;
			uniform float4 _L2Pos;

			uniform float _L1Intensity;
			uniform float4 _L1Color;

			uniform float _L2Intensity;
			uniform float4 _L2Color;
