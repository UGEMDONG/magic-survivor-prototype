using System.Collections;
using UnityEngine;

// 버닝 클래스는 전체적으로 넘후 좋음. 애초에 WeaponBase를 상속받고 있었고, 자신만의 필드가 있어서 구현하기 좋은 케이스
public class Burning : WeaponBase
{
    [Header("Burning Fields")]
    [SerializeField] private GameObject firePrefab;
    [Space]
    [SerializeField] private int maxBurningCount = 10;
    [SerializeField] float attackDuration = 2f;

    public override void TryAttack()
    {
        StartCoroutine(FlameAttack());
    }
    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
    }

    IEnumerator FlameAttack()
    {
        // 라디안 값으로 계산((삼각함수 대입하기 편하게
        float tickAngle = Mathf.PI * 2f / maxBurningCount;  // 각 쪼개기
        for (int i = 0; i < maxBurningCount; i++)
        {
            float roll = tickAngle * i;
            Vector2 flameDirection = new Vector2(Mathf.Sin(roll), Mathf.Cos(roll));

            GameObject fireObject = Instantiate(firePrefab, transform);
            fireObject.transform.localPosition = Vector3.zero;
            float angle = Vector2.SignedAngle(new Vector2(0, 1), flameDirection);
            fireObject.transform.Rotate(0f, 0f, angle);
            Fire fire = fireObject.GetComponent<Fire>();
            fire.owner = this;
            fire.damage = damage;

            yield return new WaitForSeconds(attackDuration / maxBurningCount);
        }
    }
}
