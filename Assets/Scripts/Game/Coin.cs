using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Coin : AbstractItem
    {
        [SerializeField] private float value;

        public float Value => value;
    }
}