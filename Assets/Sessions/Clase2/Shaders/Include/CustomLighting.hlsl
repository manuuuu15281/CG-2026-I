//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
//

void MainLight_float (float3 PositionWS,out float3 Direction, out float3 Color, out float ShadowAttenuation)//las funciones que vayamos a usar para nodos SIEMPRE tiene que ser un void. Segundo requerimiento: _float (es un sufijo de precisión que debe tener)
{
    #if defined(SHADERGRAPH_PREVIEW)
    Direction = normalize(float3(1,1,-1)); //Intrinsic function = no necesita librerias
    Color = 1.0f;
    ShadowAttenuation = 1.0f;
    #else
    float4 shadowCoord = TransformWorldToShadowCoord(PositionWS);
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    ShadowAttenuation = mainLight.shadowAttenuation;
    #endif
}

void AdditionalLightsSimple_float(float PositionWS, float3 NormalWS, float3 ViewDirectionWS, out float3 Lit)
{
    #ifdef SHADERGRAPH_PREVIEW

    Lit = 0;

    #else
    unit additionalLightCount = GetAdditionalLightsCount();

  

    //TODO: Forward+

    LIGHT_LOOP_BEGIN(additionalLightCount)
    Light currentLight = GetAdditionalLight(lightIndex, PositionWS);
    float lambert = dot(currentLight.direction, NormalWS);
    lambert = max (0, lambert * 0.5f + 0.5f); //half lambert

    //Diffuse
    float diffuse = lambert * 
    currentLight.color *
    currentLight.shadowAttenuation *
    currentLight.distanceAttenuation;

    //Specular
    float3 h = normalize(ViewDirectionWS + currentLight.direction);
    
    float blinnPhong= dot(h, NormalWS);
    blinnPhong = max(0,blinnPhong);
    blinnPhong = pow(blinnPhong, 60.0f);

    float specular = blinnPhong *    
    currentLight.color *
    currentLight.shadowAttenuation *
    currentLight.distanceAttenuation;

    Lit += diffuse + specular;

    LIGHT_LOOP_END

    #endif
}