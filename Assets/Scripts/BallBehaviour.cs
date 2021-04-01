using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallBehaviour : MonoBehaviour
{
    [SerializeField]
    private float ballInitialVelocity;
    private float ballVelocity;
    public float ballLaunchForce;
    private Rigidbody2D ballRigidBody;
    private bool ballInPlay; // set to false if ball hits floor
    public Transform paddle; 

    public void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
        ballVelocity = ballInitialVelocity;
    }

    public void Start()
    {
       // ballRigidBody.AddForce(Vector2.up * 500);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballInPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !ballInPlay) // if spacebar is pressed while ball is resting on paddle
        {
            ballInPlay = true;
            ballRigidBody.AddForce(Vector2.up * ballLaunchForce);
        }   
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.transform.tag == "Brick")
        {
            Destroy(collider.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Floor"))
        {
            Debug.Log("Ball has hit Floor");
            ballRigidBody.velocity = Vector2.zero;
            ballInPlay = false;
        }
    }
}
