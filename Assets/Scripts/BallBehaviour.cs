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
        
        //if (Input.GetButtonDown("Jump") && ballIsMoving == false)
        //{
        //    transform.parent = null;    //Disconnect the Ball from the Paddle
        //    ballIsMoving = true;      //The Ball is in play (is moving)
        //    ballRigidBody.isKinematic = false;		//Uncheck the isKinematic in the Rigidbody 2D

        //    if (Input.GetAxis("Horizontal") == 0f)      
        //        ballRigidBody.AddForce(new Vector2(1f, ballVelocity));  
        //    else if (Input.GetAxis("Horizontal") > 0f) 
        //        ballRigidBody.AddForce(new Vector2(ballVelocity, ballVelocity)); 
        //    else if (Input.GetAxis("Horizontal") < 0f)      
        //        ballRigidBody.AddForce(new Vector2(-ballVelocity, ballVelocity));	
        //}
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Paddle")
    //    {
    //        if (Input.GetAxis("Horizontal") == 0f)
    //            ballRigidBody.AddForce(new Vector2(1f, ballInitialVelocity));
    //        else if (Input.GetAxis("Horizontal") > 0f)
    //            ballRigidBody.AddForce(new Vector2(ballInitialVelocity, ballInitialVelocity));
    //        else if (Input.GetAxis("Horizontal") < 0f)
    //            ballRigidBody.AddForce(new Vector2(-ballInitialVelocity, ballInitialVelocity));
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Floor"))
        {
            Debug.Log("Ball has hit Floor");
            ballRigidBody.velocity = Vector2.zero;
            ballInPlay = false;
        }
    }

    private void Respawn()
    {
        transform.position = paddle.position;
    }
}
