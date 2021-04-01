using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleBehaviour : MonoBehaviour
{
    private Rigidbody2D paddleRigidBody;
    [SerializeField]
    private float paddleSpeed;
    [SerializeField]
    private float leftBorderWallPosition;
    [SerializeField]
    private float rightBorderWallPosition;

    private void Awake()
    {
        paddleRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime * paddleSpeed);

        if (transform.position.x < leftBorderWallPosition)
        {
            transform.position = new Vector2(leftBorderWallPosition, transform.position.y);
        }

        if (transform.position.x > rightBorderWallPosition)
        {
            transform.position = new Vector2(rightBorderWallPosition, transform.position.y);
        }
    }
}
