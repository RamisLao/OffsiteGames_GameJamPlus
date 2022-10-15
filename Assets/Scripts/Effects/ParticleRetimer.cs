#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RetimeParticle
{
    public ParticleSystem particle;
    [Header("Particle Duration")]
    public float baseDuration = 1f;
    public float newDuration = 1f;
    [Header("Particle Delayed Start")]
    public float delayDuration = 0f;
}

public class ParticleRetimer : MonoBehaviour
{
    [SerializeField] public RetimeParticle[] ParticlesToRetime;
    private ParticleSystem[] ps;
    public void ApplyRetiming()
    {
        Debug.Log("Starting retiming process on " + this.gameObject.name + "...");
        for (int i = 0; i < ParticlesToRetime.Length; i++)
        {
            ps = ParticlesToRetime[i].particle.gameObject.GetComponentsInChildren<ParticleSystem>();

            for (int j = 0; j < ps.Length; j++)
            {
                ParticleSystem.MainModule psMain = ps[j].main;
                if (ParticlesToRetime[i].baseDuration != 0f && ParticlesToRetime[i].newDuration != 0f)
                {
                    psMain.simulationSpeed = ParticlesToRetime[i].baseDuration / ParticlesToRetime[i].newDuration;
                }
                else
                {
                    psMain.simulationSpeed = 1f;
                    ParticlesToRetime[i].newDuration = ParticlesToRetime[i].baseDuration;
                }
                psMain.startDelay = ParticlesToRetime[i].delayDuration * psMain.simulationSpeed;
            }
        }
        Debug.Log("Retiming applied on " + this.gameObject.name);
    }

    public void OnValidate()
    {
        ApplyRetiming();
    }
}
#endif