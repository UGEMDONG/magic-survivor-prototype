using UnityEngine;

/*
1. interface 란?
interface 는 '이런 기능을 제공하기로 약속한다' 라고 미리 약속을 적어두는 일종의 계약서이다.
기본적으로 실제 동작 코드 없이, 프로퍼티, 매서드 이름, 매개변수, 반환 타입 만 정의한다.

2. 왜 약속을 먼저 잡는가?
만약 무기 기능과 무기 기능을 사용해야하는(플레이어, 적 등)쪽을 서로 다른 사람이 작업하는 경우에,
만약 약속이 없으면 사용하는 쪽은 Bow 클래스의 Attack 을 부르고,
Sword 클래스의 Swing 을 부르고.. 처럼 모든 무기를 일일이 알아야 한다.

하지만 IWeapon 이라는 약속을 먼저 잡아두면,
무기를 만드는 사람은 IWeapon 에서 정의한 약속만 지키면 되고,
무기를 쓰는 사람은 IWeapon 만 알면 된다. Bow 든 Burning 이든 상관없다.
즉, 양쪽이 서로의 내부 구현을 전혀 몰라도 협업이 가능해진다.

3. 흐름
1) 먼저 IWeapon 같은 interface 로 약속을 적는다.
2) 무기 개발자는 그 약속을 그대로 따라 Bow, Burning 등을 만든다.
3) 사용하는 쪽 (WeaponTestPlayer 등) 은 IWeapon 타입으로만 받아서 쓴다.
   -> 새 무기가 추가돼도 사용하는 쪽 코드는 한 줄도 안 바뀐다.
*/

public interface IWeapon
{
    // 무기 이름. WeaponProvider 에서 이름으로 무기를 찾아낼 때 사용된다.
    public string WeaponName { get; }

    // 공격 방향을 어떻게 정할지에 대한 타입. ("NearestEnemy", "MoveDirection" 등)
    // 이 값을 보고 플레이어가 알맞은 방향을 골라서 SetAttackDirection 으로 넣어준다.
    public string DirectionType { get; }

    // 실제 공격이 나갈 방향.
    public Vector2 AttackDirection { get; }

    // 데미지 값.
    public float Damage { get; }

    // 공격 한 번을 끝낸 후 다시 공격이 가능해질 때까지 걸리는 시간.
    public float AttackCoolTime { get; }

    // 현재 남아있는 쿨타임.
    public float CurrentCoolTime { get; }

    // 공격이 가능한 상태인지.
    public bool IsAttackReady { get; }

    // 실제 공격 동작. position 은 공격이 시작되는 위치 (보통 플레이어 위치).
    public void Attack(Vector2 position);

    // 매 프레임마다 쿨타임을 줄여달라고 요청할 때 호출된다.
    public void TickCoolTime(float deltaTime);

    // 공격 방향을 외부에서 정해준다.
    public void SetAttackDirection(Vector2 direction);
}
