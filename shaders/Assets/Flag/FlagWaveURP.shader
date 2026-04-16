Shader "Custom/FlagWaveURP"
{
    Properties
    {
        _BaseMap("Base Texture", 2D) = "white" {}
        _WaveSpeed("Wave Speed", Float) = 2.0
        _WaveAmplitude("Wave Amplitude", Float) = 0.2
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

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float _WaveSpeed;
                float _WaveAmplitude;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                float wave = sin(_Time.y * _WaveSpeed + input.positionOS.x) * _WaveAmplitude;
                input.positionOS.z += wave;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);
                return color;
            }
            ENDHLSL
        }
    }
}
