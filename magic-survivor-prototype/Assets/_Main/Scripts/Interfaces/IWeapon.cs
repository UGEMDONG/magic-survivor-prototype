using UnityEngine;

public interface IWeapon
{
    public string AttackType { get; }

    public float BaseDamage { get; }
    public float CurrentDamage { get; }
    public void Attack();
    public void CoolDown(float deltaTime);
    public void Damaging();
    public void SetAttackDirection(Vector2 direction);
}
