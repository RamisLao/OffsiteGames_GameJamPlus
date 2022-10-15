using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGameObject : Effect
{
    [SerializeField] private GameObject _go;

    public override void Reset()
    {
        Stop();
    }

    public override void Play()
    {
        if (_go == null) return;
        if (_instantiateNew)
        {
            var go = Instantiate(_go, transform.position, transform.rotation, null);
        }
        else
        {
            _go.SetActive(true);
        }
    }

    public override void Stop()
    {
        if (_go == null) return;
        _go.SetActive(false);
    }
}
