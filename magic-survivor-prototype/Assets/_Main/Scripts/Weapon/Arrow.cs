using UnityEngine;

public class Arrow : AttackObjectBase
{

    public Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
        lifeTime = 20f;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        
    }


}
