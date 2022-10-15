using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : Health
{
    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventInitHealth;
    [SerializeField] private IntEventChannelSO _eventSubtractHealth;

    [Title("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _eventHealthReachedZero;

    private void Awake()
    {
        if (_eventInitHealth != null) _eventInitHealth.OnEventRaised += InitHealth;
        if (_eventSubtractHealth != null) _eventSubtractHealth.OnEventRaised += SubtractHealth;
    }

    protected override void HealthReachedZero()
    {
        _eventHealthReachedZero.RaiseEvent();
    }
}
