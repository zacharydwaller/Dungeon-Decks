// From Unity forums, source: pedroahpolonio, Megagamefan100
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// Updated 4/26/17 by Zachary Waller to allow color, using 2X multiplicative blending with color and grabpass tex

Shader "Unlit/FrostedGlass"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Radius("Radius", Range(1, 255)) = 1
	}

	Category
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Opaque"
		}

		SubShader
		{
			GrabPass
			{
				Tags{ "LightMode" = "Always" }
			}

			Pass
			{
				Tags{ "LightMode" = "Always" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex : POSITION;
					float2 texcoord: TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex : POSITION;
					float4 uvgrab : TEXCOORD0;
				};

				v2f vert(appdata_t v)
				{
					v2f o;

					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif

					o.vertex = UnityObjectToClipPos(v.vertex);

					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					return o;
				}

				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;
				float _Radius;

				half4 frag(v2f i) : COLOR
				{
					half4 sum = half4(0,0,0,0);

					#define GRABXYPIXEL(kernelx, kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely, i.uvgrab.z, i.uvgrab.w)))

					sum += GRABXYPIXEL(0.0, 0.0);
					int measurements = 1;

					for(float range = 0.1f; range <= _Radius; range += 0.1f)
					{
						sum += GRABXYPIXEL(range, range);
						sum += GRABXYPIXEL(range, -range);
						sum += GRABXYPIXEL(-range, range);
						sum += GRABXYPIXEL(-range, -range);
						measurements += 4;
					}

					return (sum / measurements);
				}

				ENDCG
			}

			GrabPass
			{
				Tags{ "LightMode" = "Always" }
			}

			Pass
			{
				Tags{ "LightMode" = "Always" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 texcoord: TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 uvgrab : TEXCOORD0;
				};

				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;
				fixed4 _Color;
				float _Radius;

				v2f vert(appdata_t v)
				{
					v2f o;
					
					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color * _Color;

					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					return o;
				}

				half4 frag(v2f i) : COLOR
				{

					half4 sum = half4(0,0,0,0);
					float radius = 1.41421356237 * _Radius;
					fixed4 color = i.color;

					#define GRABXYPIXEL(kernelx, kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely, i.uvgrab.z, i.uvgrab.w)))

					sum += GRABXYPIXEL(0.0, 0.0);
					int measurements = 1;

					for(float range = 1.41421356237f; range <= radius * 1.41; range += 1.41421356237f)
					{
						sum += GRABXYPIXEL(range, 0);
						sum += GRABXYPIXEL(-range, 0);
						sum += GRABXYPIXEL(0, range);
						sum += GRABXYPIXEL(0, -range);
						measurements += 4;
					}

					return ((sum / measurements) * color) + (color * color.a);
				}

				ENDCG
			}
		}
	}
}