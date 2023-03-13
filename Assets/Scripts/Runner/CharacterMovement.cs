using System;
using DefaultNamespace.Runner;
using UnityEngine;

public enum CharacterState
{
    Idle,
    Movement,
    Jumping,
    Falling,
}

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float speed;


    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontal, 0f, forward);
        Vector3 movement = input * (speed * Time.fixedDeltaTime);
        Vector3 pos = transform.position + movement;
        playerRigidbody.MovePosition(pos);
        SetState(CharacterState.Movement, forward);
    }

    private void SetState(CharacterState state, float value)
    {
        playerAnimator.SetState(state, value);
    }
}