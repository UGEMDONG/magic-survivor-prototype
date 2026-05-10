using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum HUDState { Exp, Level,money,Time }
    public HUDState type;
    [SerializeField] Text text;
    [SerializeField] Slider slider;
    [SerializeField] UITestPlayer player;
    void Awake()
    {
    }

    private void Update()
    {
        switch (type)
        {
            case HUDState.Exp:
                    slider.value = (float)player.Exp / player.MaxExp;
                break;
            case HUDState.Level:
                text.text = string.Format("Lv.{0:F0}", player.Level);
                break;
            case HUDState.money:
                    text.text = string.Format("Money:{0:F0}", player.Money); 
                break;
            case HUDState.Time:
                // 시간은 어캐해야할지 모르겠음
                break;
        }
    }
}
