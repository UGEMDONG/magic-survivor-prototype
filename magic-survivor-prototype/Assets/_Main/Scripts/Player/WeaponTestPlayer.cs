using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
전체 흐름 요약
1) IWeapon, IDamageable 같은 interface 로 역할을 약속.
2) WeaponBase, AttackObjectBase 같은 abstract 로 공통 코드를 한 번만 작성.
3) Bow, Burning, Arrow, Fire, TestEnemy 같은 구현체 클래스가 각자 자기 일만 함.
4) 사용하는 쪽 (이 플레이어 클래스) 은 interface 만 알고도 전부 다 굴림.
*/
public class WeaponTestPlayer : MonoBehaviour,IExpReceiver
{

    // IExpReceiver
    private float exp = 0;
    public float Exp => exp;
    public float MaxExp => 100;
    public int Level => 1;

    [SerializeField] private WeaponHolder weaponHolder;

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
    // 여기 weapons 리스트의 타입이 구체 클래스(Bow, Burning) 가 아니라 IWeapon 이라는
    // 점에 핵심. 어떤 무기든 IWeapon 이라는 약속만 지키면 이 리스트에 들어갈 수 있다.
    public IReadOnlyList<IWeapon> Weapons { get; }

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
        if (nearestEnemy != null)
        {
            directionToNearestEnemy = (nearestEnemy.gameObject.transform.position - transform.position).normalized;
            
        }
        if (rb.linearVelocity != Vector2.zero)
        {
            directionToMove = (rb.linearVelocity - Vector2.zero).normalized;
        }
    }

    void Start()
    {
        if (weaponHolder == null)
        {
            weaponHolder = GetComponent<WeaponHolder>();
        }
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sortingOrder = 10;   // 플레이어 안가려지게
    }

    void Update()
    {
        moveLeft = Keyboard.current.aKey.isPressed;
        moveRight = Keyboard.current.dKey.isPressed;
        moveUp = Keyboard.current.wKey.isPressed;
        moveDown = Keyboard.current.sKey.isPressed;

        nearestEnemy = GetNearestEnemy();
        SetPlayerAttackDirections();
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
        if (weaponHolder != null)
        {
            weaponHolder.SetMoveDirection(inputDirection);
        }
    }
}
