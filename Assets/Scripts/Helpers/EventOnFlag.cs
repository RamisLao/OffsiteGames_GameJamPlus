using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnFlag : CallOnRaiseFlag
{
    public UnityEvent OnFlag;

    protected override void OnRaiseFlag()
    {
        OnFlag.Invoke();
    }
}
