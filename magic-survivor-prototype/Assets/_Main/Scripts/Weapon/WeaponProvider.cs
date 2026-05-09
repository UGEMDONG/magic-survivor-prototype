using System.Collections.Generic;
using UnityEngine;

/*
WeaponProvider 는 IWeaponProvider 약속의 실제 구현
외부에서는 이 클래스가 List<GameObject> 로 무기들을 들고 있는지, 어떻게 캐싱하는지
전혀 알 필요가 없다. "IWeapon 을 이름으로 달라고 하면 줄 수 있다" 는 결과만 보장된다.

핵심 로직: GameObject 에서 IWeapon 으로 끌어올리기
Bow.cs 든 Burning.cs 든 모두 MonoBehaviour 컴포넌트로 GameObject 에 붙어 있고,
동시에 IWeapon 을 구현한다. 그래서 GetComponent<IWeapon>() 으로 받아오면
받는 쪽은 이게 Bow 인지 Burning 인지 모른 채로 IWeapon 으로 다룰 수 있다.
여기서 interface 의 진짜 효과가 생김. 각 무기의 구현체 타입을 신경 쓰지 않아도 된다 (예: Bow 든 Burning 이든 상관없음).
*/

public class WeaponProvider : MonoBehaviour, IWeaponProvider
{
    [SerializeField]
    private List<GameObject> allWeaponsObject = new List<GameObject>();
    private List<IWeapon> allWeapons = new List<IWeapon>();
    public IReadOnlyList<IWeapon> AllWeapons => allWeapons;

    public IWeapon NameToIWeapon(string weaponName)
    {
        foreach (IWeapon weapon in allWeapons)
        {
            if (weapon.WeaponName == weaponName)
            {
                Debug.Log($"return {weaponName}");
                return weapon;
            }
        }
        Debug.Log($"null {weaponName}");
        return null;
    }

    void Start()
    {
        // 인스펙터에 꽂아둔 GameObject 들에서 IWeapon 컴포넌트만 뽑아 보관한다.
        // 이때부터 이 리스트의 원소들은 IWeapon이라는 추상 개념으로만 다뤄진다.
        foreach (GameObject weaponObject in allWeaponsObject)
        {
            allWeapons.Add(weaponObject.GetComponent<IWeapon>());
        }
    }
}
