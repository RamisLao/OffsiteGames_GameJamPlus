using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Apply Card Effect Event Channel")]
public class ApplyCardEffectEventChannelSO : ScriptableObject
{
	public UnityAction<CardData, Agent> OnEventRaised;

	public void RaiseEvent(CardData cardData, Agent agent)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(cardData, agent);
	}
}
