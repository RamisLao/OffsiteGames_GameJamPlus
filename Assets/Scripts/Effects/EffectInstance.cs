using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInstance : MonoBehaviour
{
    [SerializeField] private bool _destroyAfterTime = false;
    [ShowIf("@this._destroyAfterTime == true")]
    [SerializeField] private float _timeToLive = 2;

    private void Start()
    {
        if (_destroyAfterTime)
            Destroy(gameObject, _timeToLive);
    }
}
