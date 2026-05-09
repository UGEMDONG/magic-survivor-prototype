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
    private Vector2 position;
    public Vector2 Position => position;

    private Collider2D hitCollider2D;
    public Collider2D HitCollider2D => hitCollider2D;

    private float maxHp = 100f;
    public float MaxHp => maxHp;

    private float currentHp;
    public float CurrentHp => currentHp;

    private bool isDead = false;
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
        Debug.Log(currentHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
