using System.Collections.Generic;
using UnityEngine;

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

    void Awake()    // 원래 Start였는데 이런 매니저형 초기화는 Awake 추천!!
    {
        foreach (GameObject weaponObject in allWeaponsObject)
        {   
            // 씬에 게임오브젝트로 인스턴스 안하면 프리펩 훼손됨 클나!!
            var weapon = Instantiate(weaponObject, transform);
            allWeapons.Add(weapon.GetComponent<IWeapon>());
        }
    }
}
