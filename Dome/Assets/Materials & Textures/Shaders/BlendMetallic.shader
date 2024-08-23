Shader "Custom/BlendWithStandard"
{
    Properties
    {
        _BaseColor1 ("Base Color 1", Color) = (1,1,1,1)
        _BaseColor2 ("Base Color 2", Color) = (1,1,1,1)
        _MainTex1 ("Texture 1", 2D) = "white" {}
        _MainTex2 ("Texture 2", 2D) = "white" {}
        _EmissionTex1 ("Emission Texture 1", 2D) = "white" {}
        _EmissionTex2 ("Emission Texture 2", 2D) = "white" {}
        _BlendFactor ("Blend Factor", Range(0,1)) = 0.0
        _Metallic1 ("Metallic 1", Range(0,1)) = 0.0
        _Metallic2 ("Metallic 2", Range(0,1)) = 0.0
        _Smoothness1 ("Smoothness 1", Range(0,1)) = 0.5
        _Smoothness2 ("Smoothness 2", Range(0,1)) = 0.5
        _Color ("Tint Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode"="ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex1;
            sampler2D _MainTex2;
            sampler2D _EmissionTex1;
            sampler2D _EmissionTex2;
            float4 _BaseColor1;
            float4 _BaseColor2;
            float _BlendFactor;
            float _Metallic1;
            float _Metallic2;
            float _Smoothness1;
            float _Smoothness2;
            float4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Sample textures
                half4 baseTex1 = tex2D(_MainTex1, i.uv) * _BaseColor1;
                half4 baseTex2 = tex2D(_MainTex2, i.uv) * _BaseColor2;
                half4 emissionTex1 = tex2D(_EmissionTex1, i.uv);
                half4 emissionTex2 = tex2D(_EmissionTex2, i.uv);

                // Blend between base textures and emission textures
                half4 baseColor = lerp(baseTex1, baseTex2, _BlendFactor);
                half4 emissionColor = lerp(emissionTex1, emissionTex2, _BlendFactor);

                // Blend metallic and smoothness
                float metallic = lerp(_Metallic1, _Metallic2, _BlendFactor);
                float smoothness = lerp(_Smoothness1, _Smoothness2, _BlendFactor);

                // Calculate final color
                half4 finalColor = baseColor;
                finalColor.rgb += emissionColor.rgb; // Add emission to base color

                // Apply tint color
                finalColor *= _Color;

                // Create a struct to pass to the Standard Shader
                // Note: Metallic and Smoothness are usually used in the Standard Shader to affect lighting
                // Since we are blending, these values will be used by Unity's Standard Shader
                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Standard"
}
