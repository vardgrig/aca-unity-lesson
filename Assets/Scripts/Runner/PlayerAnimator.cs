using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementMaxSpeed;

    private readonly int Speed = Animator.StringToHash("Speed");
    private readonly int Jump = Animator.StringToHash("Jump");


    public void SetState(CharacterState state, float value)
    {
        switch (state)
        {
            case CharacterState.Idle:
                animator.SetFloat(Speed, 0);
                break;
            case CharacterState.Movement:
                animator.SetFloat(Speed, value * movementMaxSpeed);
                break;
            case CharacterState.Jumping:
                animator.SetTrigger(Jump);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}