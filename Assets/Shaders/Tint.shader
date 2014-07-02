Shader "Dash/Tint"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_SunTint ("Sun Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Cutoff ("Alpha Cutoff", Range (0,1)) = 0.5

	}
	SubShader
	{
		Tags
		{ 
//		"RenderType" = "Opaque" 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="TransparentCutOut" 
			"PreviewType"="Plane"
//			"CanUseSpriteAtlas"="True"
		}
//		
		LOD 300
//		
		Cull Off
		Lighting On
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
		//vertex:vert
		//
			sampler2D _MainTex;
//			sampler2D _BumpMap;
			fixed4 _SunTint;
			fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
//				float2 uv_BumpMap;
				fixed4 color;
			};

//			void vert (inout appdata_full v, out Input o)
//			{
//				#if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
//				v.vertex = UnityPixelSnap (v.vertex);
//				#endif
//				v.normal = float3(0,0,-1);
//				v.tangent =  float4(1, 0, 0, 1);
//				
//				UNITY_INITIALIZE_OUTPUT(Input, o);
//				o.color = _Color;
//			}

			void surf (Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			   
				float isColorToReplace = (c.g >= 0.1) && (c.b <= 0.1);
//				float isColorToReplace = abs(1 - (c.r / c.g / c.b)) <= .8;
				c.rgb = ((1 - isColorToReplace) * (5 * c.rgb)) + (isColorToReplace * (5 * c.g * _SunTint));
//				c.a = ((1 - isColorToReplace) * c.a) + (isColorToReplace);
			   
//				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		
			    o.Albedo = c.rgb;
			    o.Alpha = c.a;
			}

//		void surf (Input IN, inout SurfaceOutput o) {
//      		o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
//     	}
		ENDCG
	}
	Fallback "Diffuse"
}
