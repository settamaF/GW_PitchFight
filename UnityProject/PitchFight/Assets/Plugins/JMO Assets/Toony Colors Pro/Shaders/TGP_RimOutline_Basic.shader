// Toony Colors Pro+Mobile Shaders
// (c) 2013, Jean Moreno

Shader "Toony Colors Pro/Rim Outline/MultipleLights/Basic"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		
		//COLORS
		_Color ("Highlight Color", Color) = (0.8,0.8,0.8,1)
		_SColor ("Shadow Color", Color) = (0.0,0.0,0.0,1)
		
		//RIM OUTLINE
		_RimColor ("Rim Color", Color) = (0.8,0.8,0.8,0.6)
		_RimMin ("Rim min", Range(0,1)) = 0.4
		_RimMax ("Rim max", Range(0,1)) = 0.6
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#include "TGP_Include.cginc"
		
		//nolightmap nodirlightmap		LIGHTMAP
		#pragma surface surf ToonyColors nolightmap nodirlightmap vertex:vert 
		
		sampler2D _MainTex;
		fixed4 _RimColor;
		float _RimMin;
		float _RimMax;
		
		struct Input
		{
			half2 uv_MainTex : TEXCOORD0;
			fixed3 rim;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			o.rim = 1.0f - saturate( dot(normalize(ObjSpaceViewDir(v.vertex)), v.normal) );
		}
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			
			//Rim Outline
			IN.rim = smoothstep(_RimMin, _RimMax, IN.rim);
			o.Albedo = lerp(c.rgb, _RimColor, IN.rim);
		}
		ENDCG
	}
	
	Fallback "VertexLit"
}
