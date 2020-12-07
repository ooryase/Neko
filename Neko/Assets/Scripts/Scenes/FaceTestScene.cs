using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class FaceTestScene : MonoBehaviour
{
    enum FaceState
    {
        None,
        Close,
        Open,
        Finish,
    }

    [SerializeField] private Transition transition = null;
    [SerializeField] private EyeOpenChecker openChecker = null;
    float timer = 3.0f;
    [SerializeField] private Animator open = null;
    [SerializeField] private Animator close = null;
    [SerializeField] private Text timerText = null;
    [SerializeField] private Slider slider = null;
    [SerializeField] private Text sliderValue = null;

    List<float> eyesSizeL = new List<float>();
    List<float> eyesSizeR = new List<float>();
    int interval = 0;
    public float EyeSizeL { get; private set; }
    public float EyeSizeR { get; private set; }

    FaceState state = FaceState.None;

    // Start is called before the first frame update
    void Start()
    {
        transition.FadeIn();
        Invoke("Close", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // エスケープで終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

        GameData.Instance.eyeOpenThreshold = Mathf.Clamp(GameData.Instance.eyeOpenThreshold + Input.GetAxis("Horizontal") / 100.0f, slider.minValue, slider.maxValue);
        slider.value = GameData.Instance.eyeOpenThreshold;
        sliderValue.text = GameData.Instance.eyeOpenThreshold.ToString("F2");

        switch (state)
        {
            case FaceState.None:
                timerText.text = "";
                break;
            case FaceState.Close:
                timerText.text = timer.ToString("F1");

                if (openChecker.KEEP_EYE_OPEN == false)
                {
                    timer = Mathf.Clamp(timer - Time.deltaTime, 0.0f, 3.0f);
                    if (timer == 0.0f)
                    {
                        timer = 3.0f;
                        close.SetTrigger("out");
                        Invoke("Open", 3.0f);
                        state = FaceState.None;
                    }
                }
                else
                {
                    timer = 3.0f;
                    timerText.text = "";
                }
                break;
            case FaceState.Open:

                timerText.text = timer.ToString("F1");

                timer = Mathf.Clamp(timer - Time.deltaTime, 0.0f, 3.0f);
                interval++;
                if (interval % 10 == 0)
                {
                    eyesSizeL.Add(openChecker.GetOpenL());
                    eyesSizeR.Add(openChecker.GetOpenR());
                }
                if (timer == 0.0f)
                {
                    open.SetTrigger("out");
                    state = FaceState.Finish;

                    EyeSizeL = eyesSizeL.Average();
                    EyeSizeR = eyesSizeR.Average();

                    //EyesData.Instance.EyeSizeL = EyeSizeL;
                    //EyesData.Instance.EyeSizeR = EyeSizeR;

                    timerText.text = "EyeSize  L : " + EyeSizeL.ToString("F2") + "　R : " + EyeSizeR.ToString("F2");
                }
                break;

            case FaceState.Finish:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Action1"))
                {
                    transition.FadeOut();
                    Invoke("LoadScene", 1.0f);
                }
                break;
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Stage0_1");
    }

    /// <summary>
    /// 目を閉じるテストスタート
    /// </summary>
    void Close()
    {
        close.SetTrigger("in");
        state = FaceState.Close;
    }

    /// <summary>
    /// 目を開けて大きさをはかる
    /// </summary>
    void Open()
    {
        open.SetTrigger("in");
        state = FaceState.Open;
    }
}
