using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterGridMeshGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    [Range(10, 300)]
    public int resolution = 160;

    public float size = 10f;

    private int lastResolution;
    private float lastSize;

    private void OnEnable()
    {
        GenerateMesh();
    }

    private void OnValidate()
    {
        GenerateMesh();
    }

    private void Update()
    {
        if (resolution != lastResolution || Mathf.Abs(size - lastSize) > 0.001f)
        {
            GenerateMesh();
        }
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Water Grid";

        int verticesPerSide = resolution + 1;

        Vector3[] vertices = new Vector3[verticesPerSide * verticesPerSide];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[resolution * resolution * 6];

        float halfSize = size * 0.5f;

        for (int z = 0; z < verticesPerSide; z++)
        {
            for (int x = 0; x < verticesPerSide; x++)
            {
                int index = z * verticesPerSide + x;

                float xPos = ((float)x / resolution) * size - halfSize;
                float zPos = ((float)z / resolution) * size - halfSize;

                vertices[index] = new Vector3(xPos, 0f, zPos);
                uvs[index] = new Vector2((float)x / resolution, (float)z / resolution);
            }
        }

        int triangleIndex = 0;

        for (int z = 0; z < resolution; z++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int bottomLeft = z * verticesPerSide + x;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + verticesPerSide;
                int topRight = topLeft + 1;

                triangles[triangleIndex++] = bottomLeft;
                triangles[triangleIndex++] = topLeft;
                triangles[triangleIndex++] = bottomRight;

                triangles[triangleIndex++] = bottomRight;
                triangles[triangleIndex++] = topLeft;
                triangles[triangleIndex++] = topRight;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = mesh;

        lastResolution = resolution;
        lastSize = size;
    }
}