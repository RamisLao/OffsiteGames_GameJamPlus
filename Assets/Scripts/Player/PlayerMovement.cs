using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private VoidEventChannelSO _eventTriggerPlayerMovementOn;
    [SerializeField] private Vector3EventChannelSO _eventTriggerPlayerMovementOff;
    public VariableVector2 _inputDirection;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _eventTriggerPlayerMovementOn.OnEventRaised += EnableMovement;
        _eventTriggerPlayerMovementOff.OnEventRaised += TakeControlOfPlayer;

        _doMove = new DoMove();
        _doDead = new DoDead();

        time = _delaySteps;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// Save the value of player input.
    /// </summary>
    /// <param name="value"> Get the value of the player input. </param>
    private void OnMove(InputValue value)
    {
        if (!_canMove) return;

        _playerInput = value.Get<Vector2>();
        _inputDirection.Value = value.Get<Vector2>();
    }

    private void TakeControlOfPlayer(Vector3 newPos)
    {
        EnableMovement();
        StartCoroutine(MovePlayerTowardsEndDestination(newPos));
    }

    private IEnumerator MovePlayerTowardsEndDestination(Vector3 newPos)
    {
        Vector2 newPos2 = new Vector2(newPos.x, newPos.y);
        while (Vector2.Distance(_rbody.position, newPos2) > 0f)
        {
            //Vector2 direction = (newPos2 - _rbody.position).normalized;
            //_playerInput = direction;
            //_inputDirection.Value = _playerInput;
            _rbody.position = new Vector2(Mathf.Lerp(_rbody.position.x, newPos2.x, Time.deltaTime * 4f),
               Mathf.Lerp(_rbody.position.y, newPos2.y, Time.deltaTime * 4f));
            yield return null;
        }
    }

    /// <summary>
    /// Enable player movement.
    /// </summary>
    private void EnableMovement()
    {
        _canMove = !_canMove;
        if (_canMove) StopAllCoroutines();
        _playerInput = Vector2.zero;
        _inputDirection.Value = _playerInput;
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
            _doMove.Execute(_animator);
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
        else
        {
            _doMove.Cancel(_animator);
            _audioSource.Stop();
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
