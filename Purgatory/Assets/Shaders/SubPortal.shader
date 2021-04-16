Shader "Unlit/SubPortal"
{
	Properties
	{
	_MainTex("Inner Texture", 2D) = "white" {}
	_Noise("NoiseTexture", 2D) = "white" {}
	_SpeedValue("Speed", float) = 0
	_DistortValue("Distortion", float) = 5
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex, _Noise;
			float _SpeedValue;
			float _DistortValue;

			struct appdata
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = v.uv;
				return o;
			}

			float2 rotate(float rotation, float2 p)
			{
				float s = sin(rotation);
				float c = cos(rotation);
				float2x2 rotationMatrix = float2x2(c, -s, s, c);
				return mul(p, rotationMatrix);
			}

			float4 frag(v2f i) : SV_Target
			{
				float4 noise = tex2D(_Noise, i.uv);

				float2 p = i.uv - (0.5, 0.5);

				p = rotate((_DistortValue * noise), p);

				float angle = atan2(p.y, p.x) * 0.5 / 3.1416;
				float dist = sqrt(pow(p.x, 2) + pow(p.y, 2));

				float2 uv;
				uv.x = (_Time.x * _SpeedValue) + 1 / (dist + 1);
				uv.y = angle;


				float4 c = tex2D(_MainTex, uv);

				return c;
				
			}
			ENDCG
		}
	}
}
