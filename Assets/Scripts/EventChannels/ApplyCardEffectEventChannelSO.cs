using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Apply Card Effect Event Channel")]
public class ApplyCardEffectEventChannelSO : ScriptableObject
{
	public UnityAction<CardData, EnemyAI> OnEventRaised;

	public void RaiseEvent(CardData cardData, EnemyAI enemy)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(cardData, enemy);
	}
}
