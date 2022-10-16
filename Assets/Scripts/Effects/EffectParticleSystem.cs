using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectParticleSystem : Effect
{
    [SerializeField] private ParticleSystem _ps;

    public override void Reset()
    {
        Stop();
    }

    public override void Play()
    {
        if (_ps == null || _ps.isPlaying) return;

        if (_instantiateNew)
        {
            var ps = Instantiate(_ps, transform.position, transform.rotation, null);
            ps.Play();
        }
        else
        {
            _ps.Play();
        }
    }

    public override void Stop()
    {
        if (_ps == null || _ps.isStopped) return;
        _ps.Stop();
    }
}
