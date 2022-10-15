using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private VariableInt _maxHealth;
    [SerializeField] private bool _isLocal = true;
    [ShowIf("@this._isLocal == true")] [SerializeField] [ReadOnly] private int _currentHealthLocal;
    [ShowIf("@this._isLocal == false")] [SerializeField] private VariableInt _currentHealthShared;
    public int CurrentHealth
    {
        get { return _isLocal ? _currentHealthLocal : _currentHealthShared.Value; }
        set
        {
            if (_isLocal) _currentHealthLocal = value;
            else _currentHealthShared.Value = value;
        }
    }

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

    private void InitHealth()
    {
        CurrentHealth = _maxHealth.Value;
    }

    private void SubtractHealth(int toSubtract)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - toSubtract, 0, _maxHealth.Value);
        if (CurrentHealth <= 0) _eventHealthReachedZero.RaiseEvent();
    }
}
