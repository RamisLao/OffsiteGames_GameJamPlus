using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCounter : MonoBehaviour
{
    [SerializeField] private VariableInt _currentMana;

    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventManaChanged;

    private TMPro.TextMeshProUGUI _manaText;

    private void Awake()
    {
        _manaText = GetComponent<TMPro.TextMeshProUGUI>();
        _eventManaChanged.OnEventRaised += UpdateManaText;
    }

    private void UpdateManaText()
    {
        _manaText.text = $"{_currentMana.Value}";
    }
}
