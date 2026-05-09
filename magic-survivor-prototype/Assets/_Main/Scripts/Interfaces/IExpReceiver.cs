using UnityEngine;

// =============================================================================
// [interface 도 다른 interface 를 상속할 수 있다]
// IExpReceiver 는 IWeaponReceiver 를 상속하고 있다.
// 즉 "경험치를 받을 수 있는 존재는, 무기도 받을 수 있는 존재여야 한다" 라는 의미.
// (게임 룰에서 레벨업 시 무기/스킬을 고르도록 만들었기 때문에 이렇게 묶였다.)
//
// 이렇게 약속들을 계층적으로 묶어두면,
//   - 경험치 구슬(EXP Orb) 쪽: IExpReceiver 만 보고 TakeExp 호출
//   - 스킬 선택 UI 쪽:        IExpReceiver 만 보고 LevelUp 호출
//   - 무기를 건네주는 쪽:      IWeaponReceiver 로만 봐도 ReceiveWeapon 호출 가능
// 이렇게 보는 시점에 따라 "필요한 약속만" 가져다 쓸 수 있게 된다.
// =============================================================================
public interface IExpReceiver: IWeaponReceiver
{
    public float Exp { get; }

    // 경험치 구슬 쪽에서 사용
    public void TakeExp(float amount);
    
    // 스킬 선택 UI 쪽에서 사용
    public void LevelUp();

}
