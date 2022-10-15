using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 playerInput;
    private Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
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
        playerInput = value.Get<Vector2>();
    }

    /// <summary>
    /// Calculates the new position of the player.
    /// </summary>
    private void Movement()
    {
        Vector2 currentPos = rbody.position;

        Vector2 targetPos = playerInput * speed;

        Vector2 newPos = currentPos + targetPos * Time.fixedDeltaTime;

        rbody.MovePosition(newPos);
    }
}
