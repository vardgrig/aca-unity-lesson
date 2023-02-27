using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Invulnerable:AbstractItem
    {
        [SerializeField] private float duration;

        public float Duration => duration;
    }
}