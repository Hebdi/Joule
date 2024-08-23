Shader "Custom/EmissionBlend"
{
    Properties
    {
        _EmissionColor1 ("Emission Color 1", Color) = (1,1,1,1)
        _EmissionColor2 ("Emission Color 2", Color) = (1,1,1,1)
        _BlendFactor ("Blend Factor", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
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

            float4 _EmissionColor1;
            float4 _EmissionColor2;
            float _BlendFactor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Blend between emission colors
                half4 blendedEmissionColor = lerp(_EmissionColor1, _EmissionColor2, _BlendFactor);

                // Output the blended emission color
                return blendedEmissionColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
