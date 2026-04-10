//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
//

void MainLight_float (float3 PositionWS,out float3 Direction, out float3 Color)//las funciones que vayamos a usar para nodos SIEMPRE tiene que ser un void. Segundo requerimiento: _float (es un sufijo de precisión que debe tener)
{
    #if defined(SHADERGRAPH_PREVIEW)
    Direction = normalize(float3(1,1,-1)); //Intrinsic function = no necesita librerias
    Color = 1.0f;
    #else
    float4 shadowCoord = TransformWorldToShadowCoord(PositionWS);
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    #endif
}