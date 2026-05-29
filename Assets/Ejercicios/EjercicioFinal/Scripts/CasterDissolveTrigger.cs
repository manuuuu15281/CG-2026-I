using UnityEngine;

public class CasterDissolveTrigger : MonoBehaviour
{
    [Header("Referencias")]
    public Animator casterAnimator;
    public DissolveEffectController dissolveEffect;

    [Header("Sincronización con la animación")]
    public float triggerDelay = 1.2f;

    [Header("Opciones")]
    public bool playOnStart = true;

    private float timer;
    private bool sequencePlaying;
    private bool sequencePaused;
    private bool dissolveTriggered;

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

        if (!dissolveTriggered && timer >= triggerDelay)
        {
            dissolveTriggered = true;

            if (dissolveEffect != null)
            {
                dissolveEffect.PlayEffect();
            }
        }
    }

    public void PlaySequence()
    {
        if (dissolveEffect == null)
        {
            Debug.LogError("Falta asignar el Dissolve Effect en CasterDissolveTrigger.");
            return;
        }

        timer = 0f;
        sequencePlaying = true;
        sequencePaused = false;
        dissolveTriggered = false;

        dissolveEffect.ResetEffect();

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

        if (dissolveEffect != null)
        {
            dissolveEffect.PauseEffect();
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

        if (dissolveEffect != null && dissolveTriggered)
        {
            dissolveEffect.ResumeEffect();
        }
    }

    public void StopSequence()
    {
        sequencePlaying = false;
        sequencePaused = false;
        dissolveTriggered = false;
        timer = 0f;

        if (casterAnimator != null)
        {
            casterAnimator.speed = 0f;
            casterAnimator.Play(0, 0, 0f);
        }

        if (dissolveEffect != null)
        {
            dissolveEffect.ResetEffect();
        }
    }
}