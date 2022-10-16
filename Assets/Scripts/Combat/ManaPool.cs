using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPool : MonoBehaviour
{
    [SerializeField] private VariableInt _maxMana;
    [SerializeField] private VariableInt _currentMana;

    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventInitMana;
    [SerializeField] private IntEventChannelSO _eventSubtractMana;

    private void Awake()
    {
        _eventInitMana.OnEventRaised += InitMana;
        _eventSubtractMana.OnEventRaised += MaybeSubtractMana;
    }

    private void InitMana()
    {
        _currentMana.Value = _maxMana.Value;
    }

    private void MaybeSubtractMana(int toSubtract)
    {
        if (_currentMana.Value < toSubtract) return;

        _currentMana.Value -= toSubtract;
    }
}
