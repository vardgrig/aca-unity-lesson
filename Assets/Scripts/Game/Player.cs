using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerCollision playerCollision;

        private void OnEnable()
        {
            playerCollision.OnItemCollected += OnItemCollected;
        }

        private void OnDisable()
        {
            playerCollision.OnItemCollected -= OnItemCollected;
        }

        private void OnItemCollected(float amount)
        {
            playerHealth.Change(amount);
        }
    }
}