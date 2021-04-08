using UnityEngine;

public class SlowSpeed : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float slowByPercent = 0.5f;

    private float floorVerticalPosition;
    private PaddleBehaviour paddle;

    private void Awake()
    {
        paddle = FindObjectOfType<PaddleBehaviour>();
        floorVerticalPosition = GameObject.FindGameObjectWithTag("Floor").transform.position.y;
    }

    private void Update()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);

        if (transform.position.y < floorVerticalPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Paddle"))
        {
            paddle.UpdateSpeed(slowByPercent); 
            Destroy(gameObject);
        }
    }
}
