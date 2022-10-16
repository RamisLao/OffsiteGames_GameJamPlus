using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardButton : MonoBehaviour
{
    private CardData _data;
    public CardData Data
    {
        get { return _data; }
        set { _data = value; }
    }

    public UnityEvent<CardData> OnSelected;

    public void Select()
    {
        OnSelected.Invoke(_data);
    }
}
