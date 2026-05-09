using UnityEngine;

public abstract class AttackObjectBase: MonoBehaviour
{
    public float damage;
    public Vector2 direction;
    public float speed = 5f;
    public Collider2D attackCollider;  
    public float lifeTime = 1f;

    protected virtual void Start()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D hitCollider)
    {
        if (hitCollider == null) return;
        IDamageable hitDamageableObject = hitCollider.GetComponent<IDamageable>();
        
        if (hitDamageableObject == null) return;
        hitDamageableObject.TakeDamage(damage);
    }

}