using UnityEngine;
using UnityEngine.InputSystem;

public class UITestPlayer : MonoBehaviour
{
    public int Exp = 0;
    public int MaxExp = 100;
    public int Level = 1;
    public int Money = 10;
    public GameObject SkillPanel;

    void Update()
    {
        // 증가 테스트
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Exp += 10;
        }else if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Level += 1;
        }
        else if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Money += 50;
        }
        // 레벨업 체크
        if (Exp >= MaxExp)
        {
            Exp = 0;
            MaxExp += 50;
            Level++;
            Debug.Log("레벨업! 현재 레벨: " + Level);

        }
    }
}
