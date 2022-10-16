using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private SettingsDeckBuilding _settingsDeckBuilding;

    [Title("References")]
    [SerializeField] private CardReleaseLimits _releaseLimits;

    [Title("Prefabs")]
    [SerializeField] private Card _cardPrefab;

    [Title("Variables")]
    [SerializeField] private RuntimeSetCardData _playerStartingDeck;
    [SerializeField] private RuntimeSetCardData _playerInGameDeck;
    [SerializeField] private VariableCard _selectedCard;
    [SerializeField] private VariableInt _currentMana;
    [SerializeField] private VariableEnemyAI _currentEnemyOnHover;
    [SerializeField] private VariablePlayerCombat _player;

    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventDrawCards;
    [SerializeField] private VoidEventChannelSO _eventDiscardCards;
    [SerializeField] private CardEventChannelSO _eventCardPointerDown;
    [SerializeField] private CardEventChannelSO _eventCardPointerUp;

    [Title("Broadcasting on")]
    [SerializeField] private IntEventChannelSO _eventSubtractMana;
    [SerializeField] private CardEventChannelSO _eventCardSelected;
    [SerializeField] private CardEventChannelSO _eventCardDeselected;
    [SerializeField] private CardEventChannelSO _eventCardEffectActivatedForEnemies;
    [SerializeField] private ApplyCardEffectEventChannelSO _eventApplyCardEffect;
    [SerializeField] private ApplyAbsorbEffectEventChannelSO _eventApplyAbsorbEffect;

    private Vector2 _selectedCardFormerPosition;
    private List<Card> _currentCards;
    private Pile _drawPile;
    private Pile _discardPile;
    private bool _waitingToDiscard = false;
    private int _amountOfCardsToDiscard = 0;

    private void Awake()
    {
        _currentCards = new();
        _eventCardPointerDown.OnEventRaised += MaybeSelectCard;
        _eventCardPointerUp.OnEventRaised += MaybeReleaseCard;
        _eventDrawCards.OnEventRaised += InitializeHand;
        _eventDiscardCards.OnEventRaised += DiscardHand;

        _playerInGameDeck.Clear();
        foreach (CardData cd in _playerStartingDeck.Items) _playerInGameDeck.Add(cd);

        InitializePiles();
    }

    private void Update()
    {
        if (!_selectedCard.IsEmpty)
        {
            Vector2 mousePos = Input.mousePosition;
            _selectedCard.Value.transform.position = mousePos;
        }
    }

    private void InitializePiles()
    {
        _drawPile = new();
        _discardPile = new();

        _playerStartingDeck.Shuffle();
        foreach (CardData c in _playerStartingDeck.Items)
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

    private void DiscardHand()
    {
        foreach (Card c in _currentCards)
        {
            AddCardToDiscardPile(c.Data);
            Destroy(c.gameObject);
        }

        _currentCards.Clear();
    }

    private void AddCardToHand()
    {
        CardData cd = GetCardFromDrawPile();
        Card iCard = Instantiate(_cardPrefab, transform);
        iCard.Data = cd;
        _currentCards.Add(iCard);
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

    private void MaybeSelectCard(Card card)
    {
        if (_waitingToDiscard)
        {
            AddCardToDiscardPile(card.Data);
            Destroy(card.gameObject);
            _amountOfCardsToDiscard--;

            if (_amountOfCardsToDiscard == 0) _waitingToDiscard = false;
            return;
        }

        if (_currentMana.Value < card.Data.ManaCost) return;

        _selectedCardFormerPosition = card.transform.position;
        _selectedCard.Value = card;
        _currentCards.Remove(card);
        _eventCardSelected.RaiseEvent(card);
    }

    private void MaybeReleaseCard(Card card)
    {
        if (_selectedCard.Value == card)
        {
            if (!_releaseLimits.IsMouseInsideLimits())
            {
                if (_selectedCard.Value.Data.AppliesToSelf ||
                    _selectedCard.Value.Data.TargetsAll || 
                    _selectedCard.Value.Data.DrawCards ||
                    _selectedCard.Value.Data.DiscardCards ||
                    !_currentEnemyOnHover.IsEmpty)
                {
                    _eventSubtractMana.RaiseEvent(card.Data.ManaCost);
                    AddCardToDiscardPile(card.Data);
                    Destroy(_selectedCard.Value.gameObject);
                    _selectedCard.Value = null;
                    //AddCardToHand();

                    ActivateCardEffect(card);
                }
                else
                {
                    ReturnCardToHand(card);
                }
            }
            else
            {
                ReturnCardToHand(card);
            }

        }
    }

    private void ActivateCardEffect(Card card)
    {
        if (card.Data.DrawCards)
        {
            for (int i = 0; i < card.Data.NumOfCardsToDraw; i++) AddCardToHand();
        }
        else if (card.Data.DiscardCards)
        {
            _waitingToDiscard = true;
            _amountOfCardsToDiscard = card.Data.NumOfCardsToDiscard;
        }
        else if (card.Data.AppliesToSelf)
            _eventApplyCardEffect.RaiseEvent(card.Data, _player.Value);
        else if (card.Data.AppliesAbsorb)
            _eventApplyAbsorbEffect.RaiseEvent(card.Data, _player.Value, _currentEnemyOnHover.Value);
        else
            _eventCardEffectActivatedForEnemies.RaiseEvent(card);
    }

    private void ReturnCardToHand(Card card)
    {
        _selectedCard.Value.transform.position = _selectedCardFormerPosition;
        _selectedCardFormerPosition = Vector2.zero;
        _selectedCard.Value = null;
        _eventCardDeselected.RaiseEvent(card);
    }
}
