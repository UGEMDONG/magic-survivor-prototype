using System.Collections.Generic;
using UnityEngine;

public interface IWeaponReceiver
{
    public IReadOnlyList<IWeapon> Weapons { get; }

    public void ReceiveWeapon(IWeapon weapon);
}
