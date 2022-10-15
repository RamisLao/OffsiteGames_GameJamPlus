using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTextUsingVariable : MonoBehaviour
{
    [SerializeField] private VariableInt _variable;

    private TMPro.TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _variable.OnChanged += UpdateText;
    }

    private void UpdateText(int value)
    {
        _text.text = $"{value}";
    }
}
