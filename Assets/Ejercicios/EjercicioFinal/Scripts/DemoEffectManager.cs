using UnityEngine;
using TMPro;

public class DemoEffectManager : MonoBehaviour
{
    [Header("Objetos principales de cada efecto")]
    public GameObject effect01_Dissolve;
    public GameObject effect02_Hologram;
    public GameObject effect03_LevelUp;

    [Header("Controladores de secuencia")]
    public CasterDissolveTrigger dissolveSequence;
    public HologramSequenceTrigger hologramSequence;
    public LevelUpSequenceTrigger levelUpSequence;

    [Header("Texto UI")]
    public TMP_Text selectedEffectText;
    public TMP_Text pauseButtonText;

    private int selectedEffect = 1;
    private bool isPaused = false;

    void Start()
    {
        SelectEffect01();
    }

    public void SelectEffect01()
    {
        StopAllEffects();

        selectedEffect = 1;
        isPaused = false;

        effect01_Dissolve.SetActive(true);
        effect02_Hologram.SetActive(false);
        effect03_LevelUp.SetActive(false);

        UpdateTexts("Effect 01: Dissolve");
    }

    public void SelectEffect02()
    {
        StopAllEffects();

        selectedEffect = 2;
        isPaused = false;

        effect01_Dissolve.SetActive(false);
        effect02_Hologram.SetActive(true);
        effect03_LevelUp.SetActive(false);

        UpdateTexts("Effect 02: Hologram");
    }

    public void SelectEffect03()
    {
        StopAllEffects();

        selectedEffect = 3;
        isPaused = false;

        effect01_Dissolve.SetActive(false);
        effect02_Hologram.SetActive(false);
        effect03_LevelUp.SetActive(true);

        UpdateTexts("Effect 03: Level Up");
    }

    public void PlaySelectedEffect()
    {
        isPaused = false;

        if (pauseButtonText != null)
        {
            pauseButtonText.text = "PAUSE";
        }

        if (selectedEffect == 1 && dissolveSequence != null)
        {
            dissolveSequence.PlaySequence();
        }
        else if (selectedEffect == 2 && hologramSequence != null)
        {
            hologramSequence.PlaySequence();
        }
        else if (selectedEffect == 3 && levelUpSequence != null)
        {
            levelUpSequence.PlaySequence();
        }
    }

    public void PauseOrResumeSelectedEffect()
    {
        if (!isPaused)
        {
            PauseSelectedEffect();
            isPaused = true;

            if (pauseButtonText != null)
            {
                pauseButtonText.text = "RESUME";
            }
        }
        else
        {
            ResumeSelectedEffect();
            isPaused = false;

            if (pauseButtonText != null)
            {
                pauseButtonText.text = "PAUSE";
            }
        }
    }

    private void PauseSelectedEffect()
    {
        if (selectedEffect == 1 && dissolveSequence != null)
        {
            dissolveSequence.PauseSequence();
        }
        else if (selectedEffect == 2 && hologramSequence != null)
        {
            hologramSequence.PauseSequence();
        }
        else if (selectedEffect == 3 && levelUpSequence != null)
        {
            levelUpSequence.PauseSequence();
        }
    }

    private void ResumeSelectedEffect()
    {
        if (selectedEffect == 1 && dissolveSequence != null)
        {
            dissolveSequence.ResumeSequence();
        }
        else if (selectedEffect == 2 && hologramSequence != null)
        {
            hologramSequence.ResumeSequence();
        }
        else if (selectedEffect == 3 && levelUpSequence != null)
        {
            levelUpSequence.ResumeSequence();
        }
    }

    private void StopAllEffects()
    {
        if (dissolveSequence != null)
        {
            dissolveSequence.StopSequence();
        }

        if (hologramSequence != null)
        {
            hologramSequence.StopSequence();
        }

        if (levelUpSequence != null)
        {
            levelUpSequence.StopSequence();
        }
    }

    private void UpdateTexts(string selectedText)
    {
        if (selectedEffectText != null)
        {
            selectedEffectText.text = selectedText;
        }

        if (pauseButtonText != null)
        {
            pauseButtonText.text = "PAUSE";
        }
    }
}