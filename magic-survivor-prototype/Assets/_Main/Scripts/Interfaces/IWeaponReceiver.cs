using System.Collections.Generic;
using UnityEngine;

// =============================================================================
// [IWeaponReceiver 는 "무기를 받아 들 수 있는 존재" 라는 약속]
// 플레이어든, 동료 NPC 든, 무기를 장착하는 어떤 시스템이든, "무기를 건네받아 가지고
// 있을 수 있다" 는 능력만 가지면 된다.
//
// 이 약속만 잡아두면, 무기를 건네주는 쪽 (예: 상점, 보상 시스템, 스킬 UI) 은
// "받는 쪽이 플레이어인지 NPC 인지" 알 필요가 없다.
// IWeaponReceiver 라는 약속만 알면 ReceiveWeapon 을 호출할 수 있다.
// =============================================================================
public interface IWeaponReceiver
{
    public IReadOnlyList<IWeapon> Weapons { get; }

    public void ReceiveWeapon(IWeapon weapon);
}
