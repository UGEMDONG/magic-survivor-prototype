using UnityEngine;

/*
Fire 도 AttackObjectBase 의 자식이지만, Arrow 와는 동작이 다르다
Arrow 는 '날아간다', Fire 는 '그 자리에 잠깐 머물다 사라진다' 라는 식으로
자기만의 행동이 다르지만, 충돌 시 IDamageable 에게 데미지 입히기라는
공통 구현은 똑같다.

그래서 공통 구현은 부모(AttackObjectBase) 에게 맡기고, Fire 는 자기 동작만 짠다.

같은 abstract 부모를 둔 자식이라도 행동을 자유롭게 다르게 가져갈 수 있다는
걸 보여주는 예시이다.
*/
public class Fire : AttackObjectBase
{   
    protected override void Start()
    {
        base.Start();
        lifeTime = 1f;
    }
    protected override void Update()
    {
        base.Update();
        if (owner != null)
            transform.position = owner.Position;
    }
    protected override void OnTriggerEnter2D(Collider2D hitCollider)
    {
        base.OnTriggerEnter2D(hitCollider);
        // Debug.Log(hitCollider.name);
    }
}
