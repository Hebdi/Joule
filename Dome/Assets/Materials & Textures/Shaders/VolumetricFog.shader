Shader "Custom/VolumetricFog"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (0.5, 0.5, 0.5, 1)
        _FogDensity ("Fog Density", Float) = 0.1
        _FogStart ("Fog Start Distance", Float) = 0.0
        _FogEnd ("Fog End Distance", Float) = 100.0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _FogColor;
            float _FogDensity;
            float _FogStart;
            float _FogEnd;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float distance = length(i.worldPos - _WorldSpaceCameraPos);
                float fogFactor = saturate((distance - _FogStart) / (_FogEnd - _FogStart));
                fogFactor = pow(fogFactor, _FogDensity);
                fixed4 color = _FogColor * fogFactor;
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
