using UnityEngine;

public class HologramRevealController : MonoBehaviour
{
    [Header("Renderers del holograma")]
    public Renderer[] hologramRenderers;

    [Header("Animación de aparición")]
    public float revealDuration = 1.2f;

    [Header("Titileo al aparecer")]
    public bool flickerOnReveal = true;
    public float flickerStrength = 0.35f;
    public float flickerFrequency = 28f;

    [Header("Estado inicial")]
    public bool visibleOnStart = false;

    private Material[] materialInstances;
    private float timer;
    private bool isPlaying;
    private bool isPaused;

    private static readonly int RevealAmountID = Shader.PropertyToID("_RevealAmount");

    void Awake()
    {
        if (hologramRenderers == null || hologramRenderers.Length == 0)
        {
            hologramRenderers = GetComponentsInChildren<Renderer>(true);
        }

        CreateMaterialInstances();

        if (visibleOnStart)
        {
            SetRevealAmount(1f);
        }
        else
        {
            SetRevealAmount(0f);
        }
    }

    void Update()
    {
        if (!isPlaying || isPaused)
            return;

        timer += Time.deltaTime;

        float t = timer / revealDuration;
        t = Mathf.Clamp01(t);

        float revealValue = t;

        if (flickerOnReveal)
        {
            float flickerFade = 1f - t;
            float flicker = Mathf.Sin(Time.time * flickerFrequency) * flickerStrength * flickerFade;
            revealValue = Mathf.Clamp01(t + flicker);
        }

        SetRevealAmount(revealValue);

        if (t >= 1f)
        {
            SetRevealAmount(1f);
            isPlaying = false;
        }
    }

    private void CreateMaterialInstances()
    {
        materialInstances = new Material[hologramRenderers.Length];

        for (int i = 0; i < hologramRenderers.Length; i++)
        {
            if (hologramRenderers[i] != null)
            {
                materialInstances[i] = hologramRenderers[i].material;
            }
        }
    }

    private void SetRevealAmount(float value)
    {
        if (materialInstances == null)
            return;

        for (int i = 0; i < materialInstances.Length; i++)
        {
            if (materialInstances[i] != null)
            {
                materialInstances[i].SetFloat(RevealAmountID, value);
            }
        }
    }

    public void PlayReveal()
    {
        timer = 0f;
        isPlaying = true;
        isPaused = false;
        SetRevealAmount(0f);
    }

    public void PauseReveal()
    {
        isPaused = true;
    }

    public void ResumeReveal()
    {
        isPaused = false;
    }

    public void ResetReveal()
    {
        timer = 0f;
        isPlaying = false;
        isPaused = false;
        SetRevealAmount(0f);
    }
}