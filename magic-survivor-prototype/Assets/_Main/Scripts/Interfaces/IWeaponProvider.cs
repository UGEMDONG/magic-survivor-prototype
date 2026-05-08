using System.Collections.Generic;

public interface IWeaponProvider
{
    public IReadOnlyList<IWeapon> AllWeapons { get; }
    public IWeapon NameToIWeapon(string weaponName);
}
