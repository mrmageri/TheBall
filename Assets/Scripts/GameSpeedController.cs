using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text speedText;
    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void SetGameSpeed()
    {
        Time.timeScale = slider.value;
        int speed = (int) slider.value;
        speedText.text = speed.ToString();
    }
}
