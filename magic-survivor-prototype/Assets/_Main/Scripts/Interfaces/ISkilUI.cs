using UnityEngine;

//SkillUI 어떤 스킬 띄울지 (본인 이건 엡스트렙에) 스킬 부여 (플레이어에게 이건 인터에)
public interface ISkilUI
{
    public void GiveSkill(int skilID);
}