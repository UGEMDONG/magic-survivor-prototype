using UnityEngine;

public class WeaponBase : MonoBehaviour, IWeapon
{
    // IWeapon 구현
    [SerializeField]
    protected string weaponName = "Classic";
    protected Vector2 attackDirection;
    [SerializeField]
    protected string directionType = "MoveDirection";
    [SerializeField]
    protected float damage = 20f;
    [SerializeField]
    protected float attackCoolTime = 0f;
    protected float currentCoolTime;
    protected bool isAttackReady = false;

    public string WeaponName => weaponName;
    public Vector2 AttackDirection => attackDirection;
    public string DirectionType => directionType;
    public float Damage => damage;
    public float AttackCoolTime => attackCoolTime;
    public float CurrentCoolTime => currentCoolTime;
    public bool IsAttackReady => isAttackReady;

    public virtual void Attack(Vector2 position)
    {
        
    }

    public void TickCoolTime(float deltaTime)
    {
        if (isAttackReady) return;

        currentCoolTime -= deltaTime;
        if (currentCoolTime <= 0)
        {
            isAttackReady = true;
        }
    }

    public virtual void SetAttackDirection(Vector2 direction)
    {
        attackDirection = direction;
    }

    protected virtual void Start()
    {
        currentCoolTime = attackCoolTime;
        isAttackReady = false;
    }
}
