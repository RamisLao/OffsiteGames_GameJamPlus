using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditionTrigger : MonoBehaviour
{
    [SerializeField] private RuntimeSetCombatTrigger _combatTriggersInGame;
    [SerializeField] private VoidEventChannelSO _eventGameIsWon;
    [SerializeField] private VariableInt _totalNumberOfCombatTriggers;
    [SerializeField] private VariableInt _cleanedCombatTriggers;

    private bool _gameIsWon = false;

    private void Start()
    {
        _totalNumberOfCombatTriggers.Value = _combatTriggersInGame.Count;
        _cleanedCombatTriggers.Value = 0;
    }

    private void Update()
    {
        if (!_gameIsWon)
        {
            _gameIsWon = true;
            int totalCleanedTriggers = 0;
            foreach (CombatTrigger ct in _combatTriggersInGame.Items)
            {
                if (ct.IsDead)
                {
                    _gameIsWon = false;
                }
                else
                {
                    totalCleanedTriggers++;
                }
            }

            _cleanedCombatTriggers.Value = totalCleanedTriggers;
            if (_gameIsWon) _eventGameIsWon.RaiseEvent();
        }
    }
}
