using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private bool _onHoverIsActive = false;
    public bool OnHoverIsActive
    {
        set { _onHoverIsActive = value; }
    }

    protected virtual void Awake()   
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public abstract void PerformActions();

    private void OnMouseOver()
    {
        if (_onHoverIsActive) _renderer.color = Color.red;
    }

    private void OnMouseExit()
    {
        if (_onHoverIsActive) _renderer.color = Color.white;
    }
}
