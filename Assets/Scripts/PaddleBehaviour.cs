using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleBehaviour : MonoBehaviour
{
    private Rigidbody2D paddleRigidBody;
    [SerializeField]
    private float paddleSpeed;

    private void Awake()
    {
        paddleRigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //Get if Any Horizontal Keys pressed ((A or D) or (Left or Right) Buttons)
        Vector2 movement = new Vector2(moveHorizontal, 0f); //Put moveHorizontal in a Vector2 Variable (x,y)...moveHorizontal will be the x axis
        paddleRigidBody.velocity = movement * paddleSpeed;

    }
}
