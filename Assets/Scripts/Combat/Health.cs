using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private VariableInt _maxHealth;
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

    protected void InitHealth()
    {
        CurrentHealth = _maxHealth.Value;
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

        CurrentHealth = Mathf.Clamp(CurrentHealth - remainingAmountToSubtract, 0, _maxHealth.Value);
        if (CurrentHealth <= 0) HealthReachedZero();
    }

    public void AddHealth(int toAdd)
    {
        CurrentHealth += toAdd;
    }

    public void AddBlockPoints(int toAdd)
    {
        _currentBlockPoints += toAdd;
    }

    public void AddSapplings(int toAdd)
    {
        _currentAmountOfSaplings += toAdd;
    }

    public void ApplySapplingDamage()
    {
        if (_currentAmountOfSaplings == 0) return;
        SubtractHealth(_currentAmountOfSaplings);
    }

    public void AddExposedPoints(int toAdd)
    {
        _currentExposedPoints += toAdd;
    }

    public void MaybeSubtractExposedPoint()
    {
        _currentExposedPoints = Mathf.Max(_currentExposedPoints - 1, 0);
    }

    public void AddProtectedPoints(int toAdd)
    {
        _currentProtectedPoints += toAdd;
    }

    public void MaybeSubtractProtectedPoints()
    {
        _currentProtectedPoints = Mathf.Max(_currentProtectedPoints - 1, 0);
    }

    protected virtual void HealthReachedZero()
    {

    }
}
