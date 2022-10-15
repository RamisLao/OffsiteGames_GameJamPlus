using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile
{
    private Stack<CardData> _stack = new Stack<CardData>();

    public void AddCard(CardData card)
    {
        _stack.Push(card);
    }

    public CardData GetCard()
    {
        return _stack.Pop();
    }

    public CardData PeekNextCard()
    {
        if (_stack.Count == 0) return null;
        return _stack.Peek();
    }

    public CardData[] GetPileAsArray()
    {
        return _stack.ToArray();
    }

    public int GetCardCount()
    {
        return _stack.Count;
    }

    public void Clear()
    {
        _stack.Clear();
    }

    public void Shuffle()
    {
        List<CardData> cardList = new(_stack.ToArray());
        cardList.Shuffle();
        _stack = new Stack<CardData>(cardList);
    }

    public bool IsEmpty()
    {
        return _stack.Count == 0;
    }

    public void Print()
    {
        string joinedString = string.Join(", ", _stack);
        Debug.Log(joinedString);
    }
}
