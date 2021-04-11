using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallBehaviour : MonoBehaviour
{
    public float ballLaunchForce;
    public Transform ballSpawnPosition;
    public Transform redExplosion;
    public Transform orangeExplosion;
    public Transform greenExplosion;
    public Transform yellowExplosion;

    public Transform extraLife;
    public Transform instantDeath;
    public Transform slowSpeed;
    public Transform fastSpeed;

    public static event System.Action OnBallHittingPaddle;
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
            transform.position = ballSpawnPosition.position; // respawn on top of paddle
        } 
    }

    private void FixedUpdate()
    {
        // if spacebar is pressed while ball is resting on paddle
        if (Input.GetButtonDown("Jump") && !ballInPlay) 
        {
            ballInPlay = true;
            ballRigidBody.AddForce(Vector2.up * ballLaunchForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.transform.tag.Contains("Brick"))
        {
            InstantiateDropItem(extraLife, otherCollider.transform.position, otherCollider.transform.rotation, 15);
            InstantiateDropItem(instantDeath, otherCollider.transform.position, otherCollider.transform.rotation, 10);
            InstantiateDropItem(slowSpeed, otherCollider.transform.position, otherCollider.transform.rotation, 15);
            InstantiateDropItem(fastSpeed, otherCollider.transform.position, otherCollider.transform.rotation, 15);

            CreateExplosion(otherCollider);
            
            OnBallHittingBrick?.Invoke(otherCollider.gameObject.GetComponent<BrickBehaviour>());

            Destroy(otherCollider.gameObject);
        }

        if (otherCollider.transform.CompareTag("Paddle"))
        {
            OnBallHittingPaddle?.Invoke();
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

    private void CreateExplosion(Collision2D otherCollider)
    {
        switch (otherCollider.transform.tag)
        {
            case "RedBrick" :
                Transform redColouredExplosion = Instantiate(redExplosion, otherCollider.transform.position, otherCollider.transform.rotation);
                Destroy(redColouredExplosion.gameObject, 2f);
                break;
            case "OrangeBrick":
                Transform orangeColouredExplosion = Instantiate(orangeExplosion, otherCollider.transform.position, otherCollider.transform.rotation);
                Destroy(orangeColouredExplosion.gameObject, 2f);
                break;
            case "GreenBrick":
                Transform greenColouredExplosion = Instantiate(greenExplosion, otherCollider.transform.position, otherCollider.transform.rotation);
                Destroy(greenColouredExplosion.gameObject, 2f);
                break;
            case "YellowBrick":
                Transform yellowColouredExplosion = Instantiate(yellowExplosion, otherCollider.transform.position, otherCollider.transform.rotation);
                Destroy(yellowColouredExplosion.gameObject, 2f);
                break;
        }
    }

    private void InstantiateDropItem(Transform dropItem, Vector3 position, Quaternion rotation, int maxChance)
    {
        int randomChance = Random.Range(1, 101);
        if (randomChance <= maxChance)
        {
            Instantiate(dropItem, position, rotation);
        }
    }
}
