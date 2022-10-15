using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/EnemyAI Event Channel")]
public class EnemyAIEventChannelSO : ScriptableObject
{
	public UnityAction<EnemyAI> OnEventRaised;

	public void RaiseEvent(EnemyAI value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
