#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

void MainLight_float(out float3 Direction, out float3 Color){

    #if defined (SHADERGRAPH_PREVIEW)
    Direction = normalize(float3(1,1,-1));
    Color = 1.0f;

    #else
    Light mainLight = GetMainLight();
    Direction = mainLight.direction;
    Color = mainLight.color;
    #endif
}