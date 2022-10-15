using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CardData _data;
    public CardData Data
    {
        get { return _data; }
        set { _data = value; }
    }

    [Title("Text")]
    [SerializeField] private TMPro.TextMeshProUGUI _manaCostText;
    [SerializeField] private TMPro.TextMeshProUGUI _nameText;
    [SerializeField] private TMPro.TextMeshProUGUI _descriptionText;

    [Title("Broadcasting on")]
    [SerializeField] private CardEventChannelSO _eventCardSelected;
    [SerializeField] private CardEventChannelSO _eventCardDeselected;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        SetupCard();
    }

    private void SetupCard()
    {
        _image.color = _data.Color;
        _manaCostText.text = $"{_data.ManaCost}";
        _nameText.text = _data.Name;
        _descriptionText.text = _data.Description;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _eventCardDeselected.RaiseEvent(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _eventCardSelected.RaiseEvent(this);
    }
}
