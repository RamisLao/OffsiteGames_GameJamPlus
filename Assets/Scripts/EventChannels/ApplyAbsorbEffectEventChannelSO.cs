using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Apply Absorb Effect Event Channel")]
public class ApplyAbsorbEffectEventChannelSO : ScriptableObject
{
	public UnityAction<CardData, Agent, Agent> OnEventRaised;

	public void RaiseEvent(CardData cardData, Agent agentFrom, Agent agentTo)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(cardData, agentFrom, agentTo);
	}
}
