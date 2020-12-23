using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private string[] storys, darkStorys;
    private int i = 0;

    private AudioSource audioSource;
    private EyeOpenChecker openChecker;
    [SerializeField] private Text text = null;

    private float timer = 0.0f;
    private float alpha = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        openChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();

        storys = new string[7];
        storys[0] = "そ れ で も 行 く ん だ ね";
        storys[1] = "お 別 れ か な";
        storys[2] = "で も お 別 れ じゃ な い よ";
        storys[3] = "き っ と 探 し に 行 く か ら";
        storys[4] = "じゃ あ さ よ う な ら";
        storys[5] = "ま た ね";
        storys[6] = "";

        darkStorys = new string[7];
        darkStorys[0] = "い か な い で";
        darkStorys[1] = "お い て か な い で";
        darkStorys[2] = "し に た く な い";
        darkStorys[3] = "す て な い で";
        darkStorys[4] = "こ こ か ら だ し て っ ！";
        darkStorys[5] = ". . .";
        darkStorys[6] = "";

        text.text = storys[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 4.0f)
        {
            timer = 0.0f;
            if (i < storys.Length - 1) i++;

            if (i == storys.Length - 1 && IsInvoking() == false)
            {
                Invoke("Title", 4.0f);
                audioSource.Play();
            }
        }
        else if (timer > 3.0f)
        {
            alpha = Mathf.Lerp(alpha, 0.0f, 0.08f); // フェードアウト
        }
        else if (timer < 1.0f)
        {
            alpha = Mathf.Lerp(alpha, 1.0f, 0.08f); // フェードイン
        }

        timer += Time.deltaTime;


        if (openChecker.KEEP_EYE_OPEN)
        {
            text.color = new Color(255, 255, 255, alpha);
            text.text = storys[i];
        }
        else
        {
            text.color = new Color(255, 0, 0, alpha);
            text.text = darkStorys[i];
        }
    }

    void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
