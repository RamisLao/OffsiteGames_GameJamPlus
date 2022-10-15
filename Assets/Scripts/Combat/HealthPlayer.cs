using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : Health
{
    [Title("Variables")]
    [SerializeField] private VariableInt _variablePlayerBlock;
    [SerializeField] private VariableInt _variablePlayerSapplings;
    [SerializeField] private VariableInt _variablePlayerExposed;
    [SerializeField] private VariableInt _variablePlayerProtected;

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

    protected override void UpdateHealthBar()
    {
    }

    protected override void UpdateBlockText()
    {
        _variablePlayerBlock.Value = _currentBlockPoints;
    }

    protected override void UpdateSapplingText()
    {
        _variablePlayerSapplings.Value = _currentAmountOfSaplings;
    }

    protected override void UpdateExposedText()
    {
        _variablePlayerExposed.Value = _currentExposedPoints;
    }

    protected override void UpdateProtectedText()
    {
        _variablePlayerProtected.Value = _currentProtectedPoints;
    }
}
