Shader "ZIPSTED/Regular"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _HighlightColor("Highlight Color", Color) = (0.6,0.6,0.6,1.0)
        _ShadowColor("Shadow Color", Color) = (0.3,0.3,0.3,1.0)
        
        _MainTex("Main Texture", 2D) = "white" {}
        _Ramp("Shadow Ramp", 2D) = "gray" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        CGPROGRAM
        
        #pragma surface surf CustomLight fullforwardshadows
        
        fixed4 _Color;
        sampler2D _MainTex;
        
        struct Input
        {
            half2 uv_MainTex;
        };
        
        fixed4 _HighlightColor;
        fixed4 _ShadowColor;
        sampler2D _Ramp;
        
        struct CustomOutput
        {
            fixed3 Albedo;
            fixed3 Normal;
            fixed3 Emission;
            half Specular;
            fixed Alpha;
        };
        
        inline half4 LightingCustomLight(CustomOutput s, half3 lightDir, half3 viewDir, half atten)
        {
            s.Normal = normalize(s.Normal);
            fixed ndl = max(0, dot(s.Normal, lightDir) * 0.5 + 0.5);
            
            fixed3 ramp = tex2D(_Ramp, fixed2(ndl,ndl));
            ramp *= atten;
            _ShadowColor = lerp(_HighlightColor, _ShadowColor, _ShadowColor.a);
            ramp = lerp(_ShadowColor.rgb,_HighlightColor.rgb,ramp);
            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * ramp;
            c.a = s.Alpha;
            return c;
        }
        
        void surf(Input IN, inout CustomOutput o)
        {
            fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = mainTex.rgb * _Color.rgb;
            o.Alpha = mainTex.a * _Color.a;
        }
        
        ENDCG
    }
    
    Fallback "Diffuse"
}
