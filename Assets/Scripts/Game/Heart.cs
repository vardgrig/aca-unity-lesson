using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Heart : AbstractItem
    {
        [SerializeField] private float value;
        public float Value => value;
    }
}