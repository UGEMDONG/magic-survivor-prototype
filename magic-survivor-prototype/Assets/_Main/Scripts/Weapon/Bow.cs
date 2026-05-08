using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{

    // IWeapon 구현
    private Vector2 attackDirection;
    private float damage = 50f;
    [SerializeField]
    private float attackCoolTime = 1f;
    private float currentCoolTime;
    private bool isAttackReady;

    public Vector2 AttackDirection => attackDirection;
    public float Damage => damage;
    public float AttackCoolTime => attackCoolTime;
    public float CurrentCoolTime => currentCoolTime;
    public bool IsAttackReady => isAttackReady;

    public void Attack()
    {
        Instantiate(arrowPrefab);
        isAttackReady = false;
        currentCoolTime = attackCoolTime;
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

    public void SetAttackDirection(Vector2 direction)
    {
        attackDirection = direction;
    }
    //

    [SerializeField]
    private GameObject arrowPrefab;

    void Start()
    {
        currentCoolTime = attackCoolTime;
    }



}
