using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Bomb : AbstractItem
    {
        [SerializeField] private float damageAmount;

        public float DamageAmount => damageAmount;
    }
}