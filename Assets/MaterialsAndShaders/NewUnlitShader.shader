Shader "Custom/TwoColorStripesShader"
{
    Properties
    {
        _Color1 ("Stripe Color 1", Color) = (1, 1, 1, 1)
        _Color2 ("Stripe Color 2", Color) = (0, 0, 0, 1)
        _StripeWidth ("Stripe Width", Range(0.01, 100)) = 0.5
        _StripeFrequency ("Stripe Frequency", Range(0.01, 100)) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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

            // Material properties
            uniform float _StripeWidth;
            uniform float _StripeFrequency;
            uniform float4 _Color1;
            uniform float4 _Color2;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Adjust the UVs to make the stripes repeat correctly
                float stripePattern = sin(i.uv.x * _StripeFrequency * 3.14159);
                
                // Create alternating stripes using step function
                float stripes = step(stripePattern, _StripeWidth);

                // Blend between the two colors based on the stripe pattern
                return lerp(_Color1, _Color2, stripes);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
