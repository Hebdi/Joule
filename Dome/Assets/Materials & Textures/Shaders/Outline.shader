Shader "Custom/OutlineOnly"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1) // Color of the outline
        _OutlineThickness ("Outline Thickness", Float) = 0.02 // Thickness of the outline
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" } // Draw on top of everything
        Pass
        {
            Name "Outline"
            ZWrite On
            ZTest LEqual
            Cull Front // Render backfaces to create an outline

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            float _OutlineThickness;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;

                // Offset the vertices along their normals to create an outline effect
                float3 offset = normalize(v.normal) * _OutlineThickness;
                o.pos = UnityObjectToClipPos(v.vertex + float4(offset, 0.0));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Return the outline color
                return _OutlineColor;
            }
            ENDCG
        }
    }
    Fallback Off
}
