using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private SettingsDeckBuilding _settingsDeckBuilding;

    [Title("Prefabs")]
    [SerializeField] private Card _cardPrefab;

    [Title("Variables")]
    [SerializeField] private RuntimeSetCardData _playerDeck;

    [Title("Listening on")]
    [SerializeField] private CardEventChannelSO _eventCardSelected;
    [SerializeField] private CardEventChannelSO _eventCardDeselected;

    private List<Card> _currentCards;
    private Pile _drawPile;
    private Pile _discardPile;
    private Card _selectedCard;

    private void Awake()
    {
        _currentCards = new();
        _eventCardSelected.OnEventRaised += MaybeSelectCard;
        _eventCardDeselected.OnEventRaised += MaybeReleaseCard;

        InitializePiles();
        InitializeHand();
    }

    private void Update()
    {
        if (_selectedCard != null)
        {
            Vector2 mousePos = Input.mousePosition;
            _selectedCard.transform.position = mousePos;
        }
    }

    private void InitializePiles()
    {
        _drawPile = new();
        _discardPile = new();

        _playerDeck.Shuffle();
        foreach (CardData c in _playerDeck.Items)
        {
            AddCardToDrawPile(c);
        }
    }

    private void InitializeHand()
    {
        for (int i = 0; i < _settingsDeckBuilding.StartingHandSize; i++)
        {
            AddCardToHand();
        }
    }

    private void AddCardToHand()
    {
        CardData cd = GetCardFromDrawPile();
        Card iCard = Instantiate(_cardPrefab, transform);
        iCard.Data = cd;
        _currentCards.Add(iCard);

        Debug.Log("Draw Pile");
        _drawPile.Print();
        Debug.Log("Discard Pile");
        _discardPile.Print();
    }

    private CardData GetCardFromDrawPile()
    {
        if (_drawPile.GetCardCount() <= 0)
        {
            ReturnDiscardPileToDrawPile();
        }

        if (_drawPile.GetCardCount() <= 0) return null;
        else return _drawPile.GetCard();
    }

    private void AddCardToDiscardPile(CardData card)
    {
        _discardPile.AddCard(card);
    }

    private void AddCardToDrawPile(CardData card)
    {
        _drawPile.AddCard(card);
    }

    private void ReturnDiscardPileToDrawPile()
    {
        while (!_discardPile.IsEmpty())
        {
            _drawPile.AddCard(_discardPile.GetCard());
        }
        _discardPile.Clear();
        _drawPile.Shuffle();
    }

    private CardData PeekNextCardInDrawPile()
    {
        return _drawPile.PeekNextCard();
    }

    private void MaybeSelectCard(Card card)
    {
        _selectedCard = card;
        _currentCards.Remove(card);
    }

    private void MaybeReleaseCard(Card card)
    {
        if (_selectedCard == card)
        {
            AddCardToDiscardPile(card.Data);
            Destroy(_selectedCard.gameObject);
            _selectedCard = null;
            AddCardToHand();
        }
    }
}
