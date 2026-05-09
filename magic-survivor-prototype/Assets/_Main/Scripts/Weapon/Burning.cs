using UnityEngine;

public class Burning : WeaponBase
{

    private int maxBurningCount = 10;
    private int currentBurningCount = 0;
    private int nextFiringTick = 10;
    private int currentNextFiringTick = 0;
    public override void Attack(Vector2 position)
    {
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
            Debug.Log(attackDirection);
            GameObject fireObject = Instantiate(firePrefab);
            fireObject.transform.position = position;
            float angle = Vector2.SignedAngle(new Vector2(0, 1), attackDirection);
            fireObject.transform.Rotate(0f, 0f, angle);
            Fire fire = fireObject.GetComponent<Fire>();
            fire.direction = attackDirection;
        }
    }


    [SerializeField]
    private GameObject firePrefab;

    protected override void Start()
    {
        weaponName = "Burning";
        damage = 10f;
        attackCoolTime = 5f;

        base.Start();
    }
}
