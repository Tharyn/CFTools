Shader "DMEffects/FadeToColor" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "MainTex" {}
		_SolidColor ("Solid Color", Color) = (1, .5, 1, 1) // color
	}

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
				
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform sampler2D _RampTex;
		uniform half _RampOffset;
		uniform float4  _SolidColor;

		fixed4 frag (v2f_img i) : SV_Target
		{
			fixed4 original = tex2D(_MainTex, i.uv);
			fixed4 fade = original * _SolidColor;
			fade.a = original.a;
			return fade;
		}
		ENDCG

		}
	}

	Fallback off
}
