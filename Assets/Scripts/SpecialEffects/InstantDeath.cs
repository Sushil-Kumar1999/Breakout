using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameManager gameManager;
    private float floorVerticalPosition;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            gameManager.InvokeInstantDeath();
            Destroy(gameObject);
        }
    }
}
