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
    [Header("Character")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [Header("Character's Attributes")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementSpeedJumping;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private float jumpForce;

    [Header("Envorinment Settings")]
    [SerializeField] private float borderX;

    [Header("Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private MobileInput mobileInput;

    private bool isGrounded = true;

    private void Start()
    {
        SwipeManager.instance.MoveEvent += SwipeMovement;
    }

    private void FixedUpdate()
    {
        IsGrounded();
        MoveForward();
    }
    void SwipeMovement(bool[] swipe)
    {
        //Move Left
        if (swipe[(int)SwipeManager.Direction.Left])
        {
            MoveLeft();
            Debug.Log("Left");
        }
        //Move Right
        else if (swipe[(int)SwipeManager.Direction.Right])
        {
            MoveRight();
            Debug.Log("Right");
        }
        //Jump
        else
        {
            Jump();
            Debug.Log("Jump");
        }
    }

    private void MoveForward()
    {
        Vector3 movement = Vector3.forward * Time.fixedDeltaTime;
        movement.x *= movementSpeed;
        movement.z *= isGrounded ? movementSpeed : movementSpeedJumping;
        Vector3 pos = transform.position + movement;
        pos.x = Mathf.Clamp(pos.x, -borderX, borderX);

        playerRigidbody.MovePosition(pos);
        if (isGrounded)
        {
            SetState(CharacterState.Movement, Vector3.forward.magnitude);
        }
    }
    private void MoveLeft()
    {
        Vector3 moveDirection = Vector3.forward + Vector3.left * jumpForce;
        playerRigidbody.MovePosition(moveDirection);
    }

    private void MoveRight()
    {
        Vector3 moveDirection = Vector3.forward + Vector3.right * jumpForce;
        playerRigidbody.MovePosition(moveDirection);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            Vector3 jumpDirection = Vector3.forward + Vector3.up * jumpForce;
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