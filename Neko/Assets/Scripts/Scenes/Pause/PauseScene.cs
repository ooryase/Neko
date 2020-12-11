using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PauseScene : MonoBehaviour
{
    private EyeOpenChecker openChecker = null;

    [SerializeField] private Text timerText = null;
    [SerializeField] private Slider slider = null;
    [SerializeField] private Text sliderValue = null;

    int interval = 0;

    // Start is called before the first frame update
    void Start()
    {
        openChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();

        timerText.text = "EyeSize";
    }

    // Update is called once per frame
    void Update()
    {
        GameData.Instance.eyeOpenThreshold = Mathf.Clamp(GameData.Instance.eyeOpenThreshold + Input.GetAxisRaw("Horizontal") / 100.0f, slider.minValue, slider.maxValue);
        slider.value = GameData.Instance.eyeOpenThreshold;
        sliderValue.text = GameData.Instance.eyeOpenThreshold.ToString("F2");

        interval++;
        if (interval >= 20)
        {
            timerText.text = "EyeSize  L : " + openChecker.GetOpenL().ToString("F2") + "　R : " + openChecker.GetOpenL().ToString("F2");
            interval = 0;
        }
    }
}
