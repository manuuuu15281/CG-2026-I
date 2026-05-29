using UnityEngine;
using TMPro;

public class DissolveUIController : MonoBehaviour
{
    [Header("Secuencia del efecto")]
    public CasterDissolveTrigger dissolveSequence;

    [Header("Texto del botón Pause")]
    public TMP_Text pauseButtonText;

    private bool isPaused = false;

    public void PlayOrRestartEffect()
    {
        if (dissolveSequence == null)
        {
            Debug.LogError("Falta asignar Dissolve Sequence en DissolveUIController.");
            return;
        }

        isPaused = false;

        if (pauseButtonText != null)
        {
            pauseButtonText.text = "PAUSE";
        }

        dissolveSequence.PlaySequence();
    }

    public void PauseOrResumeEffect()
    {
        if (dissolveSequence == null)
        {
            Debug.LogError("Falta asignar Dissolve Sequence en DissolveUIController.");
            return;
        }

        if (!isPaused)
        {
            dissolveSequence.PauseSequence();
            isPaused = true;

            if (pauseButtonText != null)
            {
                pauseButtonText.text = "RESUME";
            }
        }
        else
        {
            dissolveSequence.ResumeSequence();
            isPaused = false;

            if (pauseButtonText != null)
            {
                pauseButtonText.text = "PAUSE";
            }
        }
    }
}