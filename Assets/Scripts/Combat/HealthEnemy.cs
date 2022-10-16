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
    [SerializeField] private Image _blockImage;
    [SerializeField] private TMPro.TextMeshProUGUI _blockText;
    [SerializeField] private Image _sapplingImage;
    [SerializeField] private TMPro.TextMeshProUGUI _sapplingText;
    [SerializeField] private Image _exposedImage;
    [SerializeField] private TMPro.TextMeshProUGUI _exposedText;
    [SerializeField] private Image _protectedImage;
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
        _blockText.text = $"{_currentBlockPoints}";
        if (_currentBlockPoints == 0) _blockImage.gameObject.SetActive(false);
        else _blockImage.gameObject.SetActive(true);
    }

    protected override void UpdateSapplingText()
    {
        _sapplingText.text = $"{_currentAmountOfSaplings}";
        if (_currentAmountOfSaplings == 0) _sapplingImage.gameObject.SetActive(false);
        else _sapplingImage.gameObject.SetActive(true);
    }

    protected override void UpdateExposedText()
    {
        _exposedText.text = $"{_currentExposedPoints}";
        if (_currentExposedPoints == 0) _exposedImage.gameObject.SetActive(false);
        else _exposedImage.gameObject.SetActive(true);
    }

    protected override void UpdateProtectedText()
    {
        _protectedText.text = $"{_currentProtectedPoints}";
        if (_currentProtectedPoints == 0) _protectedImage.gameObject.SetActive(false);
        else _protectedImage.gameObject.SetActive(true);
    }
}
