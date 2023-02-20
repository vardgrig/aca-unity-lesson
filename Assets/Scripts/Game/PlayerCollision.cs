using System;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public delegate void ItemCollect(float amount);
    public class PlayerCollision : MonoBehaviour
    {
        public event ItemCollect OnItemCollected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Fruit fruit = other.transform.parent.GetComponent<Fruit>();
            if (fruit == null)
            {
                return;
            }

            fruit.ChangeColor(Color.red);
            // fruit.gameObject.SetActive(false);
            OnItemCollected?.Invoke(fruit.Value);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Fruit fruit = other.transform.parent.GetComponent<Fruit>();
            if (fruit == null)
            {
                return;
            }

            fruit.RevertColor();
        }
    }
}