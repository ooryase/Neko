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
        if (canPause == false) return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            if (IsInvoking() == false)
            {
                if (Pause == false)
                {
                    PauseStart();
                }
                else
                {
                    PauseEnd();
                }
            }
        }
    }

    void PauseStart()
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

        //webCam.GetComponent<MeshRenderer>().enabled = true;
        Pause = true;
    }

    public void PauseEnd()
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

    public void TitleBackStart()
    {
        PauseEnd();

        Invoke("TitleBack", 1.0f);
        transition.FadeOut();
    }

    public void RebootStart()
    {
        PauseEnd();

        Invoke("Reboot", 1.0f);
        transition.FadeOut();
    }

    void TitleBack()
    {
        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// 再起動
    /// </summary>
    void Reboot()
    {

#if !UNITY_EDITOR
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
#else
        TitleBack(); // エディターの場合タイトルに戻るだけ
#endif
    }

    public void LandMarkTrue()
    {
        webCam.GetComponent<MeshRenderer>().enabled = true;
    }
    public void LandMarkFalse()
    {
        webCam.GetComponent<MeshRenderer>().enabled = false;
    }
}