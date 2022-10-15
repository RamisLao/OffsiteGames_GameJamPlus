using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Title("Variables")]
    [SerializeField] private VariableInt _maxHealth;

    [Title("Debugging")]
    [SerializeField] private bool _isLocal = true;
    [ShowIf("@this._isLocal == true")] [SerializeField] [ReadOnly] private int _currentHealthLocal;
    [ShowIf("@this._isLocal == false")] [SerializeField] private VariableInt _currentHealthShared;
    public int CurrentHealth
    {
        get { return _isLocal ? _currentHealthLocal : _currentHealthShared.Value; }
        set
        {
            if (_isLocal) _currentHealthLocal = value;
            else _currentHealthShared.Value = value;
        }
    }

    [SerializeField] [ReadOnly] private int _currentBlockPoints = 0;
    [SerializeField] [ReadOnly] private int _currentAmountOfSaplings = 0;
    [SerializeField] [ReadOnly] private int _currentExposedPoints = 0;
    [SerializeField] [ReadOnly] private int _currentProtectedPoints = 0;

    [Title("References")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TMPro.TextMeshProUGUI _healthText;
    [SerializeField] private TMPro.TextMeshProUGUI _blockText;
    [SerializeField] private TMPro.TextMeshProUGUI _sapplingText;
    [SerializeField] private TMPro.TextMeshProUGUI _exposedText;
    [SerializeField] private TMPro.TextMeshProUGUI _protectedText;

    [Title("Broadcasting on")]
    [SerializeField] private EnemyAIEventChannelSO _eventEnemyIsDead;

    public void InitHealth()
    {
        CurrentHealth = _maxHealth.Value;
        _healthBar.wholeNumbers = true;
        _healthBar.minValue = 0;
        _healthBar.maxValue = _maxHealth.Value;
        UpdateHealthBar();
        UpdateBlockText();
        UpdateSapplingText();
        UpdateExposedText();
        UpdateProtectedText();
    }

    private void UpdateHealthBar()
    {
        _healthBar.value = CurrentHealth;
        _healthText.text = $"{CurrentHealth}/{_maxHealth.Value}";
    }

    private void UpdateBlockText()
    {
        _blockText.text = $"B: {_currentBlockPoints}";
    }
    
    private void UpdateSapplingText()
    {
        _sapplingText.text = $"S: {_currentAmountOfSaplings}";
    }

    private void UpdateExposedText()
    {
        _exposedText.text = $"E: {_currentExposedPoints}";
    }

    private void UpdateProtectedText()
    {
        _protectedText.text = $"P: {_currentProtectedPoints}";
    }

    public void SubtractHealth(int toSubtract)
    {
        int remainingExposed = Mathf.Max(_currentExposedPoints - _currentProtectedPoints, 0);
        int remainingProtected = Mathf.Max(_currentProtectedPoints - _currentExposedPoints, 0);

        if (remainingExposed > 0)
        {
            float percentageToAdd = _currentExposedPoints * 0.25f;
            toSubtract = Mathf.FloorToInt(toSubtract + (toSubtract * percentageToAdd));
        }
        else if (remainingProtected > 0)
        {
            float percentageToSubtract = _currentProtectedPoints * 0.25f;
            toSubtract = Mathf.FloorToInt(toSubtract - (toSubtract * percentageToSubtract));
            toSubtract = Mathf.Max(toSubtract, 0);
        }

        int remainingAmountToSubtract = Mathf.Max(toSubtract - _currentBlockPoints, 0);
        _currentBlockPoints = Mathf.Max(_currentBlockPoints - toSubtract, 0);
        UpdateBlockText();

        CurrentHealth = Mathf.Clamp(CurrentHealth - remainingAmountToSubtract, 0, _maxHealth.Value);
        UpdateHealthBar();
        if (CurrentHealth <= 0) HealthReachedZero();
    }

    public void AddHealth(int toAdd)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + toAdd, _maxHealth.Value);
        UpdateHealthBar();
    }

    public void AddBlockPoints(int toAdd)
    {
        _currentBlockPoints += toAdd;
        UpdateBlockText();
    }

    public void AddSapplings(int toAdd)
    {
        _currentAmountOfSaplings += toAdd;
        UpdateSapplingText();
    }

    public void ApplySapplingDamage()
    {
        if (_currentAmountOfSaplings == 0) return;
        SubtractHealth(_currentAmountOfSaplings);
        _currentAmountOfSaplings--;
        UpdateSapplingText();
    }

    public void AddExposedPoints(int toAdd)
    {
        _currentExposedPoints += toAdd;
        UpdateExposedText();
    }

    public void MaybeSubtractExposedPoint()
    {
        _currentExposedPoints = Mathf.Max(_currentExposedPoints - 1, 0);
        UpdateExposedText();
    }

    public void AddProtectedPoints(int toAdd)
    {
        _currentProtectedPoints += toAdd;
        UpdateProtectedText();
    }

    public void MaybeSubtractProtectedPoints()
    {
        _currentProtectedPoints = Mathf.Max(_currentProtectedPoints - 1, 0);
        UpdateProtectedText();
    }

    protected virtual void HealthReachedZero()
    {
        _eventEnemyIsDead.RaiseEvent(GetComponent<EnemyAI>());
    }
}
