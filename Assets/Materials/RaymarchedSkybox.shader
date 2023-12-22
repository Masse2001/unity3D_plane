Shader "Custom/RaymarchedSkybox" {
    Properties{
        _SkyColor("Sky Color", Color) = (0, 0, 0, 1)
        _GroundColor("Ground Color", Color) = (0, 0, 0, 1)
        _FogColor("Fog Color", Color) = (0, 0, 0, 1)
        _FogClouds("Fog Amount Clouds", Float) = 0.001
        _FogSky("Fog Amount Sky", Float) = 0.1
        _FogGround("Fog Amount Ground", Float) = 0.1
        _CloudHeight("Height of Cloud", Float) = 1
        _CloudThickness("Cloud Thickness", Float) = 1
        _CloudOpacity("Opacity of Cloud", Float) = 0.1
        _CloudScale("Density of Cloud", Vector) = (0, 0, 1)
        _TopSurfaceScale("Top Surface Scale", Float) = 0.1
        _BottomSurfaceScale("Bottom Surface Scale", Float) = 0.1
        _CloudTex("Cloud Texture", 2D) = "white" { }
        _CloudSoftness("Cloud Softness", Float) = 0.1
        _SampleTex("Sample Texture", 2D) = "white" { }
        SAMPLES("Number of Samples", Float) = 25
    }

        SubShader{
            Tags {
                "Queue" = "Background"
                "RenderType" = "Background"
                "PreviewType" = "Skybox"
            }
            Cull Off
            ZWrite Off

            HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            float4 _SkyColor;
            float4 _GroundColor;
            float4 _FogColor;
            float _FogClouds;
            float _FogSky;
            float _FogGround;
            float _CloudHeight;
            float _CloudThickness;
            float _CloudOpacity;
            float3 _CloudScale;
            float _TopSurfaceScale;
            float _BottomSurfaceScale;
            float _CloudSoftness;
            sampler2D _CloudTex;
            sampler2D _SampleTex;
            int SAMPLES;

            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 vertex : SV_POSITION;
                float3 viewVector : TEXCOORD1;
            };
            ENDHLSL

            Pass {
                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = TransformObjectToHClip(v.vertex);
                    o.viewVector = v.vertex.xyz;
                    return o;
                }

                float4 frag(v2f i) : SV_Target {
                    float3 viewVector = i.viewVector;
                    if (viewVector.y > 0) { // SKY
                        viewVector = viewVector / viewVector.y;
                        float3 viewerPosition = _WorldSpaceCameraPos;
                        float3 position = viewerPosition + viewVector * (_CloudHeight - viewerPosition.y);
                        float cloudFog = 1 - (1 / (_FogClouds * length(viewVector) + 1));
                        float3 stepSize = viewVector * _CloudThickness / SAMPLES;
                        float stepOpacity = 1 - (1 / (_CloudOpacity * length(stepSize) + 1));
                        float4 col = float4(_FogColor.rgb * cloudFog, cloudFog);
                        for (int j = 0; j < SAMPLES; j++) {
                            position += stepSize;
                            float2 uv = position.xz / _CloudScale.xy;
                            float h = SAMPLE_TEXTURE2D(_CloudTex, sampler_CloudTex, uv).r;
                            float cloudTopHeight = 1 - (h * _TopSurfaceScale);
                            float cloudBottomHeight = h * _BottomSurfaceScale;
                            float f = (position.y - _CloudHeight) / _CloudThickness;
                            if (f > cloudBottomHeight && f < cloudTopHeight) {
                                float dist = min(cloudTopHeight - f, f - cloudBottomHeight);
                                float localOpacity = saturate(dist / _CloudSoftness);
                                float4 cloudColor = float4(1, 1, 1, 1);
                                col += (1 - col.a) * stepOpacity * localOpacity * cloudColor;
                                if (col.a > 0.99) { // almost opaque:stop marching
                                    col.rgb *= 1 / col.a;
                                    col.a = 1;
                                    break;
                                }
                            }
                        }
                        float skyFog = 1 - (1 / (_FogSky * length(viewVector) + 1));
                        float4 totalSkyColor = lerp(_SkyColor, _FogColor, skyFog);
                        col += (1 - col.a) * totalSkyColor;
                        return col;
                    }
                    else if (viewVector.y < 0) { // GROUND
                        viewVector = viewVector / viewVector.y;
                        float groundFog = 1 - (1 / (_FogGround * length(viewVector) + 1));
                        return lerp(_GroundColor, _FogColor, groundFog);
                    }
                    else { // HORIZON
                        return _FogColor;
                    }
                }
                ENDHLSL
            }
    }
}
