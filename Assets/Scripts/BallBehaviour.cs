using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallBehaviour : MonoBehaviour
{
    public float ballLaunchForce;
    public Transform paddle;
    public Transform brickShatterEffect;

    public static event System.Action OnBallHittingFloor;
    public static event System.Action<BrickBehaviour> OnBallHittingBrick;


    private GameManager gameManager;
    private Rigidbody2D ballRigidBody;
    private bool ballInPlay; // set to false if ball hits floor

    private void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        
        if (!ballInPlay)
        {
            transform.position = paddle.position; // respawn on top of paddle
        }

        if (Input.GetButtonDown("Jump") && !ballInPlay) // if spacebar is pressed while ball is resting on paddle
        {
            ballInPlay = true;
            ballRigidBody.AddForce(Vector2.up * ballLaunchForce);
        }   
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.transform.tag == "Brick")
        {
            Transform explosion = Instantiate(brickShatterEffect, otherCollider.transform.position, otherCollider.transform.rotation);
            Destroy(explosion.gameObject, 2f);

            OnBallHittingBrick?.Invoke(otherCollider.gameObject.GetComponent<BrickBehaviour>());

            Destroy(otherCollider.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Floor"))
        {
            OnBallHittingFloor?.Invoke();

            ballRigidBody.velocity = Vector2.zero; // to reset momentum of ball and prevent it flying off
            ballInPlay = false;
        }
    }
}
