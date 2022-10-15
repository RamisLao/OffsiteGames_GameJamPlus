using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Title("Movement Settings")]
    [SerializeField] private float _speed;
    private Vector2 _playerInput;
    private Rigidbody2D _rbody;
    private bool _canMove = true;

    //Animations
    private Animator _animator;
    private DoMove _doMove;
    private DoDead _doDead;

    [Title("Listening on")]
    public VoidEventChannelSO _eventOnCombat;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _eventOnCombat.OnEventRaised += EnableMovement;

        _doMove = new DoMove();
        _doDead = new DoDead();
    }

    private void FixedUpdate()
    {
        if(_canMove)
            Movement();
    }

    /// <summary>
    /// Save the value of player input.
    /// </summary>
    /// <param name="value"> Get the value of the player input. </param>
    private void OnMove(InputValue value)
    {
        _playerInput = value.Get<Vector2>();

        if(_playerInput.magnitude > 0)
        {
            _doMove.Execute(_animator);
        }
        else
        {
            _doMove.Cancel(_animator);
        }
    }

    /// <summary>
    /// Enable player movement.
    /// </summary>
    private void EnableMovement()
    {
        _canMove = !_canMove;
    }

    /// <summary>
    /// Calculates the new position of the player.
    /// </summary>
    private void Movement()
    {
        Vector2 currentPos = _rbody.position;

        Vector2 targetPos = _playerInput * _speed;

        Vector2 newPos = currentPos + targetPos * Time.fixedDeltaTime;

        _rbody.MovePosition(newPos);
    }
}
