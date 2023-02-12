using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform squareTransform;
    public Rigidbody2D squareRigidbody;
    public float speed = 10;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            Vector2 direction = new Vector2(horizontal, vertical);
            Vector2 movement = direction * (10 * Time.deltaTime);

            Vector2 position = squareTransform.position;
            position += movement;

            squareRigidbody.MovePosition(position);
        }
    }
}