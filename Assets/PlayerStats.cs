using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private VariableInt _playerCurrentHealth;
    [SerializeField] private VariableInt _playerMaxHealth;
    [SerializeField] private VariableInt _playerCurrentBlock;
    [SerializeField] private VariableInt _playerCurrentSappling;
    [SerializeField] private VariableInt _playerCurrentProtected;
    [SerializeField] private VariableInt _playerCurrentExposed;
    [SerializeField] private VariableInt _playerCurrentMana;
    [SerializeField] private Image _blockImage;
    [SerializeField] private Image _sapplingImage;
    [SerializeField] private Image _protectedImage;
    [SerializeField] private Image _exposedImage;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TMPro.TextMeshProUGUI _healthText;
    [SerializeField] private TMPro.TextMeshProUGUI _blockText;
    [SerializeField] private TMPro.TextMeshProUGUI _sapplingText;
    [SerializeField] private TMPro.TextMeshProUGUI _protectedText;
    [SerializeField] private TMPro.TextMeshProUGUI _exposedText;
    [SerializeField] private TMPro.TextMeshProUGUI _manaText;

    private void Awake()
    {
        _playerCurrentHealth.OnChanged.AddListener(UpdateHealthBar);
        _playerCurrentBlock.OnChanged.AddListener(UpdateBlockText);
        _playerCurrentSappling.OnChanged.AddListener(UpdateSapplingText);
        _playerCurrentProtected.OnChanged.AddListener(UpdateProtectedText);
        _playerCurrentExposed.OnChanged.AddListener(UpdateExposedText);
        _playerCurrentMana.OnChanged.AddListener(UpdateMana);
    }

    private void UpdateHealthBar(int value)
    {
        _healthBar.value = value;
        _healthText.text = $"{value}/{_playerMaxHealth.Value}";
    }

    private void UpdateMana(int value)
    {
        _manaText.text = $"{_playerCurrentMana.Value}";
    }

    private void UpdateBlockText(int value)
    {
        _blockText.text = $"{value}";
        if (value == 0) _blockImage.gameObject.SetActive(false);
        else _blockImage.gameObject.SetActive(true);
    }

    private void UpdateSapplingText(int value)
    {
        _sapplingText.text = $"{value}";
        if (value == 0) _sapplingImage.gameObject.SetActive(false);
        else _sapplingImage.gameObject.SetActive(true);
    }

    private void UpdateExposedText(int value)
    {
        _exposedText.text = $"{value}";
        if (value == 0) _exposedImage.gameObject.SetActive(false);
        else _exposedImage.gameObject.SetActive(true);
    }

    private void UpdateProtectedText(int value)
    {
        _protectedText.text = $"{value}";
        if (value == 0) _protectedImage.gameObject.SetActive(false);
        else _protectedImage.gameObject.SetActive(true);
    }
}
