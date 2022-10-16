using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatCanvas : MonoBehaviour
{
    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventOnCombatActivated;
    [SerializeField] private VoidEventChannelSO _eventOnCombatDeactivated;

    [Title("References")]
    [SerializeField] private GameObject _buttonEndTurn;
    [SerializeField] private GameObject _playerStats;
    [SerializeField] private Image _handImage;

    private void Awake()
    {
        _eventOnCombatActivated.OnEventRaised += OnCombatActivated;
        _eventOnCombatDeactivated.OnEventRaised += OnCombatDeactivated;
    }

    private void OnCombatActivated()
    {
        _buttonEndTurn.SetActive(true);
        _playerStats.SetActive(true);
        _handImage.enabled = true;
    }

    private void OnCombatDeactivated()
    {
        _buttonEndTurn.SetActive(false);
        _playerStats.SetActive(false);
        _handImage.enabled = false;
    }
}
