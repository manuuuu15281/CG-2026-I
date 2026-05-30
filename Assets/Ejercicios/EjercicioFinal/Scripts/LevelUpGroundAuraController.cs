using UnityEngine;

public class LevelUpGroundAuraController : MonoBehaviour
{
    [Header("Renderer del aura")]
    public Renderer auraRenderer;

    [Header("Delay inicial")]
    public float startDelay = 1.2f;

    [Header("Animación del aura")]
    public float fadeInDuration = 0.3f;
    public float holdDuration = 1.1f;
    public float fadeOutDuration = 0.8f;

    [Header("Estado inicial")]
    public bool playOnStart = false;

    private Material auraMaterialInstance;
    private float timer;
    private bool isPlaying;
    private bool isPaused;

    private enum AuraState
    {
        Idle,
        Waiting,
        FadeIn,
        Hold,
        FadeOut,
        Finished
    }

    private AuraState state = AuraState.Idle;

    private static readonly int AuraIntensityID = Shader.PropertyToID("_AuraIntensity");

    void Awake()
    {
        if (auraRenderer == null)
        {
            auraRenderer = GetComponent<Renderer>();
        }

        if (auraRenderer != null)
        {
            auraMaterialInstance = auraRenderer.material;
            SetAuraIntensity(0f);
        }
    }

    void Start()
    {
        if (playOnStart)
        {
            PlayAura();
        }
    }

    void Update()
    {
        if (!isPlaying || isPaused || auraMaterialInstance == null)
            return;

        timer += Time.deltaTime;

        if (state == AuraState.Waiting)
        {
            if (timer >= startDelay)
            {
                timer = 0f;
                state = AuraState.FadeIn;
                SetAuraIntensity(0f);
            }
        }
        else if (state == AuraState.FadeIn)
        {
            float t = Mathf.Clamp01(timer / fadeInDuration);
            SetAuraIntensity(t);

            if (t >= 1f)
            {
                timer = 0f;
                state = AuraState.Hold;
                SetAuraIntensity(1f);
            }
        }
        else if (state == AuraState.Hold)
        {
            SetAuraIntensity(1f);

            if (timer >= holdDuration)
            {
                timer = 0f;
                state = AuraState.FadeOut;
            }
        }
        else if (state == AuraState.FadeOut)
        {
            float t = Mathf.Clamp01(timer / fadeOutDuration);
            float intensity = Mathf.Lerp(1f, 0f, t);
            SetAuraIntensity(intensity);

            if (t >= 1f)
            {
                FinishAura();
            }
        }
    }

    private void SetAuraIntensity(float value)
    {
        if (auraMaterialInstance != null)
        {
            auraMaterialInstance.SetFloat(AuraIntensityID, value);
        }
    }

    public void PlayAura()
    {
        timer = 0f;
        isPlaying = true;
        isPaused = false;
        state = AuraState.Waiting;
        SetAuraIntensity(0f);
    }

    public void PauseAura()
    {
        isPaused = true;
    }

    public void ResumeAura()
    {
        isPaused = false;
    }

    public void ResetAura()
    {
        timer = 0f;
        isPlaying = false;
        isPaused = false;
        state = AuraState.Idle;
        SetAuraIntensity(0f);
    }

    private void FinishAura()
    {
        timer = 0f;
        isPlaying = false;
        isPaused = false;
        state = AuraState.Finished;
        SetAuraIntensity(0f);
    }
}