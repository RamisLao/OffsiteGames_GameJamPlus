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
    [SerializeField] private VoidEventChannelSO _eventPlayerTurnEnded;

    [Title("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _eventRemoveLeftOverBlock;
    [SerializeField] private VoidEventChannelSO _eventDrawCards;
    [SerializeField] private VoidEventChannelSO _eventGainMana;
    [SerializeField] private VoidEventChannelSO _eventDiscardHand;

    private ETurn _currentTurn;

    private void Awake()
    {
        _eventPlayerTurnEnded.OnEventRaised += PlayerTurnEnd;
    }

    private void Start()
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
    }

    private IEnumerator EnemyTurnPreparationCoroutine()
    {
        yield return null;
    }

    private IEnumerator EnemyTurnStartCoroutine()
    {
        yield return null;
    }

    private IEnumerator EnemyTurnEndCoroutine()
    {
        yield return null;
    }
}
