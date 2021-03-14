Shader "Starfighter Game Template/LegacyPlanetShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal map", 2D) = "bump" {}
		_SpecMap ("Specular map", 2D) = "black" {}
		_CloudMap ("Cloud map", 2D) = "black" {}
      	_Detail ("Detail", 2D) = "gray" {}
      	_Glossiness ("Glossiness", Range(0,1)) = 0.5
      	_RimColor ("Atmosphere Color", Color) = (0.26,0.19,0.16,0.0)
      	_RimPower ("Atmosphere Power", Range(0.5,8.0)) = 3.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
     	sampler2D _Detail;
     	sampler2D _SpecMap;
     	sampler2D _CloudMap;
     	float4 _RimColor;
      	float _RimPower;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_SpecMap;
			float2 uv_CloudMap;
          	float2 uv_Detail;
          	float3 viewDir;
		};

		half _Glossiness;
		fixed4 _Color;
		fixed4 _CloudColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Albedo *= tex2D (_Detail, IN.uv_Detail).rgb * 2;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			fixed4 specTex = tex2D(_SpecMap, IN.uv_SpecMap);
			o.Smoothness = _Glossiness * specTex.g;
			fixed4 cl = tex2D (_CloudMap, IN.uv_CloudMap);
			o.Albedo += cl.rgb * cl.a;
			o.Alpha = c.a;
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          	o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		}

		ENDCG
	}

	FallBack "Diffuse"
}
