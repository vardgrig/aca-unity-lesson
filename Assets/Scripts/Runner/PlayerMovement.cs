using System;
using System.Collections;
using UnityEngine;

public enum CharacterState
{
    Idle,
    Movement,
    Jumping,
    Falling,
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private MobileInput mobileInput;
    [SerializeField] private CharacterMovementBounds movementBounds;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementSpeedJumping;
    [SerializeField] private float movementLeftRightTime = 0.3f;

    private bool _isGrounded = true;

    [SerializeField] private float groundCheckDistance = 0.1f;

    [SerializeField] private float jumpForce;
    [SerializeField] private float forwardJumpMultiplier = 0.5f;

    [SerializeField] private float borderX;

    private int _currentIndex = 1;

    private bool _isHandleMovement;

    private Coroutine _leftRightCoroutine;

    Vector3 startPosition;

    private void OnEnable()
    {
        mobileInput.OnSwipe += OnSwipe;
        playerCollision.OnCollisionObstacle += OnPlayerCollisionObstacle;
    }

    private void OnDisable()
    {
        mobileInput.OnSwipe -= OnSwipe;
        playerCollision.OnCollisionObstacle -= OnPlayerCollisionObstacle;
    }

    private void Start()
    {
        startPosition = transform.position;
    }
    private void OnPlayerCollisionObstacle(IObstacle obstacle)
    {
        SetState(CharacterState.Idle);
    }

    public void SetBlockState(bool state)
    {
        _isHandleMovement = state;
        playerRigidbody.isKinematic = !state;
    }

    public void SetDifficultySpeed()
    {
        var targetSpeed = PlayerPrefs.GetInt("Difficulty");
        if(targetSpeed == 0)
        {
            movementSpeed = 4;
        }
        else
        {
            movementSpeed = 8;
        }
    }

    private void OnSwipe(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.Left:
            case SwipeDirection.Right:
                HandleHorizontalMovement(direction);
                break;
            case SwipeDirection.Up:
                Jump();
                break;
            case SwipeDirection.Down:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private void HandleHorizontalMovement(SwipeDirection swipeDirection)
    {
        int desiredIndex = movementBounds.GetNextIndex(swipeDirection, _currentIndex);
        _currentIndex = desiredIndex;
        Vector3 desiredPosition = movementBounds.GetPosition(desiredIndex);
        RunCoroutine(MovePlayerToPosition(desiredPosition, movementLeftRightTime));
    }

    private void RunCoroutine(IEnumerator enumerator)
    {
        if (_leftRightCoroutine != null)
        {
            StopCoroutine(_leftRightCoroutine);
            _leftRightCoroutine = null;
        }

        _leftRightCoroutine = StartCoroutine(enumerator);
    }

    private IEnumerator MovePlayerToPosition(Vector3 position, float time)
    {
        Vector3 currentPosition = playerRigidbody.position;
        float delta = 0f;
        while (delta <= time)
        {
            delta += Time.deltaTime;
            float percent = delta / time;
            currentPosition.z = position.z = playerRigidbody.position.z;
            currentPosition.y = position.y = playerRigidbody.position.y;
            playerRigidbody.position = Vector3.Lerp(currentPosition, position, percent);
            yield return null;
        }
    }


    private void FixedUpdate()
    {
        if (!_isHandleMovement)
        {
            return;
        }

        IsGrounded();
        Move();
    }

    private void Move()
    {
        Vector3 velocity = playerRigidbody.velocity;
        velocity.z = _isGrounded ? movementSpeed : forwardJumpMultiplier * movementSpeed;
        playerRigidbody.velocity = velocity;
        if (_isGrounded)
        {
            SetState(CharacterState.Movement, 1f);
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            var velocity = playerRigidbody.velocity;
            velocity.y = jumpForce;
            playerRigidbody.velocity = velocity;
            _isGrounded = false;
            SetState(CharacterState.Jumping);
        }
    }

    private void IsGrounded()
    {
        Vector3 center = transform.TransformPoint(capsuleCollider.center);
        float radius = capsuleCollider.radius;
        float castDistance = (capsuleCollider.height / 2f) - radius + groundCheckDistance;
        _isGrounded = Physics.SphereCast(center, radius, Vector3.down, out RaycastHit hit, castDistance, groundLayer);
    }

    private void SetState(CharacterState state, float value = 0)
    {
        playerAnimator.SetState(state, value);
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}