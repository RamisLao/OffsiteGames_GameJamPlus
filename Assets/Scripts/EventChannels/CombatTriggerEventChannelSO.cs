using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Combat Trigger Event Channel")]
public class CombatTriggerEventChannelSO : ScriptableObject
{
	public UnityAction<CombatTrigger> OnEventRaised;

	public void RaiseEvent(CombatTrigger value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
