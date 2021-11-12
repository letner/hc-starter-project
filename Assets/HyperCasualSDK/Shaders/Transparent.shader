Shader "ZIPSTED/Transparent"
{
    Properties
    {
        _Color("Color", Color) = (0.5,0.5,0.5,1.0)
        _MainTex("Main Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "ForceNoShadowCasting"="True" }
        LOD 1
        CGPROGRAM
        
        #pragma surface surf Lambert decal:blend
        
        fixed4 _Color;
        sampler2D _MainTex;
        
        struct Input
        {
            half2 uv_MainTex;
        };
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = mainTex.rgb * _Color.rgb * _LightColor0.rgb;
            o.Emission = .2;
            o.Alpha = mainTex.a;
        }
        
        ENDCG
    }
    
    Fallback "Diffuse"
}
