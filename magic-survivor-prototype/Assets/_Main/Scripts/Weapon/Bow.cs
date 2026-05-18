using UnityEngine;

// IWeapon을 간편화 하면서 외부와의 결합도를 낮췄다, 그렇다고 해서 당연히 WeaponBase추상이 복잡해진 것도 아님
// 하지만 자연스레 클래스 WeaponBase를 받는 것이 기본적인 무기의 기준이 됨. Bow는 특이하긴 해도 공격을 완전히 구현하고, 또 쿨탐 메서드를 재사용 하지 못하는건 아쉽다고 봄.
// 이제 IWeapon은 단순히 문제 없이 무기를 다룰 수 있게 해주는 인터페이스 ((갠적으로 클래스는 곧 정체성이라고 생각해서 웬만한 무기는 추상 상속이 맞는듯
public class Bow : WeaponBase//MonoBehaviour, IWeapon
{
    [Header("Bow Fields")]
    [SerializeField] TestEnemy enemy;
    [SerializeField] protected float attackRange = 10f;

    public override void TryAttack()
    {
        Vector2 dir = GetAttackDirection();
        if (dir == Vector2.zero) return;

        GameObject arrowObject = Instantiate(arrowPrefab);
        arrowObject.transform.position = transform.position;
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.direction = dir;
        arrow.damage = damage;
    }


    protected override Vector2 GetAttackDirection()
    {
        if (!enemy) return Vector2.zero;
        return (enemy.transform.position - transform.position).normalized;
    }


    [SerializeField]
    private GameObject arrowPrefab;

    protected override void Start()
    {
        base.Start();
    }

    public override void Initialize(WeaponHolder weaponHolder)
    {
        base.Initialize(weaponHolder);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        var sense = Physics2D.OverlapCircle(transform.position, attackRange, 1 << LayerMask.NameToLayer("Enemy"));
        if (sense != null && sense.TryGetComponent<TestEnemy>(out TestEnemy enemy))
        {
            this.enemy = enemy;
        }
        else this.enemy = null;
    }

    public override void Upgrade()
    {
        base.Upgrade();
    }
}
