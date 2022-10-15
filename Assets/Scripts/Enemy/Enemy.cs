using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Title("Listening on")]
    public VoidEventChannelSO _eventOnCombat;
    public VariableTransform _cameraTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke(nameof(CallEventOnCombat), .15f);
        }
    }

    private void CallEventOnCombat()
    {
        _cameraTarget.Value = transform;
        _eventOnCombat.RaiseEvent();
    }
}
