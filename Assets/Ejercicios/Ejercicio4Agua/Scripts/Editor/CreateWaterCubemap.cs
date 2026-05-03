using UnityEditor;
using UnityEngine;

public static class CreateWaterCubemap
{
    [MenuItem("Ejercicio4Agua/Create Water Reflection Cubemap")]
    public static void CreateCubemap()
    {
        int size = 128;

        Cubemap cubemap = new Cubemap(size, TextureFormat.RGBA32, false);
        cubemap.name = "T_WaterReflectionCubemap";

        SetFace(cubemap, CubemapFace.PositiveY, new Color(0.35f, 0.65f, 1.0f, 1.0f)); // Sky top
        SetFace(cubemap, CubemapFace.NegativeY, new Color(0.05f, 0.12f, 0.18f, 1.0f)); // Dark ground/water
        SetFace(cubemap, CubemapFace.PositiveX, new Color(0.12f, 0.35f, 0.55f, 1.0f));
        SetFace(cubemap, CubemapFace.NegativeX, new Color(0.08f, 0.25f, 0.45f, 1.0f));
        SetFace(cubemap, CubemapFace.PositiveZ, new Color(0.18f, 0.45f, 0.70f, 1.0f));
        SetFace(cubemap, CubemapFace.NegativeZ, new Color(0.10f, 0.30f, 0.50f, 1.0f));

        cubemap.Apply();

        string path = "Assets/Ejercicios/Ejercicio4Agua/Textures/T_WaterReflectionCubemap.cubemap";

        AssetDatabase.CreateAsset(cubemap, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = cubemap;

        Debug.Log("Water Reflection Cubemap created at: " + path);
    }

    private static void SetFace(Cubemap cubemap, CubemapFace face, Color color)
    {
        int size = cubemap.width;
        Color[] pixels = new Color[size * size];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        cubemap.SetPixels(pixels, face);
    }
}