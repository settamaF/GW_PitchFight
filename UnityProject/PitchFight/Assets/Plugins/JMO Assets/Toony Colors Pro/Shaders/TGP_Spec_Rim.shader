// Toony Colors Pro+Mobile Shaders
// (c) 2013, Jean Moreno

Shader "Toony Colors Pro/Normal/MultipleLights/Specular Rim"
{
	Properties
	{
		_MainTex ("Base (RGB) Gloss (A) ", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		
		//SPECULAR
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		
		//COLORS
		_Color ("Highlight Color", Color) = (0.8,0.8,0.8,1)
		_SColor ("Shadow Color", Color) = (0.0,0.0,0.0,1)
		
		//RIM LIGHT
		_RimColor ("Rim Color", Color) = (0.8,0.8,0.8,0.6)
		_RimPower ("Rim Power", Range(-2,10)) = 0.5
		
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#include "TGP_Include.cginc"
		
		//nolightmap nodirlightmap		LIGHTMAP
		//approxview halfasview			SPECULAR/VIEW DIR
		#pragma surface surf ToonyColorsSpec nolightmap nodirlightmap vertex:vert approxview halfasview 
		
		sampler2D _MainTex;
		float _RimPower;
		fixed4 _RimColor;
		fixed _Shininess;
		
		struct Input
		{
			half2 uv_MainTex : TEXCOORD0;
			fixed3 rim;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			half rim = 1.0f - saturate( dot(normalize(ObjSpaceViewDir(v.vertex)), v.normal) );
			o.rim = (_RimColor.rgb * pow(rim, _RimPower)) * _RimColor.a;
		}
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb;
			
			//Specular
			o.Gloss = c.a;
			o.Specular = _Shininess;
			//Rim Light
			o.Emission = IN.rim;
		}
		ENDCG
	}
	
	Fallback "Toony Colors Pro/Normal/MultipleLights/Basic Rim"
}
