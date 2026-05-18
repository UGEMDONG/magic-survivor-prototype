using UnityEngine;

// =============================================================================
// [IDamageable 은 "내가 상대를 몰라도 된다" 의 핵심 예시]
// 화살(Arrow) 이나 불(Fire) 이 적에게 닿았을 때 데미지를 주려면, 원래대로라면
// "TestEnemy" 클래스를 직접 알아야 한다. 그런데 만약 Boss 클래스, NPC 클래스,
// 파괴 가능한 오브젝트(상자, 나무 등) 도 데미지를 받게 만들고 싶다면?
// Arrow 안에 if (TestEnemy 면 ...) else if (Boss 면 ...) ... 같은 분기를
// 끝없이 추가해야 할 것이다.
//
// 이걸 막기 위해 "데미지를 받을 수 있는 모든 것이 공통으로 가져야 할 약속"
// 만 IDamageable 이라는 interface 로 미리 잡아둔다.
//
// 그러면 Arrow / Fire / AttackObjectBase 쪽 코드는 이렇게만 적으면 된다:
//   "충돌한 상대가 IDamageable 이면 TakeDamage 를 호출한다."
// 상대가 무슨 클래스인지, 내부에 hp 가 어떻게 저장되어 있는지, 죽을 때 어떻게
// 처리하는지는 전혀 몰라도 된다. -> 이게 interface 가 주는 가장 큰 이득이다.
//
// 새 적 클래스가 추가되어도 IDamageable 만 구현하면 자동으로 화살/불에 맞아
// 데미지를 입게 된다. Arrow / Fire / AttackObjectBase 코드는 한 줄도 안 바뀐다.
// =============================================================================
public interface IDamageable
{
    public Vector2 Position { get; }
    public Collider2D HitCollider2D { get; }
    public float MaxHp { get; }
    public float CurrentHp { get; }
    public bool IsDead { get; }

    // 외부에서 데미지를 줄 때 호출되는 함수. 내부 구현은 적마다 다를 수 있다.
    public void TakeDamage(float amount);

    // 사망 처리. 적마다 사망 연출이 달라도 됨 (이펙트, 사운드, 보상 드랍 등).
    public void Die();
}  
