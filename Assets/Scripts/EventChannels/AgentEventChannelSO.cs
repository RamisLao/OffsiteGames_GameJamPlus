using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Agent Event Channel")]
public class AgentEventChannelSO : ScriptableObject
{
	public UnityAction<Agent> OnEventRaised;

	public void RaiseEvent(Agent value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
