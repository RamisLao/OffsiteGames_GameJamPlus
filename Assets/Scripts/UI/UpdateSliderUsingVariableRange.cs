using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderUsingVariableRange : MonoBehaviour
{
    [SerializeField] private VariableInt _currentValue;
    [SerializeField] private VariableInt _maxValue;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _currentValue.OnChanged.AddListener(ValueChanged);
    }

    private void Start()
    {
        _slider.wholeNumbers = true;
        _slider.minValue = 0;
        _slider.maxValue = _maxValue.Value;
        _slider.value = _currentValue.Value;
    }

    private void ValueChanged(int value)
    {
        _slider.value = value;
    }
}
