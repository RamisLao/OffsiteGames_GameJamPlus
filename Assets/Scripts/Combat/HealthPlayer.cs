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
    [SerializeField] private VoidEventChannelSO _eventPlayerTurnPreparation;
    [SerializeField] private VoidEventChannelSO _eventPlayerTurnCleanup;

    private void Awake()
    {
        if (_eventInitHealth != null) _eventInitHealth.OnEventRaised += InitHealth;
        _eventPlayerTurnPreparation.OnEventRaised += PlayerTurnPreparation;
        _eventPlayerTurnCleanup.OnEventRaised += PlayerTurnCleanup;
        base.InitHealth();
    }

    public override void InitHealth()
    {
        _currentBlockPoints = 0;
        _currentAmountOfSaplings = 0;
        _currentExposedPoints = 0;
        _currentProtectedPoints = 0;
        _variablePlayerBlock.Value = 0;
        _variablePlayerSapplings.Value = 0;
        _variablePlayerExposed.Value = 0;
        _variablePlayerProtected.Value = 0;
        _variablePlayerBlock.OnChanged.Invoke(_currentBlockPoints);
        _variablePlayerSapplings.OnChanged.Invoke(_currentAmountOfSaplings);
        _variablePlayerExposed.OnChanged.Invoke(_currentExposedPoints);
        _variablePlayerProtected.OnChanged.Invoke(_currentProtectedPoints);
    }

    private void PlayerTurnPreparation()
    {
        ApplySapplingDamage();
    }

    private void PlayerTurnCleanup()
    {
        MaybeSubtractExposedPoint();
        MaybeSubtractProtectedPoints();
    }

    protected override void HealthReachedZero()
    {
        _eventHealthIsZero.RaiseEvent(GetComponent<PlayerCombat>());
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
