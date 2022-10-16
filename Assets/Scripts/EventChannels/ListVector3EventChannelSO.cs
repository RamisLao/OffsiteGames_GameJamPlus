using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/List Vector3 Event Channel")]
public class ListVector3EventChannelSO : ScriptableObject
{
	public UnityAction<List<Vector3>> OnEventRaised;

	public void RaiseEvent(List<Vector3> value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
