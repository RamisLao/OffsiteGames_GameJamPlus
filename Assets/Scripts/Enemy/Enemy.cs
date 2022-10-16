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

    [Title("Variables")]
    [SerializeField] private VariableInt _room;

    [Title("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _eventOnCombatActivated;
    [SerializeField] private EnemyAIEventChannelSO _eventAddEnemyToCombat;

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
        _eventOnCombatActivated.RaiseEvent();
        _eventAddEnemyToCombat.RaiseEvent(GetComponent<EnemyAI>());
    }

    private void Dead()
    {
        _palmTree.CleanPalm();
        _eventOnCombatActivated.RaiseEvent();

        if (_room != null)
            _room.Value += 1;

        Destroy(gameObject);
    }
}
