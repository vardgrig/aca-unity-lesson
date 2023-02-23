using System;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerBank playerBank;
        [SerializeField] private PlayerCollision playerCollision;
        public event Action OnEat;

        private void OnEnable()
        {
            playerCollision.OnItemCollected += OnItemCollected;
        }

        private void OnDisable()
        {
            playerCollision.OnItemCollected -= OnItemCollected;
        }

        private void OnItemCollected(IItem item)
        {
            switch (item)
            {
                case Bomb bomb:
                    playerHealth.Change(bomb.DamageAmount);
                    break;
                case Coin coin:
                    playerBank.Add(coin.Value);
                    break;
                case Heart heart:
                    playerHealth.Change(heart.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(item));
            }

            OnEat?.Invoke();
        }
    }
}