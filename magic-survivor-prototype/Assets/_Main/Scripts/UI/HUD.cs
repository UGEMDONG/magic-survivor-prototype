using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum HUDState { Exp, Level,Time }
    public HUDState type;
    [SerializeField] Text text;
    [SerializeField] Slider slider;
    [SerializeField] GameObject playerObj;
    private IExpReceiver player;

    void Awake()
    {
        player = playerObj.GetComponent<IExpReceiver>();
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
            case HUDState.Time:
                            text.text = string.Format("Time:{0:F2}", Time.time);
                break;
        }
    }
}
