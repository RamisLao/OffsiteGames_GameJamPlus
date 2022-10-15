using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private RuntimeSetEnemyAI _currentEnemiesInCombat;

    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventPrepareEnemyTurn;
    [SerializeField] private VoidEventChannelSO _eventStartEnemyTurn;
    [SerializeField] private CardEventChannelSO _eventCardSelected;
    [SerializeField] private CardEventChannelSO _eventCardDeselected;

    [Title("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _eventEndEnemyTurn;

    private void Awake()
    {
        _eventPrepareEnemyTurn.OnEventRaised += PrepareEnemyTurn;
        _eventStartEnemyTurn.OnEventRaised += RunEnemyTurn;
        _eventCardSelected.OnEventRaised += CardHasBeenSelected;
        _eventCardDeselected.OnEventRaised += CardHasBeenDeselected;
    }

    private void PrepareEnemyTurn()
    {
        Debug.Log("Enemies prepared!");
    }

    private void RunEnemyTurn()
    {
        foreach(EnemyAI ai in _currentEnemiesInCombat.Items)
        {
            ai.PerformActions();
        }
        Debug.Log("All Actions performed!");

        _eventEndEnemyTurn.RaiseEvent();
    }

    private void CardHasBeenSelected(Card card)
    {
        foreach (EnemyAI ai in _currentEnemiesInCombat.Items)
        {
            ai.OnHoverIsActive = true;
        }
    }

    private void CardHasBeenDeselected(Card card)
    {
        foreach (EnemyAI ai in _currentEnemiesInCombat.Items)
        {
            ai.OnHoverIsActive = false;
        }
    }
}
