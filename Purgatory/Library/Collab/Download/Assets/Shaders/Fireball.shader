Shader "Unlit/Fireball"
{
	Properties
	{
		// Color property for material inspector, default to white
		_NoiseTex("Noise", 2D) = "white" {}
		_NoiseTex2("Noise2", 2D) = "white" {}
		_DistortionTex("Distortion", 2D) = "white" {}
		_Gradient("Gradient", 2D) = "white" {}
		_Color("Color1", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (0,0,0,1)
	}
		SubShader
	{
		Tags
	{
			"Queue" = "Transparent"
	}
			Blend SrcAlpha OneMinusSrcAlpha
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				float4 _OutsideColor;
				float4 _InsideFarColor;
				float4 _InsideNearColor;
				float4 _Color, _Color2;
				sampler2D _NoiseTex;
				sampler2D _NoiseTex2;
				sampler2D _DistortionTex;
				sampler2D _Gradient;

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				// vertex shader
				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				float random(float2 p) {
					return frac(sin(dot(p.xy, float2(12.9898, 78.233))) * 43758.5453123) * sin(cos(dot(12.34324, 124221312)));
				}

				float noise(float2 p)
				{
					const float K1 = 0.366025404; // (sqrt(3)-1)/2;
					const float K2 = 0.211324865; // (3-sqrt(3))/6;

					float2 i = floor(p + (p.x + p.y)*K1);

					float2 a = p - i + (i.x + i.y)*K2;
					float2 o = (a.x > a.y) ? float2(1.0, 0.0) : float2(0.0, 1.0);
					float2 b = a - o + K2;
					float2 c = a - 1.0 + 2.0*K2;

					float3 h = max(0.5 - float3(dot(a, a), dot(b, b), dot(c, c)), 0.0);

					float3 n = h * h*h*h*float3(dot(a, random(i + 0.0)), dot(b, random(i + o)), dot(c, random(i + 1.0)));

					return dot(n, float3(70.0, 70.0, 70.0));
				}

				float fbm(float2 uv)
				{
					float f;
					float2x2 m = float2x2(1.6, 1.2, -1.2, 1.6);
					f = mul(0.5000, noise(uv)); uv = mul(m, uv);
					f += mul(0.2500, noise(uv)); uv = mul(m, uv);
					f += mul(0.1250, noise(uv)); uv = mul(m, uv);
					f += mul(0.0625, noise(uv)); uv = mul(m, uv);
					f = 0.5 + 0.5 * f;
					return f;
				}

				// pixel shader
				fixed4 frag(v2f i) : SV_Target
				{	
						float2 uv = i.uv.xy - float2(0.0, 0.8);
						float2 q = float2(uv.x, (-uv.y));
						q.y *= 2.4;
						float strength = floor(4);
						float T3 = max(1,0.675*strength) * _Time.y;
						q.x = (q.x - 1.0 * floor(q.x / 1.0)) - 0.5;
						//q.x = mod(q.x,1.0) - 0.5;
						q.y -= 0.25;
						float n = fbm(strength*q - float2(0,T3));
						float c = 1 - 16 * pow(max(0, length(q*float2(1.8 + q.y*1.5,0.75)) - n * max(0, q.y + 0.25)), 1.2);
						//	float c1 = n * c * (1.5-pow(1.25*uv.y,4.));
						float c1 = n * c * (1.5 - pow(2.50*uv.y,4));
						c1 = clamp(c1,0.29,1);

						float3 col = float3(1.9*c1, 2*c1*c1*c1, c1*c1*c1*c1*c1*c1);

						float a = c * (1.0 - pow(i.uv.y, 3.0));
						float4 color = float4(lerp(float3(0, 0, 0), col, a), 1.0);

						if (color.r <= 0.3 && color.g <= 0.3 && color.b <= 0.3) {
							color = (0, 0, 0, 0);
							return color;
						}
						else {
							return color;
						}
					//float2 uv = i.uv;
					//float4 n = tex2D(_NoiseTex, (uv.x, uv.y + _Time.x));
					//float4 n2 = tex2D(_NoiseTex2, (uv.x, uv.y + _Time.x * 0.6));
					//float4 combine_n = lerp(n, n2, i.uv.y);

					//float4 Color = lerp(_Color2, _Color, i.uv.y);

					//float4 dist = tex2D(_DistortionTex, i.uv);
					//float2 uvD = (uv.x, i.uv.y - dist.r - _Time.y);

					//float4 nd = tex2D(_NoiseTex, uvD) * Color;

					//return nd;
				}
				ENDCG
			}
	}
}