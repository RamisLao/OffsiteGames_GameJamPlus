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

    [Title("Broadcasting on")]
    [SerializeField] private CardEventChannelSO _eventCardPointerDown;
    [SerializeField] private CardEventChannelSO _eventCardPointerUp;

    private Image _image;
    private bool _isSelectable = true;
    public bool IsSelectable
    {
        set { _isSelectable = value; }
    }

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
        _image.sprite = _data.Image;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isSelectable) return;

        _eventCardPointerDown.RaiseEvent(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isSelectable) return;

        _eventCardPointerUp.RaiseEvent(this);
    }

}
