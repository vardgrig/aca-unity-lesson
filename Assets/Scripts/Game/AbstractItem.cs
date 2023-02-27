using UnityEngine;

namespace DefaultNamespace.Game
{
    public abstract class AbstractItem : MonoBehaviour, IItem
    {
        public virtual void Activate()
        {
        }

        public virtual void DeActivate()
        {
            Destroy(gameObject);
        }
    }
}