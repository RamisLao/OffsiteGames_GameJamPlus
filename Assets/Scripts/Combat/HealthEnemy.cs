using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : Health
{
    [Title("References")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TMPro.TextMeshProUGUI _healthText;
    [SerializeField] private TMPro.TextMeshProUGUI _blockText;
    [SerializeField] private TMPro.TextMeshProUGUI _sapplingText;
    [SerializeField] private TMPro.TextMeshProUGUI _exposedText;
    [SerializeField] private TMPro.TextMeshProUGUI _protectedText;

    public override void InitHealth()
    {
        base.InitHealth();

        _healthBar.wholeNumbers = true;
        _healthBar.minValue = 0;
        _healthBar.maxValue = _maxHealth.Value;
        UpdateHealthBar();
        UpdateBlockText();
        UpdateSapplingText();
        UpdateExposedText();
        UpdateProtectedText();
    }

    protected override void UpdateHealthBar()
    {
        _healthBar.value = CurrentHealth;
        _healthText.text = $"{CurrentHealth}/{_maxHealth.Value}";
    }

    protected override void UpdateBlockText()
    {
        _blockText.text = $"B: {_currentBlockPoints}";
    }

    protected override void UpdateSapplingText()
    {
        _sapplingText.text = $"S: {_currentAmountOfSaplings}";
    }

    protected override void UpdateExposedText()
    {
        _exposedText.text = $"E: {_currentExposedPoints}";
    }

    protected override void UpdateProtectedText()
    {
        _protectedText.text = $"P: {_currentProtectedPoints}";
    }
}
