#ifndef VERTEX_WIND_INCLUDED
#define VERTEX_WIND_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

void wind_simplified_float(
    float3 vertex_xyz,
    float4 color,
    float _WaveFreq,
    float _WaveHeight,
    float _WaveScale,
    out float3 wind_xyz)
{
    // URP time replacement
    float time = _TimeParameters.x;

    float phase_slow = time * _WaveFreq;
    float phase_med  = time * 3 * _WaveFreq;
    float phase_fast = time * 5 * _WaveFreq;

    float offset  = (vertex_xyz.x + (vertex_xyz.z * _WaveScale)) * _WaveScale;
    float offset2 = (vertex_xyz.x + (vertex_xyz.z * _WaveScale * 3)) * _WaveScale * 3;
    float offset3 = (vertex_xyz.x + (vertex_xyz.z * _WaveScale * 5)) * _WaveScale * 5;

    float sin1 = sin(phase_slow + offset);
    float sin2 = sin(phase_med + offset2);
    float sin3 = sin(phase_fast + offset3);

    float sin_combined = (sin1 * 4) + sin2 + (sin3 * 0.5);

    float wind_factor = sin_combined * _WaveHeight * 0.1;
    wind_factor *= color.r;

    wind_xyz = float3(wind_factor, wind_factor * 0.2, wind_factor);

    // URP matrix replacement
    float3x3 worldToObject = (float3x3)UNITY_MATRIX_I_M;
    wind_xyz = mul(worldToObject, wind_xyz);
}

#endif