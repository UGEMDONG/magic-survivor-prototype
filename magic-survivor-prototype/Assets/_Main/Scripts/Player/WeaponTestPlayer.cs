using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Vector2 directionToMove = new Vector2(0, 1);

    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private Vector2 inputDirection = Vector2.zero;

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

    void SetPlayerAttackDirections()
    {
        directionToNearestEnemy = (nearestEnemy.gameObject.transform.position - transform.position).normalized;
        
        if (rb.linearVelocity != Vector2.zero)
        {
            directionToMove = (rb.linearVelocity - Vector2.zero).normalized;
        }
    }

    void Start()
    {
        ReceiveWeapon(weaponProvider.NameToIWeapon("Bow"));
        ReceiveWeapon(weaponProvider.NameToIWeapon("Burning"));
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveLeft = Keyboard.current.aKey.isPressed;
        moveRight = Keyboard.current.dKey.isPressed;
        moveUp = Keyboard.current.wKey.isPressed;
        moveDown = Keyboard.current.sKey.isPressed;

        nearestEnemy = GetNearestEnemy();
        SetPlayerAttackDirections();

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
                else if (weapon.DirectionType == "MoveDirection")
                {
                    weapon.SetAttackDirection(directionToMove);
                    weapon.Attack(transform.position);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if ((moveLeft && moveRight) || (moveLeft == false && moveRight == false))
        {
            inputDirection.x = 0;
        }
        else
        {
            if (moveLeft)
            {
                inputDirection.x = -1;
            }
            else if (moveRight)
            {
                inputDirection.x = 1;
            }
        }

        if ((moveUp && moveDown) || (moveUp == false && moveDown == false))
        {
            inputDirection.y = 0;
        }
        else
        {
            if (moveUp)
            {
                inputDirection.y = 1;
            }
            else if (moveDown)
            {
                inputDirection.y = -1;
            }
        }

        rb.linearVelocity = new Vector2(inputDirection.x * 5f, inputDirection.y * 5f);

    }
}
