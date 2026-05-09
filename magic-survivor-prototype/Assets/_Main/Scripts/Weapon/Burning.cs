using UnityEngine;

/*
1. abstract 의 이득
Bow 와 Burning 을 비교해보면,
Bow: IWeapon 만 구현. weaponName, damage, currentCoolTime, TickCoolTime,
SetAttackDirection ... 까지 전부 본인이 직접 작성했다.

Burning: WeaponBase (abstract) 를 상속. 위에 적힌 공통 코드는 부모가 다 가지고
있으므로, Burning 은 Burning 만의 고유한 공격 패턴인 Attack 만
override 하면 끝난다.

코드 길이를 보면 Burning 이 훨씬 짧다. 그런데 IWeapon 약속은 Bow 와 똑같이 다 지킨다.
즉, 외부에서 Burning 을 IWeapon 으로 다뤄도 아무 문제 없이 동작한다.

2. abstract(interface) 에 있는 약속만 지키면 된다.
아래의 maxBurningCount, currentBurningCount, nextFiringTick 같은 필드는
IWeapon 약속서에는 전혀 없는 Burning 만의 내부 상태다.
하지만 IWeapon 의 약속은 모두 지켜진 상태이기 때문에
Burning 만의 고유 동작을 개발하면서도 이 무기를 사용하는 쪽은 코드를 한 줄도 바꾸지 않아도 정상작동 한다.
*/


public class Burning : WeaponBase
{

    // Burning 만 가지고 있는 내부 상태. (IWeapon 과 무관, WeaponBase 와도 무관)
    // 쿨타임 한 번에 불 10개를 일정 간격으로 내뿜는다 라는 Burning 고유 룰을
    // 처리하기 위한 변수들이다.
    private int maxBurningCount = 10;
    private int currentBurningCount = 0;
    private int nextFiringTick = 10;
    private int currentNextFiringTick = 0;

    // WeaponBase.Attack 은 virtual 이었고, Burning 은 그걸
    // override 해서 자기 방식대로 다시 쓴다.
    public override void Attack(Vector2 position)
    {
        Debug.Log(attackCoolTime);
        currentNextFiringTick += 1;
        if (currentNextFiringTick >= nextFiringTick)
        {
            currentNextFiringTick = 0;
            currentBurningCount += 1;
            if (currentBurningCount >= maxBurningCount)
            {
                currentBurningCount = 0;
                isAttackReady = false;
                currentCoolTime = attackCoolTime;
            }
            GameObject fireObject = Instantiate(firePrefab);
            fireObject.transform.position = position;
            float angle = Vector2.SignedAngle(new Vector2(0, 1), attackDirection);
            fireObject.transform.Rotate(0f, 0f, angle);
            Fire fire = fireObject.GetComponent<Fire>();
            fire.direction = attackDirection;
            fire.damage = damage;
        }
    }


    [SerializeField]
    private GameObject firePrefab;
}
