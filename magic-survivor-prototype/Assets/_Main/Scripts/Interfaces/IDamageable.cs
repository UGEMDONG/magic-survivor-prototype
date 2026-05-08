using UnityEngine;

public interface IDamageable
{
    public Vector2 Position { get; }
    public Collider2D HitCollider2D { get; }
    public float MaxHp { get; }
    public float CurrentHp { get; }
    public bool IsDead { get; }

    public void TakeDamage(float amount);
    public void Die();
}  
