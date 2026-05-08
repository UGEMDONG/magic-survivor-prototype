using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{

    // IWeapon 구현
    private string weaponName = "Bow";
    private Vector2 attackDirection;
    private string directionType = "NearestEnemy";
    private float damage = 50f;
    [SerializeField]
    private float attackCoolTime = 1f;
    private float currentCoolTime;
    private bool isAttackReady;

    public string WeaponName => weaponName;
    public Vector2 AttackDirection => attackDirection;
    public string DirectionType => directionType;
    public float Damage => damage;
    public float AttackCoolTime => attackCoolTime;
    public float CurrentCoolTime => currentCoolTime;
    public bool IsAttackReady => isAttackReady;

    public void Attack(Vector2 position)
    {
        Debug.Log(attackDirection);
        GameObject arrowObject = Instantiate(arrowPrefab);
        arrowObject.transform.position = position;
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.direction = attackDirection;
        isAttackReady = false;
        currentCoolTime = attackCoolTime;
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

    public void SetAttackDirection(Vector2 direction)
    {
        attackDirection = direction;
    }
    //

    [SerializeField]
    private GameObject arrowPrefab;

    void Start()
    {
        currentCoolTime = attackCoolTime;
    }



}
