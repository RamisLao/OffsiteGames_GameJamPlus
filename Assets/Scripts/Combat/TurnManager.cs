using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETurn
{
    Player,
    Enemy
}

public class TurnManager : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private SettingsCombat _settingsCombat;

    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventOnCombatActivated;
    [SerializeField] private VoidEventChannelSO _eventPlayerButtonTurnEndPressed;
    [SerializeField] private VoidEventChannelSO _eventEnemiesHavePerformedActions;
    [SerializeField] private VoidEventChannelSO _eventAllEnemiesDead;

    [Title("Broadcasting on")]
    // Setup
    [SerializeField] private VoidEventChannelSO _eventActivateCombatCanvas;
    [SerializeField] private VoidEventChannelSO _eventSetupEnemyManager;
    [SerializeField] private VoidEventChannelSO _eventInitPlayerHealth;

    // Player
    [SerializeField] private VoidEventChannelSO _eventDrawCards;
    [SerializeField] private VoidEventChannelSO _eventGainMana;
    [SerializeField] private VoidEventChannelSO _eventDiscardHand;
    [SerializeField] private VoidEventChannelSO _eventPlayerTurnPreparation;
    [SerializeField] private VoidEventChannelSO _eventPlayerTurnCleanup;
    
    // Enemy
    [SerializeField] private VoidEventChannelSO _eventEnemyTurnPreparation;
    [SerializeField] private VoidEventChannelSO _eventEnemyTurnRun;

    // Deactivate
    [SerializeField] private VoidEventChannelSO _eventDeactivateCombatCanvas;
    [SerializeField] private VoidEventChannelSO _eventOnCombatDeactivated;

    private ETurn _currentTurn;
    private EnemyTrigger _currentEnemyTrigger;

    private void Awake()
    {
        _eventOnCombatActivated.OnEventRaised += ActivateCombat;
        _eventPlayerButtonTurnEndPressed.OnEventRaised += PlayerTurnEnd;
        _eventEnemiesHavePerformedActions.OnEventRaised += EnemyTurnEnd;
        _eventAllEnemiesDead.OnEventRaised += EndCombat;
    }

    private void ActivateCombat()
    {
        _eventActivateCombatCanvas.RaiseEvent();
        _eventInitPlayerHealth.RaiseEvent();
        _eventSetupEnemyManager.RaiseEvent();

        StartCombat();
    }

    private void StartCombat()
    {
        _currentTurn = ETurn.Enemy;
        NextTurn();
    }

    private void NextTurn()
    {
        switch(_currentTurn)
        {
            case ETurn.Player:
                _currentTurn = ETurn.Enemy;
                StartCoroutine(EnemyTurnPreparationCoroutine());
                break;
            case ETurn.Enemy:
                _currentTurn = ETurn.Player;
                StartCoroutine(PlayerTurnPreparationCoroutine());
                break;
        }
    }

    private IEnumerator PlayerTurnPreparationCoroutine()
    {
        if (_eventPlayerTurnPreparation != null) _eventPlayerTurnPreparation.RaiseEvent();
        if (_eventDrawCards != null) _eventDrawCards.RaiseEvent();
        if (_eventGainMana != null) _eventGainMana.RaiseEvent();
        yield return null;

        StartCoroutine(PlayerTurnStartCoroutine());
    }

    private IEnumerator PlayerTurnStartCoroutine()
    {
        yield return null;
    }

    private void PlayerTurnEnd()
    {
        StartCoroutine(PlayerTurnEndCoroutine());
    }

    private IEnumerator PlayerTurnEndCoroutine()
    {
        _eventDiscardHand.RaiseEvent();
        _eventPlayerTurnCleanup.RaiseEvent();
        yield return null;
        NextTurn();
    }

    private IEnumerator EnemyTurnPreparationCoroutine()
    {
        _eventEnemyTurnPreparation.RaiseEvent();
        yield return null;
        StartCoroutine(EnemyTurnStartCoroutine());
    }

    private IEnumerator EnemyTurnStartCoroutine()
    {
        _eventEnemyTurnRun.RaiseEvent();
        yield return null;
    }

    private void EnemyTurnEnd()
    {
        StartCoroutine(EnemyTurnEndCoroutine());
    }

    private IEnumerator EnemyTurnEndCoroutine()
    {
        yield return null;
        NextTurn();
    }

    private void EndCombat()
    {
        _eventDiscardHand.RaiseEvent();
        _eventDeactivateCombatCanvas.RaiseEvent();
        _eventOnCombatDeactivated.RaiseEvent();
    }
}
