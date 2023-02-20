using UnityEngine;

namespace DefaultNamespace.Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float speed;
        
        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 dir = new Vector2(horizontal, vertical);
            Vector2 movement = dir * speed * Time.deltaTime;

            Vector2 position = playerTransform.position;
            position += movement;

            playerTransform.position = position;
        }
    }
}