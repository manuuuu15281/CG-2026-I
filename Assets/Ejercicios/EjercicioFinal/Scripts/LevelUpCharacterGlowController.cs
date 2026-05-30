using UnityEngine;

public class LevelUpCharacterGlowController : MonoBehaviour
{
    [Header("Renderers del personaje")]
    public Renderer[] characterRenderers;

    [Header("Material dorado del Level Up")]
    public Material levelUpMaterial;

    [Header("Delay inicial")]
    public float startDelay = 1f;

    [Header("Animación del brillo")]
    public float fadeInDuration = 0.35f;
    public float holdDuration = 1.2f;
    public float fadeOutDuration = 0.8f;

    [Header("Estado inicial")]
    public bool playOnStart = false;
    public bool restoreOriginalMaterialsAtEnd = true;

    private Material[][] originalMaterials;
    private Material[][] levelUpMaterialInstances;

    private float timer;
    private bool isPlaying;
    private bool isPaused;

    private enum GlowState
    {
        Idle,
        Waiting,
        FadeIn,
        Hold,
        FadeOut,
        Finished
    }

    private GlowState state = GlowState.Idle;

    private static readonly int GlowAmountID = Shader.PropertyToID("_GlowAmount");

    void Awake()
    {
        if (characterRenderers == null || characterRenderers.Length == 0)
        {
            characterRenderers = GetComponentsInChildren<Renderer>(true);
        }

        CacheOriginalMaterials();
        CreateLevelUpMaterialInstances();
        SetGlowAmount(0f);
    }

    void Start()
    {
        if (playOnStart)
        {
            PlayGlow();
        }
    }

    void Update()
    {
        if (!isPlaying || isPaused)
            return;

        timer += Time.deltaTime;

        if (state == GlowState.Waiting)
        {
            if (timer >= startDelay)
            {
                timer = 0f;
                state = GlowState.FadeIn;

                ApplyLevelUpMaterials();
                SetGlowAmount(0f);
            }
        }
        else if (state == GlowState.FadeIn)
        {
            float t = Mathf.Clamp01(timer / fadeInDuration);
            SetGlowAmount(t);

            if (t >= 1f)
            {
                timer = 0f;
                state = GlowState.Hold;
                SetGlowAmount(1f);
            }
        }
        else if (state == GlowState.Hold)
        {
            SetGlowAmount(1f);

            if (timer >= holdDuration)
            {
                timer = 0f;
                state = GlowState.FadeOut;
            }
        }
        else if (state == GlowState.FadeOut)
        {
            float t = Mathf.Clamp01(timer / fadeOutDuration);
            float glow = Mathf.Lerp(1f, 0f, t);
            SetGlowAmount(glow);

            if (t >= 1f)
            {
                FinishGlow();
            }
        }
    }

    private void CacheOriginalMaterials()
    {
        originalMaterials = new Material[characterRenderers.Length][];

        for (int i = 0; i < characterRenderers.Length; i++)
        {
            if (characterRenderers[i] != null)
            {
                originalMaterials[i] = characterRenderers[i].materials;
            }
        }
    }

    private void CreateLevelUpMaterialInstances()
    {
        levelUpMaterialInstances = new Material[characterRenderers.Length][];

        for (int i = 0; i < characterRenderers.Length; i++)
        {
            if (characterRenderers[i] == null || levelUpMaterial == null)
                continue;

            int materialCount = characterRenderers[i].materials.Length;
            levelUpMaterialInstances[i] = new Material[materialCount];

            for (int j = 0; j < materialCount; j++)
            {
                levelUpMaterialInstances[i][j] = new Material(levelUpMaterial);
            }
        }
    }

    private void ApplyLevelUpMaterials()
    {
        for (int i = 0; i < characterRenderers.Length; i++)
        {
            if (characterRenderers[i] != null && levelUpMaterialInstances[i] != null)
            {
                characterRenderers[i].materials = levelUpMaterialInstances[i];
            }
        }
    }

    private void RestoreOriginalMaterials()
    {
        for (int i = 0; i < characterRenderers.Length; i++)
        {
            if (characterRenderers[i] != null && originalMaterials[i] != null)
            {
                characterRenderers[i].materials = originalMaterials[i];
            }
        }
    }

    private void SetGlowAmount(float value)
    {
        if (levelUpMaterialInstances == null)
            return;

        for (int i = 0; i < levelUpMaterialInstances.Length; i++)
        {
            if (levelUpMaterialInstances[i] == null)
                continue;

            for (int j = 0; j < levelUpMaterialInstances[i].Length; j++)
            {
                if (levelUpMaterialInstances[i][j] != null)
                {
                    levelUpMaterialInstances[i][j].SetFloat(GlowAmountID, value);
                }
            }
        }
    }

    public void PlayGlow()
    {
        if (levelUpMaterial == null)
        {
            Debug.LogError("Falta asignar el Level Up Material.");
            return;
        }

        timer = 0f;
        isPlaying = true;
        isPaused = false;
        state = GlowState.Waiting;

        SetGlowAmount(0f);

        if (restoreOriginalMaterialsAtEnd)
        {
            RestoreOriginalMaterials();
        }
    }

    public void PauseGlow()
    {
        isPaused = true;
    }

    public void ResumeGlow()
    {
        isPaused = false;
    }

    public void ResetGlow()
    {
        timer = 0f;
        isPlaying = false;
        isPaused = false;
        state = GlowState.Idle;

        SetGlowAmount(0f);

        if (restoreOriginalMaterialsAtEnd)
        {
            RestoreOriginalMaterials();
        }
    }

    private void FinishGlow()
    {
        timer = 0f;
        isPlaying = false;
        isPaused = false;
        state = GlowState.Finished;

        SetGlowAmount(0f);

        if (restoreOriginalMaterialsAtEnd)
        {
            RestoreOriginalMaterials();
        }
    }
}