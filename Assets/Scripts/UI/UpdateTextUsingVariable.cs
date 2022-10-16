using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTextUsingVariable : MonoBehaviour
{
    [SerializeField] private VariableInt _variable;
    [SerializeField] protected string _prefix;
    [SerializeField] protected string _suffix;

    protected TMPro.TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _variable.OnChanged.AddListener(UpdateText);
    }

    protected virtual void UpdateText(int value)
    {
        _text.text = $"{_prefix}{value}{_suffix}";
    }
}
