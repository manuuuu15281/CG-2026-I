using UnityEngine;

public class LevelUpLightBeamController : MonoBehaviour
{
    [Header("Renderer del beam")]
    public Renderer beamRenderer;

    [Header("Delay inicial")]
    public float startDelay = 1.2f;

    [Header("Animación del beam")]
    public float fadeInDuration = 0.25f;
    public float holdDuration = 1.1f;
    public float fadeOutDuration = 0.8f;

    [Header("Estado inicial")]
    public bool playOnStart = false;

    private Material beamMaterialInstance;
    private float timer;
    private bool isPlaying;
    private bool isPaused;

    private enum BeamState
    {
        Idle,
        Waiting,
        FadeIn,
        Hold,
        FadeOut,
        Finished
    }

    private BeamState state = BeamState.Idle;

    private static readonly int BeamIntensityID = Shader.PropertyToID("_BeamIntensity");

    void Awake()
    {
        if (beamRenderer == null)
        {
            beamRenderer = GetComponent<Renderer>();
        }

        if (beamRenderer != null)
        {
            beamMaterialInstance = beamRenderer.material;
            SetBeamIntensity(0f);
        }
    }

    void Start()
    {
        if (playOnStart)
        {
            PlayBeam();
        }
    }

    void Update()
    {
        if (!isPlaying || isPaused || beamMaterialInstance == null)
            return;

        timer += Time.deltaTime;

        if (state == BeamState.Waiting)
        {
            if (timer >= startDelay)
            {
                timer = 0f;
                state = BeamState.FadeIn;
                SetBeamIntensity(0f);
            }
        }
        else if (state == BeamState.FadeIn)
        {
            float t = Mathf.Clamp01(timer / fadeInDuration);
            SetBeamIntensity(t);

            if (t >= 1f)
            {
                timer = 0f;
                state = BeamState.Hold;
                SetBeamIntensity(1f);
            }
        }
        else if (state == BeamState.Hold)
        {
            SetBeamIntensity(1f);

            if (timer >= holdDuration)
            {
                timer = 0f;
                state = BeamState.FadeOut;
            }
        }
        else if (state == BeamState.FadeOut)
        {
            float t = Mathf.Clamp01(timer / fadeOutDuration);
            float intensity = Mathf.Lerp(1f, 0f, t);
            SetBeamIntensity(intensity);

            if (t >= 1f)
            {
                FinishBeam();
            }
        }
    }

    private void SetBeamIntensity(float value)
    {
        if (beamMaterialInstance != null)
        {
            beamMaterialInstance.SetFloat(BeamIntensityID, value);
        }
    }

    public void PlayBeam()
    {
        timer = 0f;
        isPlaying = true;
        isPaused = false;
        state = BeamState.Waiting;
        SetBeamIntensity(0f);
    }

    public void PauseBeam()
    {
        isPaused = true;
    }

    public void ResumeBeam()
    {
        isPaused = false;
    }

    public void ResetBeam()
    {
        timer = 0f;
        isPlaying = false;
        isPaused = false;
        state = BeamState.Idle;
        SetBeamIntensity(0f);
    }

    private void FinishBeam()
    {
        timer = 0f;
        isPlaying = false;
        isPaused = false;
        state = BeamState.Finished;
        SetBeamIntensity(0f);
    }
}