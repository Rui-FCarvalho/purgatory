// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Portal 1"
{
    Properties
    {
	_MainTex("Texture", 2D) = "white" {}
	_MotionTex("Texture", 2D) = "white" {}
	_Speed("Speed", float) = 0
	_Pivot("", Vector) = (0.5, 0.5, 1.0, 1.0)
	_Swirl("Swirl", float) = 5
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex, _MotionTex;
			float4 _Pivot;
			float _Speed;
			float _Swirl;

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

			//Rotate stuff, found a lot online
			float2 rotate(float magnitude, float2 p)
			{
				float sinTheta = sin(magnitude);
				float cosTheta = cos(magnitude);
				float2x2 rotationMatrix = float2x2(cosTheta, -sinTheta, sinTheta, cosTheta);
				return mul(p, rotationMatrix);
			}

            float4 frag (v2f i) : SV_Target
            {
				float4 motion = tex2D(_MotionTex, i.uv);

				float2 p = i.uv - _Pivot.xy;
				p = rotate(_Swirl * (motion.r * _Time), p);

				float a = atan2(p.y, p.x) * 0.5;
				float r = sqrt(dot(p, p));
				float2 uv;

				uv.x = (_Time * _Speed) - 1 / (r + 1);
				uv.y = _Pivot.z * a / 3.1416;

				float4 c = tex2D(_MainTex, uv);

				return c;
            }

            ENDCG
        }
    }
}
