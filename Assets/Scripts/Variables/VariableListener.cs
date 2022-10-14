using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VariableListener<T> : MonoBehaviour
{
    [SerializeField] private Variable<T> _variable;
    public UnityEvent<T> OnChanged;

    private void OnEnable()
    {
        _variable.OnChanged.AddListener(OnVariableChanged);
    }

    private void OnDisable()
    {
        _variable.OnChanged.RemoveListener(OnVariableChanged);
    }

    protected virtual void OnVariableChanged(T value)
    {
        OnChanged.Invoke(value);
    }
}
