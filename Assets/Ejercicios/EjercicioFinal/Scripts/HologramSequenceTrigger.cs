using UnityEngine;

public class HologramSequenceTrigger : MonoBehaviour
{
    [Header("Referencias")]
    public Animator casterAnimator;
    public HologramRevealController hologramReveal;

    [Header("Sincronización")]
    public float triggerDelay = 1.1f;

    [Header("Opciones")]
    public bool playOnStart = true;

    private float timer;
    private bool sequencePlaying;
    private bool sequencePaused;
    private bool hologramTriggered;

    void Start()
    {
        if (casterAnimator == null)
        {
            casterAnimator = GetComponent<Animator>();
        }

        if (playOnStart)
        {
            PlaySequence();
        }
    }

    void Update()
    {
        if (!sequencePlaying || sequencePaused)
            return;

        timer += Time.deltaTime;

        if (!hologramTriggered && timer >= triggerDelay)
        {
            hologramTriggered = true;

            if (hologramReveal != null)
            {
                hologramReveal.PlayReveal();
            }
        }
    }

    public void PlaySequence()
    {
        if (hologramReveal == null)
        {
            Debug.LogError("Falta asignar Hologram Reveal en HologramSequenceTrigger.");
            return;
        }

        timer = 0f;
        sequencePlaying = true;
        sequencePaused = false;
        hologramTriggered = false;

        hologramReveal.ResetReveal();

        if (casterAnimator != null)
        {
            casterAnimator.speed = 1f;
            casterAnimator.Play(0, 0, 0f);
        }
    }

    public void PauseSequence()
    {
        if (!sequencePlaying)
            return;

        sequencePaused = true;

        if (casterAnimator != null)
        {
            casterAnimator.speed = 0f;
        }

        if (hologramReveal != null)
        {
            hologramReveal.PauseReveal();
        }
    }

    public void ResumeSequence()
    {
        if (!sequencePlaying)
            return;

        sequencePaused = false;

        if (casterAnimator != null)
        {
            casterAnimator.speed = 1f;
        }

        if (hologramReveal != null && hologramTriggered)
        {
            hologramReveal.ResumeReveal();
        }
    }

    public void StopSequence()
    {
        sequencePlaying = false;
        sequencePaused = false;
        hologramTriggered = false;
        timer = 0f;

        if (casterAnimator != null)
        {
            casterAnimator.speed = 0f;
            casterAnimator.Play(0, 0, 0f);
        }

        if (hologramReveal != null)
        {
            hologramReveal.ResetReveal();
        }
    }
}