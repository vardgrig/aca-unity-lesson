using UnityEngine;

namespace DefaultNamespace.Game
{
    public delegate void ItemCollect(IItem item);

    public class PlayerCollision : MonoBehaviour
    {
        public event ItemCollect OnItemCollected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IItem item = other.transform.parent.GetComponent<IItem>();
            if (item == null)
            {
                return;
            }
            
            OnItemCollected?.Invoke(item);
        }
    }
}