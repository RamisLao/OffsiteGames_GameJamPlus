using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBasic : EnemyAI
{
    [SerializeField] private RuntimeSetEnemyAI _currentEnemiesInBattle;

    protected override void Awake()
    {
        base.Awake();

        AddMyselfToBattle();
    }

    private void OnDisable()
    {
        RemoveMyselfFromBattle();
    }

    private void AddMyselfToBattle()
    {
        _currentEnemiesInBattle.Add(this);
    }

    private void RemoveMyselfFromBattle()
    {
        if (_currentEnemiesInBattle.Contains(this)) _currentEnemiesInBattle.Remove(this);
    }

    public override void PerformActions()
    {
        Debug.Log("Action performed!");
    }
}
