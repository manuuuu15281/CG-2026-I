using UnityEngine;

public class LevelUpSequenceTrigger : MonoBehaviour
{
    [Header("Referencias")]
    public Animator characterAnimator;
    public LevelUpCharacterGlowController glowController;
    public LevelUpGroundAuraController groundAuraController;
    public LevelUpLightBeamController lightBeamController;
    public ParticleSystem[] particleSystems;

    [Header("Sincronización")]
    public float particlesDelay = 1.2f;

    [Header("Opciones")]
    public bool playOnStart = true;

    private float timer;
    private bool sequencePlaying;
    private bool sequencePaused;
    private bool particlesTriggered;

    void Start()
    {
        if (characterAnimator == null)
        {
            characterAnimator = GetComponent<Animator>();
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

        if (!particlesTriggered && timer >= particlesDelay)
        {
            particlesTriggered = true;
            PlayParticles();
        }
    }

    public void PlaySequence()
    {
        timer = 0f;
        sequencePlaying = true;
        sequencePaused = false;
        particlesTriggered = false;

        StopAndClearParticles();

        if (characterAnimator != null)
        {
            characterAnimator.speed = 1f;
            characterAnimator.Play(0, 0, 0f);
        }

        if (glowController != null)
        {
            glowController.ResetGlow();
            glowController.PlayGlow();
        }

        if (groundAuraController != null)
        {
            groundAuraController.ResetAura();
            groundAuraController.PlayAura();
        }

        if (lightBeamController != null)
        {
            lightBeamController.ResetBeam();
            lightBeamController.PlayBeam();
        }
    }

    public void PauseSequence()
    {
        if (!sequencePlaying)
            return;

        sequencePaused = true;

        if (characterAnimator != null)
        {
            characterAnimator.speed = 0f;
        }

        if (glowController != null)
        {
            glowController.PauseGlow();
        }

        if (groundAuraController != null)
        {
            groundAuraController.PauseAura();
        }

        if (lightBeamController != null)
        {
            lightBeamController.PauseBeam();
        }

        PauseParticles();
    }

    public void ResumeSequence()
    {
        if (!sequencePlaying)
            return;

        sequencePaused = false;

        if (characterAnimator != null)
        {
            characterAnimator.speed = 1f;
        }

        if (glowController != null)
        {
            glowController.ResumeGlow();
        }

        if (groundAuraController != null)
        {
            groundAuraController.ResumeAura();
        }

        if (lightBeamController != null)
        {
            lightBeamController.ResumeBeam();
        }

        ResumeParticles();
    }

    public void StopSequence()
    {
        timer = 0f;
        sequencePlaying = false;
        sequencePaused = false;
        particlesTriggered = false;

        if (characterAnimator != null)
        {
            characterAnimator.speed = 0f;
            characterAnimator.Play(0, 0, 0f);
        }

        if (glowController != null)
        {
            glowController.ResetGlow();
        }

        if (groundAuraController != null)
        {
            groundAuraController.ResetAura();
        }

        if (lightBeamController != null)
        {
            lightBeamController.ResetBeam();
        }

        StopAndClearParticles();
    }

    private void PlayParticles()
    {
        if (particleSystems == null)
            return;

        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (particleSystems[i] != null)
            {
                particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                particleSystems[i].Play(true);
            }
        }
    }

    private void PauseParticles()
    {
        if (particleSystems == null)
            return;

        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (particleSystems[i] != null)
            {
                particleSystems[i].Pause(true);
            }
        }
    }

    private void ResumeParticles()
    {
        if (particleSystems == null)
            return;

        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (particleSystems[i] != null)
            {
                particleSystems[i].Play(true);
            }
        }
    }

    private void StopAndClearParticles()
    {
        if (particleSystems == null)
            return;

        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (particleSystems[i] != null)
            {
                particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
}