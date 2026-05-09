using UnityEngine;

/*
1. abstract 클래스란?
abstract 클래스는 그 자체로 직접 사용할 수 없고, 반드시 다른 클래스가 상속해서 사용해야 하는 클래스다.
공통으로 사용하는 변수나 메서드는 abstract 클래스에 미리 구현해두고,
무기마다 다르게 동작해야 하는 부분은 자식 클래스에서 구현하도록 강제하거나 override해서 바꿀 수 있다.

예를 들어 WeaponBase에는 쿨타임 감소 처리, 공격 방향 설정 같은 공통 기능을 넣고,
Burning, Bow 같은 개별 무기는 WeaponBase를 상속받아 자신만의 Attack() 동작만 구현할 수 있다.

2.1. interface vs abstract
interface: '이 기능들은 반드시 있어야 한다' 는 약속만 적는다. 일반적으로 실제 내부 구현 코드는 없음.
abstract: 약속도 적을 수 있고, '실제 동작 코드까지' 미리 적어둘 수 있다.

2.2.interface 를 사용하는 이유
1) 외부에서 사용할 기능의 약속을 명확히 하기 위해
2) 구체적인 클래스에 의존하지 않기 위해
3) 여러 클래스가 같은 기능을 가진 것처럼 다루기 위해
4) C#에서 클래스 상속은 하나만 가능하지만 interface는 여러 개 구현할 수 있기 때문에
5) 협업할 때 '이 기능은 이렇게 쓰면 된다'는 기준을 만들기 위해

interface를 쓰는 이유는 실제 구현 방식과 상관없이, 
외부에서 해당 기능을 동일한 방식으로 사용할 수 있게 하기 위해서다.
예를 들어 Player는 Bow, Burning 같은 구체적인 무기 클래스를 몰라도 
IWeapon이라는 약속만 보고 Attack(), TickCoolTime() 등을 호출할 수 있다. 
또한 interface는 여러 개를 동시에 구현할 수 있기 때문에, 특정 상속 구조에 묶이지 않고 
기능 단위로 코드를 설계할 수 있다는 장점이 있다.

3. 일반적인 흐름에 대한 예시
1) IWeapon 으로 '모든 무기가 가져야 할 약속' 을 정의한다.
2) 거의 모든 무기에 공통으로 들어가는 코드 (쿨타임 감소, 공격 방향 설정 등)는
   WeaponBase 라는 abstract 로 정의한다.
3) 새 무기는 WeaponBase 를 상속받아서 Attack() 같은 무기별 고유 동작 만 짜면 끝.

4.1. 반드시 interface 의 약속만 abstract 로 정리해야 하는 건 아니다
abstract 는 '반복적으로 등장하는 공통 코드를 묶어둔다' 가 본질이라서,
interface 와 무관한 헬퍼 메서드, 공용 필드, 자체 룰 같은 것도 같이 넣을 수 있다.
예를 들면 AttackObjectBase 는 어떤 interface 도 구현하지 않지만,
Arrow 와 Fire 가 공통으로 가지는 동작만 묶기 위해 abstract 로 만들어졌다.

4.2. Arrow와 Fire 는 왜 interface 로 정의하지 않았는가?
다른 기능에 대한 의존성이 없기때문이다.
Arrow와 Fire 는 무기 기능 개발하는 쪽에서만 사용해도 충분하기 때문에 
interface 로 미리 약속을 정해 둘 필요가 없다.

5. WeaponBase 를 만들고 얻을 수 있는 이득
1) Bow 처럼 매 무기마다 weaponName, damage, attackCoolTime ... 같은 필드를
  처음부터 다시 선언할 필요가 없다.
2) TickCoolTime, SetAttackDirection 같은 어느 무기나 동작이 똑같은 메서드는
  여기서 한 번만 작성해두면 모든 자식 무기가 자동으로 가진다.
3) 그러면 새 무기 (예: Burning) 를 만들 때, 진짜로 그 무기만의 핵심 로직인
  Attack 동작에만 집중해서 짤 수 있다 (Burning.cs 와 Bow.cs 의 코드 길이를 비교해보면 체감이 확 온다).
*/
public class WeaponBase : MonoBehaviour, IWeapon
{
    // IWeapon 구현
    // 모든 무기가 공통으로 들고 있어야 하는 데이터들. 이 코드에서는 전부 interface 에서 미리 약속한 내용들이다.
    // protected 로 두면 자식 클래스 (Burning 등) 에서 그대로 접근해서 쓸 수 있다.
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

    // IWeapon 약속 중 프로퍼티 get 부분은 여기서 한 번만 처리해버린다.
    // = 자식 클래스는 이걸 다시 쓸 필요가 없다.
    public string WeaponName => weaponName;
    public Vector2 AttackDirection => attackDirection;
    public string DirectionType => directionType;
    public float Damage => damage;
    public float AttackCoolTime => attackCoolTime;
    public float CurrentCoolTime => currentCoolTime;
    public bool IsAttackReady => isAttackReady;


    // 아직 기본 구현은 하지 않았음
    // 아마 그냥 충돌하면 데미지가 들어오게끔 구현 할 것 같음
    public virtual void Attack(Vector2 position)
    {
        
    }

    // 쿨타임 깎는 로직은 어떤 무기라도 동일하게 동작한다.
    // 그래서 여기서 한 번만 작성하고, 모든 자식 무기가 그대로 물려받게 한다.
    // = Bow 처럼 무기마다 같은 코드를 반복해서 적던 문제가 사라진다.
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
