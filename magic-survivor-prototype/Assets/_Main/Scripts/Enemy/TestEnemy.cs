using UnityEngine;

/*
TestEnemy 는 IDamageable 약속을 따라 만든 구현체 클래스 (더미데이터)
Arrow / Fire 입장에서는 이 클래스를 전혀 몰라도 된다.
"IDamageable 이면 TakeDamage 호출" 이라는 약속만 있으면 자동으로 동작한다.

나중에 BossEnemy, FlyingEnemy 같은 새 적을 만들 때도, IDamageable 만
구현해두면 기존 무기/공격 시스템이 즉시 동작한다
= 코드 변경 없이 확장 가능.
*/
public class TestEnemy : MonoBehaviour, IDamageable
{
    // 제발 프로퍼티는 밑에 분리해서 써주세요... 쌰갈
    private Collider2D hitCollider2D;
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float currentHp;
    private bool isDead = false;
    [SerializeField] JohnNaSimpleHpBar hpBar;

    public Vector2 Position => transform.position;
    public float MaxHp => maxHp;
    public Collider2D HitCollider2D => hitCollider2D;
    public float CurrentHp => currentHp;
    public bool IsDead => isDead;
    
    void Start()
    {
        currentHp = maxHp;
    }

    // IDamageable 약속의 실제 구현. 이 적은 그냥 hp 깎고 죽으면 Destroy.
    // 다른 적은 여기서 방어력 계산, 피격 모션, 사운드 등을 자유롭게 넣을 수 있다.
    public void TakeDamage(float amount)
    {
        currentHp -= amount;
        // Debug.Log(currentHp);
        hpBar.SetValue(currentHp / maxHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        hpBar.SetValue(0f); // 의미 없긴해..
        Destroy(gameObject);
    }
}
