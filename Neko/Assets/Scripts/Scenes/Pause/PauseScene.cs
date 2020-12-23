using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class PauseScene : MonoBehaviour
{
    private EyeOpenChecker openChecker = null;

    [SerializeField] private Text eyeL = null;
    [SerializeField] private Text eyeR = null;
    [SerializeField] private Slider slider = null;
    [SerializeField] private Slider gaugeL = null;
    [SerializeField] private Slider gaugeR = null;
    [SerializeField] private Text sliderValue = null;
    [SerializeField] private Text explanation = null;
    [SerializeField] private Canvas canvas = null;

    [SerializeField] private Button button = null;

    [SerializeField] private EventSystem eventSystem = null;

    private PauseManager pauseManager;
    private bool onFaceTest = false;

    //int interval = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        openChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseManager>();

        button.Select();

        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onFaceTest)
        {
            GameData.Instance.eyeOpenThreshold = Mathf.Clamp(GameData.Instance.eyeOpenThreshold + Input.GetAxisRaw("Horizontal") / 200.0f, slider.minValue, slider.maxValue);
            slider.value = GameData.Instance.eyeOpenThreshold;
            sliderValue.text = GameData.Instance.eyeOpenThreshold.ToString("F2");


            eyeL.text = "L        " + openChecker.GetOpenL().ToString("F2");
            eyeR.text = "R        " + openChecker.GetOpenR().ToString("F2");

            gaugeL.value = openChecker.GetOpenL();
            gaugeR.value = openChecker.GetOpenR();
        }

        if(eventSystem.currentSelectedGameObject == null)
        {
            button.Select();
        }

        switch (eventSystem.currentSelectedGameObject.name)
        {
            case "Back":
                explanation.text = "ゲームに戻ります。";
                break;
            case "Face":
                explanation.text = "目の判定を調節します。\nEyesLevelより目が大きいと、「目が開いている」という判定になります。";
                break;
            case "Title":
                explanation.text = "タイトルに戻ります。";
                break;
            case "Reboot":
                explanation.text = "再起動します。";
                break;
        }
    }

    public void OnPushBack()
    {
        pauseManager.PauseEnd();
    }

    public void OnPushFace()
    {
        if (onFaceTest)
        {
            onFaceTest = false;
            canvas.enabled = false;

            pauseManager.LandMarkFalse();
        }
        else
        {
            onFaceTest = true;
            canvas.enabled = true;

            pauseManager.LandMarkTrue();
        }

    }

    public void OnPushTitle()
    {
        pauseManager.TitleBackStart();
    }

    public void OnPushReboot()
    {
        pauseManager.RebootStart();
    }
}
