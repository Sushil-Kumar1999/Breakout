using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    [SerializeField] private float paddleSpeed;
    [SerializeField] private float leftWallPosition;
    [SerializeField] private float rightWallPosition;

    private void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime * paddleSpeed);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftWallPosition, rightWallPosition),
                                         transform.position.y);
    }
}
