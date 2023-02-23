using UnityEngine;

namespace DefaultNamespace.Game
{
    public abstract class AbstractItem : MonoBehaviour, IItem
    {
        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void DeActivate()
        {
            gameObject.SetActive(false);
        }
    }
}