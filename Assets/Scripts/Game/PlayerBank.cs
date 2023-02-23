using UnityEngine;

namespace DefaultNamespace.Game
{
    public class PlayerBank : MonoBehaviour
    {
        private float _coins;

        public float Coins => _coins;

        public void Add(float amount)
        {
            _coins += amount;
        }
    }
}