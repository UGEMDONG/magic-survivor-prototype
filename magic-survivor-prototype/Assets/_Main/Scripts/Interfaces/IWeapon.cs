using UnityEngine;

public interface IWeapon
{
    public Vector2 AttackDirection { get; }
    public void Attack();
    public void TickCoolTime(float deltaTime);
    public void SetAttackDirection(Vector2 direction);
}