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

    [Title("Sounds")]
    [SerializeField] private List<AudioClip> _footSteps;
    private AudioSource _audioSource;
    [SerializeField] private float _delaySteps;
    private float time;

    [Title("Listening on")]
    public VoidEventChannelSO _eventOnCombatActivated;
    public VoidEventChannelSO _eventOnCombatDeactivated;
    public VariableVector2 _inputDirection;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _eventOnCombatActivated.OnEventRaised += EnableMovement;
        _eventOnCombatDeactivated.OnEventRaised += EnableMovement;

        _doMove = new DoMove();
        _doDead = new DoDead();

        time = _delaySteps;
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
        _inputDirection.Value = value.Get<Vector2>();

        if(_playerInput.magnitude > 0 && _canMove)
        {
            _doMove.Execute(_animator);
            //PlayFootSteps();
        }
        else
        {
            _doMove.Cancel(_animator);
            _audioSource.Stop();
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

        if (_playerInput.magnitude > 0)
        {
            if (time < 0)
            {
                PlayFootSteps();
                time = _delaySteps;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    private void PlayFootSteps()
    {
        if (_audioSource.isPlaying || _footSteps.Count <= 0)
            return;

        int random = Random.Range(0, _footSteps.Count);
        _audioSource.PlayOneShot(_footSteps[random]);
    }
}
