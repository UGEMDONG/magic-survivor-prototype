using System.Collections.Generic;
using UnityEngine;

public class WeaponTestPlayer : MonoBehaviour,
IExpReceiver
{

    // IExpReceiver
    private float exp = 0;
    public float Exp => exp;
    // 더미 데이터라서 구현하지 않음
    public void TakeExp(float amount)
    {
        return;
    }
    public void LevelUp()
    {
        return;
    }
 

    // IWeaponReceiver
    private List<IWeapon> weapons = new List<IWeapon>();
    public IReadOnlyList<IWeapon> Weapons { get; }

    public void ReceiveWeapon(IWeapon weapon)
    {
        weapons.Add(weapon);
    }
    //

    [SerializeField]
    private WeaponProvider weaponProvider;
    private TestEnemy[] enemies;
    private TestEnemy nearestEnemy;
    private Vector2 directionToNearestEnemy = Vector2.zero;
    private Vector2 directionToMove = Vector2.zero;


    TestEnemy GetNearestEnemy()
    {
        enemies = FindObjectsByType<TestEnemy>(FindObjectsSortMode.None);   
        float minDistance = -1f;
        TestEnemy nearestEnemy = null;
        foreach (TestEnemy enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.gameObject.transform.position, transform.position);
            if (minDistance > distance || minDistance == -1)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    void SetAttackDirections()
    {
        directionToNearestEnemy = (nearestEnemy.gameObject.transform.position - transform.position).normalized;
        
    }

    void Start()
    {
        ReceiveWeapon(weaponProvider.NameToIWeapon("Bow"));
    }

    void Update()
    {
        nearestEnemy = GetNearestEnemy();
        SetAttackDirections();

        foreach (IWeapon weapon in weapons)
        {
            weapon.TickCoolTime(Time.deltaTime);
            if (weapon.IsAttackReady)
            {
                if (weapon.DirectionType == "NearestEnemy")
                {
                    if (nearestEnemy != null)
                    {
                        weapon.SetAttackDirection(directionToNearestEnemy);
                        weapon.Attack(transform.position);
                    }
                }
            }
        }
    }
}
