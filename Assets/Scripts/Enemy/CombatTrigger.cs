using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    [Title("Camera Settings")]
    [SerializeField] private float _focusDistance;
    [SerializeField] private VariableTransform _cameraTarget;
    [SerializeField] private VariableFloat _cameraDistance;

    [Title("References")]
    [SerializeField] private List<EnemyAI> _enemiesToAddToCombat;
    [SerializeField] private List<PalmTreeGroves> _associatedPalmTreeGroves;
    [SerializeField] private Transform _playerPosition;

    [Title("Broadcasting on")]
    [SerializeField] private CombatTriggerEventChannelSO _eventOnCombatActivated;
    [SerializeField] private EnemyAIEventChannelSO _eventAddEnemyToCombat;
    [SerializeField] private Vector3EventChannelSO _eventTriggerPlayerMovementOff;
    private bool _isDead = true;
    public bool IsDead => _isDead;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collider.enabled = false;
            Invoke(nameof(CallEventOnCombat), .15f);
        }
    }

    private void CallEventOnCombat()
    {
        _cameraTarget.Value = transform;
        _cameraDistance.Value = _focusDistance;
        foreach(EnemyAI ai in _enemiesToAddToCombat)
            _eventAddEnemyToCombat.RaiseEvent(ai);

        StartCoroutine(CallOnCombatActivated());
    }

    private IEnumerator CallOnCombatActivated()
    {
        yield return new WaitForSeconds(0.5f);
        _eventOnCombatActivated.RaiseEvent(this);
        _eventTriggerPlayerMovementOff.RaiseEvent(_playerPosition.position);
    }

    public void CleanAssociatedPalmTreeGrove()
    {
        foreach (PalmTreeGroves p in _associatedPalmTreeGroves)
            p.CleanPalm();

        _isDead = false;
    }
}