using UnityEngine;

namespace DefaultNamespace.Game
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float defaultHealth;
        [SerializeField] private float currentHealth;

        private void Start()
        {
            currentHealth = defaultHealth;
        }

        public void Change(float amount)
        {
            if (Mathf.Abs(amount) <= currentHealth)
            {
                currentHealth += amount;
            }

            if (currentHealth == 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("DIE");
        }
    }
}