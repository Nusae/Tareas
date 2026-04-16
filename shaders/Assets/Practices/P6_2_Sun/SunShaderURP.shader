Shader "Custom/SunShaderURP"
{
    Properties
    {
        _MainTex("Sun Texture", 2D) = "white" {}
        _SunColor("Sun Color", Color) = (1, 1, 0, 1)
        _Speed("Scroll Speed", Vector) = (0.1, 0.05, 0, 0)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _SunColor;
                float4 _Speed;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float2 scrollingUV = input.uv + _Speed.xy * _Time.y;
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, scrollingUV);
                return texColor * _SunColor;
            }
            ENDHLSL
        }
    }
}
