using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("Movimiento de levitación")]
    public float amplitude = 0.25f;
    public float frequency = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.localPosition = startPosition + new Vector3(0f, yOffset, 0f);
    }
}