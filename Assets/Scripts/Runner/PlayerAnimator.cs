using System;
using UnityEngine;

namespace DefaultNamespace.Runner
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float movementMaxSpeed;

        private readonly int Speed = Animator.StringToHash("Speed");

        public void SetState(CharacterState state, float value)
        {
            switch (state)
            {
                case CharacterState.Movement:
                    animator.SetFloat(Speed, value * movementMaxSpeed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}