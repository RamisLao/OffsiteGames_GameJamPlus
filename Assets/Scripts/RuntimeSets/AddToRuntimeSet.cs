using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AddToRuntimeSet<T> : CallOnRaiseFlag where T : class
{
    [SerializeField] private RuntimeSet<T> _set;
    [InfoBox("Plug a Component of type T in here if this script is not in the same object as the Component", InfoMessageType.Warning, "ComponentIsNull")]
    [SerializeField] private T _component;
    private bool ComponentIsNull => _component == null;
    [SerializeField] private bool _removeOnDisable = true;

    protected override void OnRaiseFlag()
    {
        if (_set == null) return;

        if (_component != null)
        {
            _set.Add(_component);
            return;
        }

        _component = GetComponent<T>();
        if (_component == null)
        {
            Debug.LogError($"Component of type {_component.GetType().Name} was not found.");
            return;
        }

        _set.Add(_component);
    }

    private void OnDestroy()
    {
        if (_set == null) return;
        if (_component != null & _removeOnDisable) _set.Remove(_component);
    }

    public void AddMyself()
    {
        _component = GetComponent<T>();
        _set.Add(_component);
    }

    public void RemoveMyself()
    {
        _component = GetComponent<T>();
        _set.Remove(_component);
    }
}
