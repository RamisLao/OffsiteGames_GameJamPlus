using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AddToVariable<T> : CallOnRaiseFlag
{
    [SerializeField] protected Variable<T> _variable;
    [InfoBox("If _t is empty, AddToVariable will try GetComponent<T>.", 
        InfoMessageType.Info)]
    [SerializeField] protected T _t;

    protected override void OnRaiseFlag()
    {
        if (!_variable) return;

        if (_t == null)
        {
            _t = GetComponent<T>();
        }

        _variable.Value = _t;
    }
}
