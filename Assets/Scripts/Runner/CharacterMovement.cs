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
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float speed;
    private static bool isGrounded = true;

    [SerializeField] private float groundCheckDistance = 0.1f;

    [SerializeField] private float jumpForce;
    [SerializeField] private float forwardJumpMultiplier = 0.5f;

    [SerializeField] private float borderX;


    private void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        IsGrounded();
        Move();
    }

    private void Move()
    {
        if (isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");
            Vector3 input = new(horizontal, 0f, forward);
            Vector3 movement = input * (speed * Time.fixedDeltaTime);
            Vector3 pos = transform.position + movement;
            if (pos.x > borderX)
                pos.x = borderX;
            
            else if (pos.x < -borderX)
                pos.x = -borderX;
            
            playerRigidbody.MovePosition(pos);
            SetState(CharacterState.Movement, forward);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");

            Vector3 jumpDirection = (forward * forwardJumpMultiplier * transform.forward) + (horizontal * forwardJumpMultiplier * transform.right) + (Vector3.up * jumpForce);

            //if (jumpDirection.x > borderX)
            //    jumpDirection.x = borderX;

            //else if (jumpDirection.x < -borderX)
            //    jumpDirection.x = -borderX;

            playerRigidbody.AddForce(jumpDirection, ForceMode.Impulse);

            isGrounded = false;
            SetState(CharacterState.Jumping);
        }
    }

    private void IsGrounded()
    {
        Vector3 center = transform.TransformPoint(GetComponent<CapsuleCollider>().center);
        float radius = GetComponent<CapsuleCollider>().radius;
        float castDistance = (GetComponent<CapsuleCollider>().height / 2f) - radius + groundCheckDistance;

        isGrounded = Physics.SphereCast(center, radius, Vector3.down, out RaycastHit hit, castDistance, groundLayer);
    }

    public static bool Grounded()
    {
        return isGrounded;
    }

    private void SetState(CharacterState state, float value = 0)
    {
        playerAnimator.SetState(state, value);
    }
}