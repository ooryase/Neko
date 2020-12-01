// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UncertainObject" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_NormalMap("Normal map", 2D) = "bump" {}
		_Shininess("Shininess", Range(0.0, 1.0)) = 0.078125
		_NoiseX("NoiseX", Range(0, 1)) = 0
		_Offset("Offset", Vector) = (0, 0, 0, 0)
		_RGBNoise("RGBNoise", Range(0, 1)) = 0
		_SinNoiseWidth("SineNoiseWidth", Float) = 1
		_SinNoiseScale("SinNoiseScale", Float) = 1
		_SinNoiseOffset("SinNoiseOffset", Float) = 1
	}
		SubShader{

			Tags { "Queue" = "Geometry" "RenderType" = "Opaque"}

			Pass {
				Tags { "LightMode" = "ForwardBase" }

				CGPROGRAM
				#include "UnityCG.cginc"

				#pragma vertex vert
				#pragma fragment frag

				float4 _LightColor0;
				sampler2D _MainTex;
				sampler2D _NormalMap;
				half _Shininess;
				fixed4 _Color;
				float _NoiseX;
				float2 _Offset;
				float _RGBNoise;
				float _SinNoiseWidth;
				float _SinNoiseScale;
				float _SinNoiseOffset;

				struct appdata {
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
					// 頂点の法線と接線の情報を取得できるようにする
					float3 normal : NORMAL;
					float4 tangent : TANGENT;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					half2 uv : TEXCOORD0;
					half3 lightDir : TEXCOORD1;
					half3 viewDir : TEXCOORD2;
				};

				float rand(float2 co) {
					return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
				}

				float2 mod(float2 a, float2 b)
				{
					return a - floor(a / b) * b;
				}

				v2f vert(appdata v) {
					v2f o;

					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord.xy;

					// 接空間におけるライト方向のベクトルと視点方向のベクトルを求める
					TANGENT_SPACE_ROTATION;
					o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex));
					o.viewDir = mul(rotation, ObjSpaceViewDir(v.vertex));

					return o;
				}

				float4 frag(v2f i) : COLOR {
					i.lightDir = normalize(i.lightDir);
					i.viewDir = normalize(i.viewDir);
					half3 halfDir = normalize(i.lightDir + i.viewDir);


					// 色を計算
					float3 col;

					float2 texUV = i.uv;

					// ノイズ、オフセットを適用
					texUV.x += sin(texUV.y * _SinNoiseWidth + _SinNoiseOffset) * _SinNoiseScale;
					texUV += _Offset;
					texUV.x += (rand(floor(texUV.y * 500) + _Time.y) - 0.5) * _NoiseX;
					texUV = mod(texUV, 1);

					// 色を取得、RGBを少しずつずらす
					col.r = tex2D(_MainTex, texUV).r;
					col.g = tex2D(_MainTex, texUV - float2(0.002, 0)).g;
					col.b = tex2D(_MainTex, texUV - float2(0.004, 0)).b;

					// RGBノイズ
					if (rand((rand(floor(texUV.y * 500) + _Time.y) - 0.5) + _Time.y) < _RGBNoise)
					{
						col.r = rand(i.uv + float2(123 + _Time.y, 0));
						col.g = rand(i.uv + float2(123 + _Time.y, 1));
						col.b = rand(i.uv + float2(123 + _Time.y, 2));
					}

					half4 tex = tex2D(_MainTex, texUV) * _Color;

					// ノーマルマップから法線情報を取得する
					half3 normal = UnpackNormal(tex2D(_NormalMap, texUV));

					// ノーマルマップから得た法線情報をつかってライティング計算をする
					half4 diff = saturate(dot(normal, i.lightDir)) * _LightColor0;
					half3 spec = pow(max(0, dot(normal, halfDir)), _Shininess * 12.80) * _LightColor0.rgb * tex.rgb;

					fixed4 color;
					color.rgb = tex.rgb * diff + spec;
					return color;
				}

				ENDCG
			}
		   }
		FallBack "Diffuse"
}