 Shader "Unlit/GreyScale" {
 Properties {
     _MainTex ("Texture", 2D) = "white" { }
	 _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
 }
 SubShader {
     Pass {
 
 CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 
 #include "UnityCG.cginc"
 
 sampler2D _MainTex;
 half4	_Color;
 
 struct v2f {
     float4  pos : SV_POSITION;
     float2  uv : TEXCOORD0;
 };
 
 float4 _MainTex_ST;
 
 v2f vert (appdata_base v)
 {
     v2f o;
     o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
     o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
     return o;
 }
 
 half4 frag (v2f i) : COLOR
 {
     half4 texcol = tex2D (_MainTex, i.uv);
	 if (texcol.a <= 0.0)
		return _Color;
	 else
	 {
	 	float lMask = (texcol.r + texcol.g + texcol.b) / 3.0;
		return half4(lMask, lMask, lMask, 1.0);
	 }
 }
 ENDCG
 
     }
 }
 Fallback "VertexLit"
 } 