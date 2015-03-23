// Toony Colors Pro+Mobile 2
// (c) 2014,2015 Jean Moreno


Shader "Toony Colors Pro 2/User/MobileBasicTCP2LightmapOutline"
{
	Properties
	{
		//TOONY COLORS
		_Color ("Color", Color) = (0.5,0.5,0.5,1.0)
		_HColor ("Highlight Color", Color) = (0.6,0.6,0.6,1.0)
		_SColor ("Shadow Color", Color) = (0.3,0.3,0.3,1.0)
		
		//DIFFUSE
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
		
		//TOONY COLORS RAMP
		_Ramp ("#RAMPT# Toon Ramp (RGB)", 2D) = "gray" {}
		
		//OUTLINE
		_OutlineColor ("#OUTLINE# Outline Color", Color) = (0.2, 0.2, 0.2, 1.0)
		_Outline ("#OUTLINE# Outline Width", Float) = 1
		
		//Outline Textured
		_TexLod ("#OUTLINETEX# Texture LOD", Range(0,10)) = 5
		
		//ZSmooth
		_ZSmooth ("#OUTLINEZ# Z Correction", Range(-3.0,3.0)) = -0.5
		
		//Z Offset
		_Offset1 ("#OUTLINEZ# Z Offset 1", Float) = 0
		_Offset2 ("#OUTLINEZ# Z Offset 2", Float) = 0
		
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		
		#pragma surface surf ToonyColorsCustom 
		#pragma target 3.0
		#pragma glsl
		
		#pragma shader_feature TCP2_RAMPTEXT
		
		//================================================================
		// VARIABLES
		
		fixed4 _Color;
		sampler2D _MainTex;
		
		
		struct Input
		{
			half2 uv_MainTex;
		};
		
		//================================================================
		// CUSTOM LIGHTING
		
		//Lighting-related variables
		fixed4 _HColor;
		fixed4 _SColor;
		sampler2D _Ramp;
		
		//Custom SurfaceOutput
		struct SurfaceOutputCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			half Specular;
			fixed Gloss;
			fixed Alpha;
		};
		
		inline half4 LightingToonyColorsCustom (SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten)
		{
			s.Normal = normalize(s.Normal);
			fixed ndl = max(0, dot(s.Normal, lightDir)*0.5 + 0.5);
			
			fixed3 ramp = tex2D(_Ramp, fixed2(ndl,ndl));
		#if !(POINT) && !(SPOT)
			ramp *= atten;
		#endif
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb,_HColor.rgb,ramp);
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * 2;
			c.a = s.Alpha;
		#if (POINT || SPOT)
			c.rgb *= atten;
		#endif
			return c;
		}
		
		inline half4 LightingToonyColorsCustom_SingleLightmap (SurfaceOutputCustom s, fixed4 color)
		{
			half3 lm = DecodeLightmap (color);
			
			float lum = Luminance(lm);
		#if TCP2_RAMPTEXT
			fixed3 ramp = tex2D(_Ramp, fixed2(lum,lum));
		#else
			fixed3 ramp = smoothstep(_RampThreshold-_RampSmooth*0.5, _RampThreshold+_RampSmooth*0.5, lum);
		#endif
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb,_HColor.rgb,ramp);
			lm *= ramp * 2;
			
			return fixed4(lm, 0);
		}
		
		inline fixed4 LightingToonyColorsCustom_DualLightmap (SurfaceOutput s, fixed4 totalColor, fixed4 indirectOnlyColor, half indirectFade)
		{
			half3 lm = lerp (DecodeLightmap (indirectOnlyColor), DecodeLightmap (totalColor), indirectFade);
			
			float lum = Luminance(lm);
		#if TCP2_RAMPTEXT
			fixed3 ramp = tex2D(_Ramp, fixed2(lum,lum));
		#else
			fixed3 ramp = smoothstep(_RampThreshold-_RampSmooth*0.5, _RampThreshold+_RampSmooth*0.5, lum);
		#endif
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb,_HColor.rgb,ramp);
			lm *= ramp * 2;
			
			return fixed4(lm, 0);
		}
		
		inline fixed4 LightingToonyColorsCustom_DirLightmap (SurfaceOutputCustom s, fixed4 color, fixed4 scale, bool surfFuncWritesNormal)
		{
			UNITY_DIRBASIS
			half3 scalePerBasisVector;
			
			half3 lm = DirLightmapDiffuse (unity_DirBasis, color, scale, s.Normal, surfFuncWritesNormal, scalePerBasisVector);
			
		
			float lum = Luminance(lm);
		#if TCP2_RAMPTEXT
			fixed3 ramp = tex2D(_Ramp, fixed2(lum,lum));
		#else
			fixed3 ramp = smoothstep(_RampThreshold-_RampSmooth*0.5, _RampThreshold+_RampSmooth*0.5, lum);
		#endif
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb,_HColor.rgb,ramp);
			lm *= ramp * 2;
			
			return half4(lm, 0);
		}
		
		//================================================================
		// SURFACE FUNCTION
		
		void surf (Input IN, inout SurfaceOutputCustom o)
		{
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = mainTex.rgb * _Color.rgb;
			o.Alpha = mainTex.a * _Color.a;
			
		}
		
		ENDCG
		//Outlines
		UsePass "Hidden/Toony Colors Pro 2/Outline Only/OUTLINE"
	}
	
	Fallback "Diffuse"
	CustomEditor "TCP2_MaterialInspector"
}
