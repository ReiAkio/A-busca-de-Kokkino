Shader "BackgroundSpriteShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_Scale ("Speed Scale " , float) = 1
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
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct Input
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct Interpolator
			{
				float4 vertex   : SV_POSITION; // posição do vertex
				fixed4 color    : COLOR; 
				float2 texcoord  : TEXCOORD0;  // uv
				
				float2 worldPos : TEXCOORD1; // coordenada central do modelo
			};
			
			fixed4 _Color;
			float _Scale;
		
			Interpolator vert(Input IN)
			{
				Interpolator OUT;

				OUT.worldPos = mul(UNITY_MATRIX_M,float4(0,0,0,1)).xy; // calula a coordenada central do modelo
				
				OUT.vertex = UnityObjectToClipPos(IN.vertex);// faz as transformações matriciais para renderizar o vertex
				OUT.texcoord = IN.texcoord; // passa o uv para o fragment shader
				OUT.color = IN.color * _Color; // calcula a cor para passar para o fragment shader

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv) // retorna a cor do pixel da posição passada
			{
				fixed4 color = tex2D (_MainTex, uv);

				#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
				#endif 

				return color;
			}

			fixed4 frag(Interpolator IN) : SV_Target
			{
				float2 xOffset = IN.worldPos / _Scale; // calcula o quanto é o offset da textura que vai pegar
				fixed4 c = SampleSpriteTexture (IN.texcoord+xOffset) * IN.color; // pega a textura com o offset e mistura com a cor
				c.rgb *= c.a; // normaliza e deixa as cores proporcionais
				return c; // retorna a cor de um pixel
			}
		ENDCG
		}
	}
}