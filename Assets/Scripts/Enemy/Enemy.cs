using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Title("Enemy Settings")]
    [SerializeField] private PalmTreeGroves _palmTree;
    public bool _isDead;

    [Title("Camera Settings")]
    [SerializeField] private float _focusDistance;
    [SerializeField] private VariableTransform _cameraTarget;
    [SerializeField] private VariableFloat _cameraDistance;

    [Title("Listening on")]
    public VoidEventChannelSO _eventOnCombat;

    private void Update()
    {
        if (_isDead)
        {
            Dead();
        }
    }

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
        _cameraDistance.Value = _focusDistance;
        _eventOnCombat.RaiseEvent();
    }

    private void Dead()
    {
        _palmTree.CleanPalm();
        _eventOnCombat.RaiseEvent();
        Destroy(gameObject);
    }
}
