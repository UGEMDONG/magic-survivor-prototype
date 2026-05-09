using UnityEngine;

/*
이 코드는 는 IWeapon 의 첫 구현체입니다.
처음에는 WeaponBase (abstract) 같은 공통 부모가 없었기 때문에,
IWeapon 에서 미리 정한 약속만을 보고 모든 항목을 처음부터 직접 구현해야 했습니다.

interface 는 무조건 해당 내용을 구현해야한다는 약속이기 때문에
IWeapon 에 적힌 프로퍼티(WeaponName, Damage 등) 와 메서드(Attack, TickCoolTime 등)
를 빠짐없이 직접 채워야 컴파일 에러가 안납니다.
이게 바로 interface 가 가진 약속 강제력입니다.

그런데 다른 무기를 또 만들고싶으면?
다음 무기(예: Burning) 도 똑같이 weaponName, damage, attackCoolTime, currentCoolTime,
TickCoolTime, SetAttackDirection ... 을 처음부터 다 구현해야합니다.
거의 똑같은 코드를 여러 무기마다 반복해서 작성하게 되는데, 
이 반복을 줄이기 위해 abstract 클래스(WeaponBase) 를 작성하게 되었습니다.
(WeaponBase.cs 의 설명 참고)
*/
public class Bow : MonoBehaviour, IWeapon
{

    // IWeapon 구현
    // IWeapon 에서 약속한 것들을 전부 직접 들고 있는 구조.
    private string weaponName = "Bow";
    private Vector2 attackDirection;
    private string directionType = "NearestEnemy";
    private float damage = 50f;
    [SerializeField]
    private float attackCoolTime = 1f;
    private float currentCoolTime;
    private bool isAttackReady;

    public string WeaponName => weaponName;
    public Vector2 AttackDirection => attackDirection;
    public string DirectionType => directionType;
    public float Damage => damage;
    public float AttackCoolTime => attackCoolTime;
    public float CurrentCoolTime => currentCoolTime;
    public bool IsAttackReady => isAttackReady;

    // Bow 만의 고유 동작: 화살을 생성해서 날린다.
    // 이 무기를 사용하는 쪽은 Attack()의 내부 구현이 어떻든 간에 
    // IWeapon 에 Attack() 매서드가 존재한다는 것만 알면 된다.
    public void Attack(Vector2 position)
    {
        GameObject arrowObject = Instantiate(arrowPrefab);
        arrowObject.transform.position = position;
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.direction = attackDirection;
        arrow.damage = damage;
        isAttackReady = false;
        currentCoolTime = attackCoolTime;
    }

    // 쿨타임 돌리는 매서드. Burning 등 다른 무기에서도 거의 똑같이 반복된다.
    // 이런 매서드처럼 여러 무기에서 똑같이 쓰는 코드 가 바로 abstract 로 묶기 좋은 후보다.
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
    

    [SerializeField]
    private GameObject arrowPrefab;

    void Start()
    {
        currentCoolTime = attackCoolTime;
    }



}
