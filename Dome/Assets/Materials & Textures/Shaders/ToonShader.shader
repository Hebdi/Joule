Shader "Custom/ToonShader_Smooth_v7"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}                    // Main texture
        _Color ("Main Color", Color) = (1,1,1,1)                 // Object's main color
        _ShadowColor ("Shadow Color", Color) = (0.2,0.2,0.2,1)   // Color of shadows
        _Cutoff ("Shadow Cutoff", Range(0,1)) = 0.5              // Cutoff value for shadow transition
        _EdgeDetectionThreshold ("Edge Detection Threshold", Range(0,1)) = 0.2 // Edge detection threshold
        _LightColor ("Light Color", Color) = (1,1,1,1)           // Light color for shading
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

            sampler2D _MainTex;
            float4 _Color;
            float4 _ShadowColor;
            float _Cutoff;
            float _EdgeDetectionThreshold;
            float4 _LightColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the main texture
                fixed4 texColor = tex2D(_MainTex, i.uv);
                
                // Compute light direction and intensity
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float NdotL = max(0, dot(i.worldNormal, lightDir));
                
                // Average normal for smoother shading
                float3 averagedNormal = normalize(i.worldNormal + lightDir);
                float smoothNdotL = max(0, dot(averagedNormal, lightDir));

                // Edge detection based on normal and light direction
                float edgeFactor = abs(smoothNdotL - NdotL) > _EdgeDetectionThreshold ? 1.0 : 0.0;
                
                // Smooth shadow gradient
                float shadowFactor = smoothstep(_Cutoff - 0.1, _Cutoff + 0.1, smoothNdotL);
                fixed4 shadowColor = lerp(_ShadowColor, _Color, shadowFactor);

                // Final color blending
                fixed4 finalColor = texColor * shadowColor;
                finalColor.rgb *= _LightColor.rgb;

                return finalColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
