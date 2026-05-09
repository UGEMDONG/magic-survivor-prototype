using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    private Vector2 position;
    public Vector2 Position => position;

    private Collider2D hitCollider2D;
    public Collider2D HitCollider2D => hitCollider2D;

    private float maxHp = 100f;
    public float MaxHp => maxHp;

    private float currentHp;
    public float CurrentHp => currentHp;

    private bool isDead = false;
    public bool IsDead => isDead;
    
    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float amount)
    {
        currentHp -= amount;
        Debug.Log(currentHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
