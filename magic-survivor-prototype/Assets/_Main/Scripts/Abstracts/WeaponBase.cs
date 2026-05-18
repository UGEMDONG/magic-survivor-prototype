using UnityEngine;

public enum WeaponDirectionType
{
    None,               // 방향이 필요 없는 무기
    MoveDirection,      // 이동 방향 기준
    TargetDirection,    // 무기가 선택한 타겟 방향 기준 ((선택한 방향은 꼭 타게팅이란건 아님.
    FixedDirection      // 고정 방향 기준               ((이거도 꼭 영원히 한방향이란건 아님.
}

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [Header("Weapon Base Fields")]
    [SerializeField] protected WeaponHolder owner;  // master 이름도 ㄱㅊ
    [Space]
    [SerializeField] protected string weaponName = "Classic";
    [SerializeField] protected WeaponDirectionType directionType;
    [SerializeField] protected float damage = 20f; 
    [SerializeField] protected float attackCoolTime = 0f;
    
    // protected Vector2 attackDirection;
    protected float currentCoolTime;
    protected bool isAttackReady = false;

    public string WeaponName => weaponName;
    public WeaponDirectionType DirectionType => directionType;
    public float Damage => damage;
    public bool IsAttackReady => isAttackReady;

    public Vector2 Position => transform.position;

    public virtual void Initialize(WeaponHolder weaponHolder)
    {
        owner = weaponHolder;
        transform.SetParent(owner.transform);   // 이걸 왜 초기화 시켜주는 애한테서 안하고 여기서 하냐: 이젠 암묵적으로 IWeapon 만이 아닌 WeaponBase를 상속받는 무기들은
        // 플레이어 기준, 플레이어 소유의 무기로 볼거임, 그래서 추상 클래스의 가상 함수에서 미리 자식으로 종속시키는거. IWeapon만 상속받는 무기들은 아마 포탑이나 소환수? 등이 있지 않을까.
        transform.localPosition = Vector3.zero;
    }
    public virtual void Upgrade()   // 이거는 바로 추상으로 만들기 아까워서 머라도 구현해봄
    {
        damage += 1f;
    }

    // 웨폰베이스를 추상으로 만든 가장 큰 이유 중 하나: 하위에서 어떻게 될 지 거의 모르고 이런 메서드 늘어날 수도 있어서
    // 근디 왜 position을 뺐냐? 말 그대로 필요하게 될진 몰라서, 필요하면 알아서 상속받아서 타겟이나 위치 필드 세우고 내부에서 구현ㄱㄱ
    public abstract void TryAttack(/*Vector2 position*/); // 이름 바꾼 이유: 내부적으로 공격이 안 될 수도 있고, 쿨탐에 따라 일단 시도해보는 공격이라서.


    // Tick 쿨타임 따로 만들기 보단 델타타임을 받는 틱에서 한번에 처리하기
    public virtual void Tick(float deltaTime)
    {      
        TickCoolTime(deltaTime);
        if(IsAttackReady)
        {
            TryAttack();
            isAttackReady = false;
            currentCoolTime = attackCoolTime;
        }
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

    // 이 메서드가 밑에 삭제된 함수 대용이긴 한데 왤캐 어설프냐면; base를 무시하는 override 함수가 될 가능성이 높음.
    // WeaponBase를 상속받고 단순 플레이어 방향 공격 무기는 이 메서드를 그냥 써도 되고 아니라면 아예 새로 만드는걸로.
    // 만약 캐이스 당 추상/가상 함수를 만들자니 자기 공격 방향 타입 빼고는 안 쓸거라 낭비고
    // 아예 추상으로 하자니 그냥 MoveDir은 쉽게 구해올 수 있으니까, 살짝 계륵인 상태긴 한데
    // 그래도 단순 이동 방향 무기들은 바로 구현되는거니까 이렇게ㄱㄱ. Bow같은데에 동작하는 예제 만들 예정
    protected virtual Vector2 GetAttackDirection()
    {
        switch(directionType)
        {
            case WeaponDirectionType.MoveDirection: return owner.MoveDirection;
            case WeaponDirectionType.TargetDirection: return Vector2.zero;
            case WeaponDirectionType.FixedDirection: return Vector2.up;
            default: return Vector2.zero;
        }
    }   

    // 이친구는 빼는게 낫다고 생각하는게, 무기마다 방향 혹은 타겟을 잡는 방법이 완전히 다를 것이고 심지어 없을 수도 있기 때문에
    // 대신에 위에 내부적으로 자신의 공격 방향 타입에 따라 얻어올 수 있는 기능을 추가했어
    // public virtual void SetAttackDirection(Vector2 direction);

    protected virtual void Start()
    {
        currentCoolTime = attackCoolTime;
        isAttackReady = false;
    }
}
