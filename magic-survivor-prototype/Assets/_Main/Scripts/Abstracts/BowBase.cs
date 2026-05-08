using UnityEngine;

public abstract class BowBase : MonoBehaviour, IWeapon
{

    [SerializeField]
    protected GameObject arrow;
    protected float baseDamage = 50f;
    protected float currentDamage;
    protected string attackType = "Instant";
    protected Vector2 attackDirection;

    public string AttackType => attackType;
    public float BaseDamage => baseDamage;
    public float CurrentDamage => currentDamage;

    protected float attackInterval = 1f;
    protected float currentCoolDown;
    protected bool attackable = false;
    
    void Start()
    {
        currentDamage = baseDamage;
        currentCoolDown = attackInterval;
    }

    public virtual void Attack()
    {
        if (attackType == "Instant")
        {
            
        }
    }

    public virtual void CanAttack()
    {
        if (attackType == "Instant")
        {
            Attack();
        }
    }

    public virtual void SetAttackDirection(Vector2 direction)
    {
        attackDirection = direction;
    }

    public virtual void CoolDown(float deltaTime)
    {
        if (attackable) return;

        currentCoolDown -= deltaTime;
        if (currentCoolDown <= 0)
        {
            attackable = true;
        }   
    }

    public virtual void Damaging()
    {
        return;
    }
}
