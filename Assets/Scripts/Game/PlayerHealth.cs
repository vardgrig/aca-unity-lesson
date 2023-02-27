using UnityEngine;

namespace DefaultNamespace.Game
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float defaultHealth;

        private float _currentHealth;
        private bool _freezeHealth;

        private void Start()
        {
            SetDefault();
        }

        public void Change(float amount)
        {
            if (_freezeHealth)
            {
                return;
            }

            if (Mathf.Abs(amount) <= _currentHealth)
            {
                _currentHealth += amount;
            }

            if (_currentHealth == 0f)
            {
                Die();
            }
        }

        public void SetDefault()
        {
            _currentHealth = defaultHealth;
        }

        public void FreezeHealth(bool state)
        {
            _freezeHealth = state;
        }

        private void Die()
        {
            Debug.Log("DIE");
        }
    }
}