using UnityEngine;

public class Fire : MonoBehaviour
{
    public Vector2 direction;
    [SerializeField]
    public float speed = 0f;
    public Rigidbody2D rb;
    public float lifeTime = 3f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        
    }
}
