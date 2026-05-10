using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPanel : MonoBehaviour
{
    Button button;

    //여기서부턴 인터페이스를 활용하여 구현해야할것으로 보이나 어캐해야할지 모르겠음
    private void OnEnable()
    {
        Debug.Log("스킬 패널이 열렸습니다.");
        Time.timeScale = 0f; // 게임 일시정지
    }

}
