using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject[] GameObjectsTohidden;
    private WebCam webCam;
    private EyeOpenChecker openChecker;
    private Transition transition;
    private float pushTime = 0.0f;

    [SerializeField] private bool canPause = true;

    public bool Pause { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Pause = false;
        var web = GameObject.FindGameObjectWithTag("WebCam");
        webCam = web.GetComponent<WebCam>();
        openChecker = web.GetComponent<EyeOpenChecker>();
        transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Transition>();
        webCam.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPause == false || openChecker.KEEP_EYE_OPEN == false)
        {
            pushTime = 0.0f;
            return;
        }


        if (Input.GetKey(KeyCode.Escape) || Input.GetButton("Pause"))
        {
            if (Pause) pushTime += Time.unscaledDeltaTime;
            else pushTime += Time.deltaTime;

            // 長押しでタイトルに戻る
            if (pushTime > 2.0f)
            {
                Time.timeScale = 1;
                Invoke("TitleBack", 1.0f);
                transition.FadeOut();
                pushTime = 0.0f;
                if (Pause)
                {
                    //サブシーンを破棄
                    SceneManager.UnloadSceneAsync("PauseScene");
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Pause"))
        {
            // ちょい押しでポーズ
            if (pushTime < 0.5f && IsInvoking() == false)
            {
                if (Pause == false)
                {
                    Time.timeScale = 0;
                    // 非表示にするオブジェクト
                    foreach (GameObject obj in GameObjectsTohidden)
                    {
                        obj.SetActive(false);
                    }

                    //メインシーンにサブシーンを追加表示する
                    //Application.LoadLevelAdditive("PauseScene");
                    SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);

                    webCam.GetComponent<MeshRenderer>().enabled = true;
                    Pause = true;
                }
                else
                {
                    Time.timeScale = 1;

                    foreach (GameObject obj in GameObjectsTohidden)
                    {
                        obj.SetActive(true);
                    }

                    //サブシーンを破棄してメインシーンを表示する
                    SceneManager.UnloadSceneAsync("PauseScene");

                    webCam.GetComponent<MeshRenderer>().enabled = false;
                    Pause = false;
                }
            }

            pushTime = 0.0f;
        }
    }

    void TitleBack()
    {
        SceneManager.LoadScene("Title");
    }

}