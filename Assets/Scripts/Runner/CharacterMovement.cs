using System;
using DefaultNamespace.Runner;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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
    [SerializeField] private MobileInput mobileInput;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementSpeedJumping;

    private bool isGrounded = true;

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
        // float horizontal = Input.GetAxis("Horizontal");
        // float forward = Input.GetAxis("Vertical");
        // Vector3 input = new(horizontal, 0f, forward);
        Vector2 mobileInputValue = this.mobileInput.GetInput();
        Vector3 input = new Vector3(mobileInputValue.x, 0f, mobileInputValue.y);
        Debug.LogError(input);
        Vector3 movement = input * Time.fixedDeltaTime;
        movement.x *= movementSpeed;
        movement.z *= isGrounded ? movementSpeed : movementSpeedJumping;
        Vector3 pos = transform.position + movement;
        pos.x = Mathf.Clamp(pos.x, -borderX, borderX);
        
        playerRigidbody.MovePosition(pos);
        if (isGrounded)
        {
            SetState(CharacterState.Movement, mobileInputValue.y);
        }
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");

            Vector3 jumpDirection = (forward * forwardJumpMultiplier * transform.forward) +
                                    (horizontal * forwardJumpMultiplier * transform.right) + (Vector3.up * jumpForce);

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
        Vector3 center = transform.TransformPoint(capsuleCollider.center);
        float radius = capsuleCollider.radius;
        float castDistance = (capsuleCollider.height / 2f) - radius + groundCheckDistance;
        isGrounded = Physics.SphereCast(center, radius, Vector3.down, out RaycastHit hit, castDistance, groundLayer);
    }

    private void SetState(CharacterState state, float value = 0)
    {
        playerAnimator.SetState(state, value);
    }
}