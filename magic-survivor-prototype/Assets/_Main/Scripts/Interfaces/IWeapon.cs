using UnityEngine;

public interface IWeapon
{
    public string WeaponName { get; }
    public string DirectionType { get; }
    public Vector2 AttackDirection { get; }
    public float Damage { get; }
    public float AttackCoolTime { get; }
    public float CurrentCoolTime { get; }
    public bool IsAttackReady { get; }
    public void Attack(Vector2 position);
    public void TickCoolTime(float deltaTime);
    public void SetAttackDirection(Vector2 direction);
}