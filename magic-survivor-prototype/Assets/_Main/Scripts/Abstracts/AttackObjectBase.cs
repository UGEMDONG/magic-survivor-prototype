using UnityEngine;

/*
AttackObjectBase.cs 는 interface 없이도 abstract 가 쓸 수 있다 를 보여주는 예시이다.
앞서 IWeapon -> WeaponBase 의 흐름은 'interface 약속을 abstract 가 받아서
공통 구현을 깔아주는' 형태였다.
그런데 abstract 는 그게 전부가 아니다.

Arrow 와 Fire 는 둘 다 월드에 잠깐 생성되어서 적과 부딪히면 데미지를 입히는 공격
오브젝트다. 
두 클래스에서 공통으로 필요한 게 있다:
1) damage / direction / speed / lifeTime 같은 데이터
2) Collider2D 로 누군가에 닿으면 IDamageable 을 찾아서 TakeDamage 호출
이라는 거의 똑같은 충돌 처리

하지만 이 둘에 대한 외부와의 약속(interface) 은 딱히 없어도 게임이 돌아간다.
외부에서 Arrow 와 Fire 를 IWeapon 처럼 추상적으로 다뤄야 할 일이 없기 때문이다.

이런 경우엔 굳이 interface 를 새로 만들 필요 없이, abstract 클래스 하나로
공통 부품들만 묶어두면 된다.
즉, 반복되는 코드를 묶을 때마다 항상 interface 가 쓰여야하는 건 아니다.
abstract 만 단독으로 쓰는 것도 충분히 자연스러운 패턴이다.
*/
public abstract class AttackObjectBase: MonoBehaviour
{
    public IWeapon owner; // 지금은 퍼블릭인데 나중에 protected로 초기화로 할당

    // ↓ Arrow, Fire 가 둘 다 필요로 하는 데이터들을 여기서 한 번만 정의한다.
    public float damage;
    public Vector2 direction;
    public float speed = 5f;
    public Collider2D attackCollider;  
    public float lifeTime = 1f;

    // 공통 초기화. 자식이 Start 를 override 하면서 base.Start() 로 부모 것도 같이
    // 호출하게 만들어두면, "Collider2D 캐싱" 같은 공통 작업을 자식마다 다시 적을
    // 필요가 없다.
    protected virtual void Start()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    // [핵심 포인트]
    // 충돌 처리 로직이 Arrow 든 Fire 든 동일하므로, 부모가 한 번만 작성한다.
    // - "부딪힌 상대가 IDamageable 을 들고 있으면 데미지를 준다."
    // - 상대가 Enemy 클래스인지 NPC 클래스인지 보스인지는 전혀 신경 안 쓴다.
    //   IDamageable 이라는 약속만 알면 되기 때문 (-> IDamageable.cs 의 설명 참고).
    protected virtual void OnTriggerEnter2D(Collider2D hitCollider)
    {
        if (hitCollider == null) return;
        IDamageable hitDamageableObject = hitCollider.GetComponent<IDamageable>();
        
        if (hitDamageableObject == null) return;
        hitDamageableObject.TakeDamage(damage);
    }

    protected virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

}
