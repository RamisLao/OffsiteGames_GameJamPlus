using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : Agent
{
    [Title("Variables")]
    [SerializeField] private RuntimeSetEnemyAI _currentEnemiesInBattle;
    [SerializeField] private VariableEnemyAI _currentEnemyOnHover;

    [Title("References")]
    [SerializeField] private GameObject _stunImage;
    [SerializeField] private GameObject _canvas;

    [Title("Listening on")]
    [SerializeField] private EnemyAIEventChannelSO _eventOnAddEnemyToCombat;

    private SpriteRenderer _renderer;
    private bool _onHoverIsActive = false;
    public bool OnHoverIsActive
    {
        set { _onHoverIsActive = value; }
    }

    protected virtual void Awake()   
    {
        _eventOnAddEnemyToCombat.OnEventRaised += OnCombatActivated;
        _renderer = GetComponent<SpriteRenderer>();
        UpdateStunImage();
    }

    private void OnDisable()
    {
        RemoveMyselfFromBattle();
    }

    private void OnCombatActivated(EnemyAI enemy)
    {
        if (enemy == this)
        {
            _currentEnemiesInBattle.Add(this);
            _canvas.SetActive(true);
        }
    }

    private void RemoveMyselfFromBattle()
    {
        if (_currentEnemiesInBattle.Contains(this)) _currentEnemiesInBattle.Remove(this);
    }

    public void MaybePerformActions()
    {
        if (IsStunned)
        {
            IsStunned = false;
            UpdateStunImage();
            return;
        }

        PerformActions();
    }

    public override void ApplyStun()
    {
        base.ApplyStun();

        UpdateStunImage();
    }

    private void UpdateStunImage()
    {
        if (IsStunned) _stunImage.SetActive(true);
        else _stunImage.SetActive(false);
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

    public void Kill()
    {
        Destroy(gameObject);
    }
}
