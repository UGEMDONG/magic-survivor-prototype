using UnityEngine;

/* 
Arrow 는 AttackObjectBase 의 자식이다
충돌 시 데미지를 주는 공통 로직 (OnTriggerEnter2D) 은 부모가 이미 작성해뒀다.
Arrow 는 그 위에 직선으로 날아간다라는 자기만의 동작만 추가하면 된다.

만약 AttackObjectBase 가 없었다면, Arrow 는
Collider2D 가져오기 + IDamageable 찾아서 데미지 주기 + 데미지/방향/속도 필드
까지 다 직접 구현해야 했을 것이다. 
Fire 도 마찬가지로 같은 코드를 또 작성해야 했다.
하지만 abstract(AttackObjectBase.cs) 덕분에 그 반복이 사라진다.
*/


public class Arrow : AttackObjectBase
{

    public Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
        lifeTime = 20f;
    }

    // Arrow 만의 고유 동작
    // 정해진 방향으로 일정 속도로 직진.
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    // Arrow 만의 고유 동작
    // 적과 충돌하면 없어지기.
    protected override void OnTriggerEnter2D(Collider2D hitCollider)
    {
        if (hitCollider == null) return;
        IDamageable hitDamageableObject = hitCollider.GetComponent<IDamageable>();
        
        if (hitDamageableObject == null) return;
        hitDamageableObject.TakeDamage(damage);
        Destroy(gameObject);
    }
}
