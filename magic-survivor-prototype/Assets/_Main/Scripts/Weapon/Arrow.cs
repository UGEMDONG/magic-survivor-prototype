using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Vector2 direction;
    [SerializeField]
    public float speed = 5f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        
    }


}
