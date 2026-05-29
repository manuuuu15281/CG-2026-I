using UnityEngine;

public class DissolveEffectController : MonoBehaviour
{
    [Header("Renderer del objeto que se disuelve")]
    public Renderer targetRenderer;

    [Header("Animación del dissolve")]
    public float startCutoff = -2f;
    public float endCutoff = 2f;
    public float duration = 2f;

    [Header("Estado")]
    public bool playOnStart = false;

    private Material materialInstance;
    private float timer;
    private bool isPlaying;

    private static readonly int CutoffHeightID = Shader.PropertyToID("_CutoffHeight");

    void Start()
    {
        if (targetRenderer == null)
        {
            Debug.LogError("Falta asignar el Target Renderer en DissolveEffectController.");
            return;
        }

        materialInstance = targetRenderer.material;

        ResetEffect();

        if (playOnStart)
        {
            PlayEffect();
        }
    }

    void Update()
    {
        if (!isPlaying || materialInstance == null)
            return;

        timer += Time.deltaTime;

        float t = timer / duration;
        t = Mathf.Clamp01(t);

        float cutoffValue = Mathf.Lerp(startCutoff, endCutoff, t);
        materialInstance.SetFloat(CutoffHeightID, cutoffValue);

        if (t >= 1f)
        {
            isPlaying = false;
        }
    }

    public void PlayEffect()
    {
        timer = 0f;
        isPlaying = true;

        if (materialInstance != null)
        {
            materialInstance.SetFloat(CutoffHeightID, startCutoff);
        }
    }

    public void PauseEffect()
    {
        isPlaying = false;
    }

    public void ResumeEffect()
    {
        isPlaying = true;
    }

    public void ResetEffect()
    {
        timer = 0f;
        isPlaying = false;

        if (materialInstance != null)
        {
            materialInstance.SetFloat(CutoffHeightID, startCutoff);
        }
    }
}