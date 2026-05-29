using UnityEngine;

public class HologramMotion : MonoBehaviour
{
    [Header("Levitación")]
    public float floatAmplitude = 0.15f;
    public float floatFrequency = 1.2f;

    [Header("Rotación")]
    public float rotationSpeed = 25f;

    private Vector3 startLocalPosition;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        transform.localPosition = startLocalPosition + new Vector3(0f, yOffset, 0f);

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}