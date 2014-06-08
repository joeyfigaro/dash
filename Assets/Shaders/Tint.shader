Shader "Custom/Tint" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
//		_MainTex ("Greyscale (R) Alpha (A)", 2D) = "white" {}
//        _ColorRamp ("Colour Palette", 2D) = "gray" {}
	}
	SubShader {
		Pass {
			CGPROGRAM
                sampler2D _MainTex;
            ENDCG
		}
		void surf (Input IN, inout SurfaceOutput o) {
		    const float3 ColorToReplace = float3(1,1,1); // green
		    const float3 NewColor = float3(1,0,1); // magenta
		   
		    // read pixel from texture
		    half4 c = tex2D (_MainTex, IN.uv_MainTex);
		   
		    // check how much ColorToReplace equals c.rgb in range 0..1
		    // 0=no match, 1=full match
		    float weight = saturate(dot(c.rgb, ColorToReplace));
		   
		    // blend between both colors
		    c.rgb = lerp(c.rgb, NewColor, weight);
		   
		    o.Albedo = c.rgb;
		    o.Alpha = c.a;
		}
	}
//	SubShader {
//        Pass {
//            Name "ColorReplacement"
//           
//            CGPROGRAM
//                #pragma vertex vert
//                #pragma fragment frag
//                #pragma fragmentoption ARB_precision_hint_fastest
//                #include "UnityCG.cginc"
//               
//                struct v2f
//                {
//                    float4  pos : SV_POSITION;
//                    float2  uv : TEXCOORD0;
//                };
// 
//                v2f vert (appdata_tan v)
//                {
//                    v2f o;
//                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//                    o.uv = v.texcoord.xy;
//                    return o;
//                }
//               
//                sampler2D _MainTex;
//                sampler2D _ColorRamp;
// 
//                float4 frag(v2f i) : COLOR
//                {
//                // SURFACE COLOUR
//                    float greyscale = tex2D(_MainTex, i.uv).r;
//               
//                // RESULT
//                    float4 result;
//                    result.rgb = tex2D(_ColorRamp, float2(greyscale, 0.5)).rgb;
//                    result.a = tex2D(_MainTex, i.uv).a;
//                    return result;
//                }
//            ENDCG
//        }
//    }
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200
//		
//		CGPROGRAM
//		#pragma surface surf Lambert
//
//		sampler2D _MainTex;
//
//		struct Input {
//			float2 uv_MainTex;
//		};
//
//		void surf (Input IN, inout SurfaceOutput o) {
//			half4 c = tex2D (_MainTex, IN.uv_MainTex);
//			o.Albedo = c.rgb;
//			o.Alpha = c.a;
//		}
//		ENDCG
//	} 
//	FallBack "Diffuse"
}
