using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have a Card argument.
/// Example: Selecting a card in your hand
/// </summary>

[CreateAssetMenu(menuName = "Events/Card Event Channel")]
public class CardEventChannelSO : ScriptableObject
{
    public event UnityAction<Card> OnEventRaised;

    public void RaiseEvent(Card value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}
