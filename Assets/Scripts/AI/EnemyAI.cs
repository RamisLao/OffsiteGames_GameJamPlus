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

    private SpriteRenderer _renderer;
    private bool _onHoverIsActive = false;
    public bool OnHoverIsActive
    {
        set { _onHoverIsActive = value; }
    }

    protected virtual void Awake()   
    {
        _renderer = GetComponent<SpriteRenderer>();
        UpdateStunImage();
        AddMyselfToBattle();
    }

    private void OnDisable()
    {
        RemoveMyselfFromBattle();
    }

    private void AddMyselfToBattle()
    {
        _currentEnemiesInBattle.Add(this);
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
