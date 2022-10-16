using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionPanel : MonoBehaviour
{
    [SerializeField] private RuntimeSetCardData _availableCards;
    [SerializeField] private RuntimeSetCardData _currentPlayerDeck;
    [SerializeField] private GameObject _panel;

    private CardButton[] _cardButtons;

    private void Awake()
    {
        _cardButtons = _panel.transform.GetComponentsInChildren<CardButton>();
    }

    
}
