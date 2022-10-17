using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatCanvas : MonoBehaviour
{
    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventActivateCombatCanvas;
    [SerializeField] private VoidEventChannelSO _eventDeactivateCombatCanvas;

    [Title("References")]
    [SerializeField] private GameObject _buttonEndTurn;
    [SerializeField] private GameObject _playerStats;
    [SerializeField] private Image _handImage;
    [SerializeField] private GameObject _exploreCanvas;

    private void Awake()
    {
        _eventActivateCombatCanvas.OnEventRaised += ActivateCombatCanvas;
        _eventDeactivateCombatCanvas.OnEventRaised += DeactivateCombatCanvas;
    }

    private void ActivateCombatCanvas()
    {
        _buttonEndTurn.SetActive(true);
        _playerStats.SetActive(true);
        _handImage.enabled = true;
        _exploreCanvas.SetActive(false);
    }

    private void DeactivateCombatCanvas()
    {
        _buttonEndTurn.SetActive(false);
        _playerStats.SetActive(false);
        _handImage.enabled = false;
        _exploreCanvas.SetActive(true);
    }
}
