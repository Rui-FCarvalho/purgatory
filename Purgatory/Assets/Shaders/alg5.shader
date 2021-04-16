Shader "tp2/alg5"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			uniform sampler2D _MainTex;

			float rand(float2 co){
			    return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
			}
			
			float rand(float c){
				return rand(float2(c,1.0));
			}
			


			float4 frag (v2f i) : SV_TARGET
			{
				
				float frequency = 15.0f;   						//calcula a frequencia de tipo tudo
				float t = float(int(_Time.y * frequency));


				float2 uv = i.uv;
		

				float2 suv = uv + 0.002 * float2( rand(t), rand(t + 23.0));	 //shake
				float3 image = tex2D(_MainTex, float2(suv.x, suv.y)).xyz; 		 		//imagem com shake
				

				float luma = dot( float3(0.5, 0.5, 0.5), image ); 				 //, controlas a luminosidade
				float3 oldImage = luma * float3(1, 0.0, 0.0); 						 //aplicar filtro cinzento,    controlar a cor

				float vI =  16.0 * (uv.x * (1.0-uv.x) * uv.y * (1.0-uv.y));  //luminosidade

				
				
				//vI += 1.0 + 0.4 * rand(t+8.);// luz central
				
				
	

        		float3 col =  oldImage /**   vI*/; 

        		return float4(col, 1.0);
				
			}

			
			ENDCG
		}
	}
}
