using UnityEngine;

public abstract class SpecialEffect : MonoBehaviour
{
    [SerializeField] private float speed;
    private float floorVerticalPosition;

    public abstract void Activate();

    private void Awake()
    {
        floorVerticalPosition = GameObject.FindGameObjectWithTag("Floor").transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.transform.CompareTag("Paddle"))
        {
            Activate();
            Destroy(gameObject);
        };
    }

    private void Update()
    {
        MoveDropItem();
    }

    private void MoveDropItem()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);

        if (transform.position.y < floorVerticalPosition)
        {
            Destroy(gameObject);
        }
    }
}
