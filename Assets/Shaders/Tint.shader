﻿Shader "Dash/Tint"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_SunTint ("Sun Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}
	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha
		
//		Pass
//		{
//		CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			#pragma multi_compile DUMMY PIXELSNAP_ON
//			#include "UnityCG.cginc"
//			
//			struct appdata_t
//			{
//				float4 vertex   : POSITION;
//				float4 color    : COLOR;
//				float2 texcoord : TEXCOORD0;
//			};
//
//			struct v2f
//			{
//				float4 vertex   : SV_POSITION;
//				fixed4 color    : COLOR;
//				half2 texcoord  : TEXCOORD0;
//			};
//			
//			sampler2D _MainTex;
//			fixed4 _Color;
//			fixed4 _SunTint;
//
//			v2f vert(appdata_t IN)
//			{
//				v2f OUT;
//				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
//				OUT.texcoord = IN.texcoord;
////				OUT.color = IN.color * _Color;
//				OUT.color = IN.color * _SunTint;
//				
//				#ifdef PIXELSNAP_ON
//				OUT.vertex = UnityPixelSnap (OUT.vertex);
//				#endif
//
//				return OUT;
//			}
//
//
//			fixed4 frag(v2f IN) : SV_Target
//			{
//				fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
//				c.rgb *= c.a;
//				return c;
//			}
//		ENDCG
//		}
		
		CGPROGRAM
		#pragma surface surf Lambert alpha
			sampler2D _MainTex;
			fixed4 _SunTint;

			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float3 viewDir;
			};

			void surf (Input IN, inout SurfaceOutput o) {
				half4 c = tex2D (_MainTex, IN.uv_MainTex);
			   
				float isColorToReplace = (c.r * c.g * c.b) == 1;
				c.rgb = ((1 - isColorToReplace) * c.rgb) + (isColorToReplace * (c.rgb * _SunTint));
			   
			    o.Albedo = c.rgb;
			    o.Alpha = c.a;
			}
		ENDCG
	}
}