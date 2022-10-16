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
    [SerializeField] private VoidEventChannelSO _eventPlayerTurnEnded;
    [SerializeField] private VoidEventChannelSO _eventEnemyTurnEnded;
    [SerializeField] private VoidEventChannelSO _eventAllEnemiesDead;

    [Title("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _eventCombatStarted;
    [SerializeField] private VoidEventChannelSO _eventRemoveLeftOverBlock;
    [SerializeField] private VoidEventChannelSO _eventInitPlayerHealth;
    [SerializeField] private VoidEventChannelSO _eventDrawCards;
    [SerializeField] private VoidEventChannelSO _eventGainMana;
    [SerializeField] private VoidEventChannelSO _eventDiscardHand;
    [SerializeField] private VoidEventChannelSO _eventPrepareEnemyTurn;
    [SerializeField] private VoidEventChannelSO _eventStartEnemyTurn;
    [SerializeField] private VoidEventChannelSO _eventOnCombatDeactivated;

    private ETurn _currentTurn;

    private void Awake()
    {
        _eventPlayerTurnEnded.OnEventRaised += PlayerTurnEnd;
        _eventEnemyTurnEnded.OnEventRaised += EnemyTurnEnd;
        _eventOnCombatActivated.OnEventRaised += StartCombat;
        _eventAllEnemiesDead.OnEventRaised += EndCombat;
    }

    [Button("Start")]
    private void StartCombat()
    {
        _eventCombatStarted.RaiseEvent();
        _eventInitPlayerHealth.RaiseEvent();
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
        if (_eventRemoveLeftOverBlock != null) _eventRemoveLeftOverBlock.RaiseEvent();
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
        yield return null;
        NextTurn();
    }

    private IEnumerator EnemyTurnPreparationCoroutine()
    {
        _eventPrepareEnemyTurn.RaiseEvent();
        yield return null;
        StartCoroutine(EnemyTurnStartCoroutine());
    }

    private IEnumerator EnemyTurnStartCoroutine()
    {
        _eventStartEnemyTurn.RaiseEvent();
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
        _eventOnCombatDeactivated.RaiseEvent();
    }
}
