using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UITestPlayer : MonoBehaviour , IExpReceiver
{
    public float exp = 0;
    public float maxExp = 100;
    public int level = 1;
    public GameObject SkillPanel;
    public float Exp
    {
        get
        {
            return exp;
        }
        // set
        // {
        //     exp = value;
        // }
    }

    //     player.Exp = 0;  우측값이 setter 의 value 로 들어간다.
    // 경험치 구슬 쪽에서 사용
    public IReadOnlyList<IWeapon> Weapons
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }
    public void ReceiveWeapon(IWeapon weapon)
    {
        throw new System.NotImplementedException();
    }
    public float MaxExp => maxExp;
    public int Level => level;

    public void TakeExp(float amount)
    {
        exp += amount;
    }
    
    // 스킬 선택 UI 쪽에서 사용
    public void LevelUp()
    {
            level += 1;
    }
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeExp(20);
        }
        if (Exp >= MaxExp)
        {
            LevelUp();
            exp = 0;
            Debug.Log("Level Up! Current Level: " + Level);
        }
    }
}
