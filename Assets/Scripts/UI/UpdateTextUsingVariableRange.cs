using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTextUsingVariableRange : UpdateTextUsingVariable
{
    [SerializeField] private VariableInt _variableUpperRange;

    protected override void UpdateText(int value)
    {
        _text.text = $"{_prefix}{value}/{_variableUpperRange.Value}{_suffix}";
    }
}
