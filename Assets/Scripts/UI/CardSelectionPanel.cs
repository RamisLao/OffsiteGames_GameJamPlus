using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionPanel : MonoBehaviour
{
    [SerializeField] private RuntimeSetCardData _availableCards;
    [SerializeField] private RuntimeSetCardData _currentPlayerDeck;
    [SerializeField] private GameObject _panel;

    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventOpenCardSelectionPanel;

    [Title("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _eventTriggerPlayerMovementOn;

    private CardButton[] _cardButtons;

    private void Awake()
    {
        _cardButtons = _panel.transform.GetComponentsInChildren<CardButton>();
        _eventOpenCardSelectionPanel.OnEventRaised += OfferCards;
    }

    private void OfferCards()
    {
        foreach (CardButton cb in _cardButtons)
        {
            CardData cd = _availableCards.GetRandomItem();
            cb.Data = cd;
            cb.GetComponent<Image>().sprite = cd.Image;
        }

        _panel.SetActive(true);
    }

    public void SelectCard(CardButton button)
    {
        _currentPlayerDeck.Add(button.Data);
        _panel.SetActive(false);
        _eventTriggerPlayerMovementOn.RaiseEvent();
    }
}
