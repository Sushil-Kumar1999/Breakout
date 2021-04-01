using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallBehaviour : MonoBehaviour
{
    public float ballLaunchForce;
    public Transform paddle;
    public Transform brickShatterEffect;

    public static event System.Action OnBallHittingFloor;
    public static event System.Action<int> OnBallHittingBrick;

    private Rigidbody2D ballRigidBody;
    private bool ballInPlay; // set to false if ball hits floor

    public void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

    public void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.transform.tag == "Brick")
        {
            Transform explosion = Instantiate(brickShatterEffect, otherCollider.transform.position, otherCollider.transform.rotation);
            Destroy(explosion.gameObject, 2f);

            OnBallHittingBrick.Invoke(otherCollider.gameObject.GetComponent<BrickBehaviour>().points);

            Destroy(otherCollider.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Floor"))
        {
            OnBallHittingFloor?.Invoke();

            ballRigidBody.velocity = Vector2.zero; // to reset momentum of ball and prevent it flying off
            ballInPlay = false;
        }
    }
}
