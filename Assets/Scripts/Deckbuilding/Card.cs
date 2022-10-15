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
    [SerializeField] private CardEventChannelSO _eventCardSelected;
    [SerializeField] private CardEventChannelSO _eventCardDeselected;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = _data.Color;
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
