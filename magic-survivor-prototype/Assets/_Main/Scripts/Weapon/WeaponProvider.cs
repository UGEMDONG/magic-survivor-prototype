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
                return weapon;
            }
        }

        return null;
    }

    void Start()
    {
        foreach (GameObject weaponObject in allWeaponsObject)
        {
            allWeapons.Add(weaponObject.GetComponent<IWeapon>());
        }
    }
}
