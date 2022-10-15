using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    protected bool IsStunned = false;

    public virtual void ApplyStun()
    {
        IsStunned = true;
    }
}
