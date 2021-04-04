using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleBehaviour : MonoBehaviour
{
    [SerializeField] private float paddleSpeed;
    [SerializeField] private float leftWallPosition;
    [SerializeField] private float rightWallPosition;
    
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        if (gameManager.gameOver)
        {
            return;
        }

        float horizontalMovement = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime * paddleSpeed);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftWallPosition, rightWallPosition),
                                         transform.position.y);
    }
}
