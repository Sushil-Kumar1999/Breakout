using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleBehaviour : MonoBehaviour
{
    [SerializeField]
    private float paddleSpeed;
    [SerializeField]
    private float leftWallPosition;
    [SerializeField]
    private float rightWallPosition;

    void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime * paddleSpeed);

        if (transform.position.x < leftWallPosition)
        {
            transform.position = new Vector2(leftWallPosition, transform.position.y);
        }

        if (transform.position.x > rightWallPosition)
        {
            transform.position = new Vector2(rightWallPosition, transform.position.y);
        }
    }
}
