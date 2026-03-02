using System;
using UnityEngine;

[ExecuteInEditMode]

public class VertexAnimationArea : MonoBehaviour
{
    [SerializeField] private float radius;

    [SerializeField] private Material targetMaterial;

    //Se llama cada frame del juego
    
    void Update()
    {
        Vector3 pos = transform.position;
        Vector4 sphere = new Vector4 (pos.x, pos.y, pos.y, w:radius);
        targetMaterial?.SetVector(name:"_SphereDescriptor", sphere);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    } 
}
