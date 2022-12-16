Shader "Hidden/Custom/Outline"
{
    HLSLINCLUDE
    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
    
    int _Thickness;
    float _TransitionSmoothness;
    float _Edge;
    float4 _Color;

    float4 Frag(VaryingsDefault i) : SV_Target {
        float2 offset = _Thickness / _ScreenParams;

        float left = LinearEyeDepth( SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(-offset.x,0)).x );
        float right = LinearEyeDepth( SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(offset.x,0)).x );
        float up = LinearEyeDepth( SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(0,offset.y)).x );
        float down = LinearEyeDepth( SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(0, -offset.y)).x );

        float delta = sqrt( pow(right - left, 2) + pow(up - down, 2) );

        float t = smoothstep(_Edge, _Edge + _TransitionSmoothness, delta);

        float4 mainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

        float4 color = lerp(mainTex, _Color, _Color.a);

        float4 output = lerp(mainTex, color, t);

        return output;
    }

    ENDHLSL
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment Frag
            ENDHLSL
        }
    }
}