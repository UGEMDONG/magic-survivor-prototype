using UnityEngine;
using UnityEngine.UI;

public class JohnNaSimpleHpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void SetValue(float value) => slider.value = value;
}
