using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Title("Variables")]
    [SerializeField] protected VariableInt _maxHealth;

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

    [SerializeField] [ReadOnly] protected int _currentBlockPoints = 0;
    [SerializeField] [ReadOnly] protected int _currentAmountOfSaplings = 0;
    [SerializeField] [ReadOnly] protected int _currentExposedPoints = 0;
    [SerializeField] [ReadOnly] protected int _currentProtectedPoints = 0;

    [Title("Broadcasting on")]
    [SerializeField] protected AgentEventChannelSO _eventHealthIsZero;

    [FoldoutGroup("Events")] public UnityEvent OnHealed;
    [FoldoutGroup("Events")] public UnityEvent OnAddBlock;
    [FoldoutGroup("Events")] public UnityEvent OnAddSappling;
    [FoldoutGroup("Events")] public UnityEvent OnSapplingDamage;
    [FoldoutGroup("Events")] public UnityEvent OnAddExposed;
    [FoldoutGroup("Events")] public UnityEvent OnAddProtected;

    public virtual void InitHealth()
    {
        CurrentHealth = _maxHealth.Value;
    }

    protected virtual void UpdateHealthBar()
    {
    }

    protected virtual void UpdateBlockText()
    {
    }

    protected virtual void UpdateSapplingText()
    {
    }

    protected virtual void UpdateExposedText()
    {
    }

    protected virtual void UpdateProtectedText()
    {
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
        OnHealed.Invoke();
    }

    public void AddBlockPoints(int toAdd)
    {
        _currentBlockPoints += toAdd;
        UpdateBlockText();
        OnAddBlock.Invoke();
    }

    public void AddSapplings(int toAdd)
    {
        _currentAmountOfSaplings += toAdd;
        UpdateSapplingText();
        OnAddSappling.Invoke();
    }

    public void ApplySapplingDamage()
    {
        if (_currentAmountOfSaplings == 0) return;
        SubtractHealth(_currentAmountOfSaplings);
        _currentAmountOfSaplings--;
        UpdateSapplingText();
        OnSapplingDamage.Invoke();
    }

    public void AddExposedPoints(int toAdd)
    {
        _currentExposedPoints += toAdd;
        UpdateExposedText();
        OnAddExposed.Invoke();
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
        OnAddProtected.Invoke();
    }

    public void MaybeSubtractProtectedPoints()
    {
        _currentProtectedPoints = Mathf.Max(_currentProtectedPoints - 1, 0);
        UpdateProtectedText();
    }

    protected virtual void HealthReachedZero()
    {
        _eventHealthIsZero.RaiseEvent(GetComponent<EnemyAI>());
    }
}
