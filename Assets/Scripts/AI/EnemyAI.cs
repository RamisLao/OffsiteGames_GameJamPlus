using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    [Title("Variables")]
    [SerializeField] VariableEnemyAI _currentEnemyOnHover;

    private SpriteRenderer _renderer;
    private bool _onHoverIsActive = false;
    public bool OnHoverIsActive
    {
        set { _onHoverIsActive = value; }
    }

    private bool _isStunned = false;

    protected virtual void Awake()   
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void MaybePerformActions()
    {
        if (_isStunned)
        {
            _isStunned = false;
            return;
        }

        PerformActions();
    }

    public void ApplyStun()
    {
        _isStunned = true;
    }

    public abstract void PerformActions();

    private void OnMouseOver()
    {
        if (_onHoverIsActive) _renderer.color = Color.red;
        _currentEnemyOnHover.Value = this;
    }

    private void OnMouseExit()
    {
        if (_onHoverIsActive) _renderer.color = Color.white;
        _currentEnemyOnHover.Value = null;
    }
}
