using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject[] GameObjectsTohidden;
    private WebCam webCam;
    private EyeOpenChecker openChecker;

    public bool Pause { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Pause = false;
        var web = GameObject.FindGameObjectWithTag("WebCam");
        webCam = web.GetComponent<WebCam>();
        openChecker = web.GetComponent<EyeOpenChecker>();
        webCam.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (openChecker.KEEP_EYE_OPEN)
        {
            // エスケープ
            if (Input.GetKeyDown(KeyCode.Escape))
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
                    Application.LoadLevelAdditive("PauseScene");

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
                    SceneManager.UnloadScene("PauseScene");

                    webCam.GetComponent<MeshRenderer>().enabled = false;
                    Pause = false;
                }
            }
        }
    }
}
