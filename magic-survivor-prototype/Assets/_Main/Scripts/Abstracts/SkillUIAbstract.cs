using System.IO;
using UnityEngine;
//SkillUI 어떤 스킬 띄울지 (본인 이건 엡스트렙에) 스킬 부여 (플레이어에게 이건 인터에)
public abstract class SkillUIAbstract: MonoBehaviour,ISkilUI
{
    protected abstract int SkillUp();
    public void GiveSkill(int skilID)
    {
    }
    void Start()
    {
        SkillUp();
        int b = 1; // 이후 플레이어가 고를 스킬 번호
        GiveSkill(b);
    }
}