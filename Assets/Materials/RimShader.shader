Shader "Randall/new" {

	Properties {

		_MainTxt texture;
	}

	Subshader {

		Pass {

			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma vertex vert;
			#pragma fragment frag;


			struct vertexInput {

				float4 vertex : POSITION;
				float4 normal : NORMAL;
			}

			struct vertexOutput {

				float4 svPos : SV_POSITION;
			}

			vertexOutput vert (vertextInput i) {

			vertexOutput o;
			o.vertext = i.vertex;

			return o;
			}

			float4 frag (vertextOutput o) : COLOR {

				
			}

			ENDCG

		}
	}
}