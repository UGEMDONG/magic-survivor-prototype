using System.Collections.Generic;
using UnityEngine;

public class WeaponTestPlayer : MonoBehaviour,
IExpReceiver
{

    // IExpReceiver
    private float exp = 0;
    public float Exp => exp;

    // 경험치 구슬 쪽에서 사용
    public void TakeExp(float amount)
    {
        return;
    }
    
    // 스킬 선택 UI 쪽에서 사용
    public void LevelUp()
    {
        return;
    }
    //

    // IWeaponReceiver
    private List<IWeapon> weapons = new List<IWeapon>();
    public IReadOnlyList<IWeapon> Weapons { get; }

    public void ReceiveWeapon(IWeapon weapon)
    {
        weapons.Add(weapon);
    }
    //

    void Start()
    {
        ReceiveWeapon();
    }
}
