using UnityEngine;

public interface IExpReceiver: IWeaponReceiver
{
    public float Exp { get; }

    // 경험치 구슬 쪽에서 사용
    public void TakeExp(float amount);
    
    // 스킬 선택 UI 쪽에서 사용
    public void LevelUp();

}
