// Toony Colors Pro+Mobile Shaders
// (c) 2013, Jean Moreno

Shader "Toony Colors Pro/Outline/MultipleLights/Basic"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		
		//COLORS
		_Color ("Highlight Color", Color) = (0.8,0.8,0.8,1)
		_SColor ("Shadow Color", Color) = (0.0,0.0,0.0,1)
		
		//OUTLINE
		_Outline ("Outline Width", Range(0,0.05)) = 0.005
		_OutlineColor ("Outline Color", Color) = (0.2, 0.2, 0.2, 1)
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#include "TGP_Include.cginc"
		
		//nolightmap nodirlightmap		LIGHTMAP
		#pragma surface surf ToonyColors LIGHTMAP
		
		sampler2D _MainTex;
		
		struct Input
		{
			half2 uv_MainTex : TEXCOORD0;
		};
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb;
			
		}
		ENDCG
		UsePass "Hidden/ToonyColors-Outline/OUTLINE"
	}
	
	Fallback "VertexLit"
}
