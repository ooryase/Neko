using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private string[] storys;
    private int i = 0;

    private AudioSource audioSource;
    private EyeOpenChecker openChecker;
    [SerializeField] private Text text = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        openChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();

        storys = new string[11];
        storys[0] = "そ れ で も 行 く ん だ ね";
        storys[1] = "い か な い で";
        storys[2] = "お 別 れ か な";
        storys[3] = "お い て か な い で";
        storys[4] = "で も お 別 れ じゃ な い よ";
        storys[5] = "し に た く な い";
        storys[6] = "き っ と 探 し に 行 く か ら";
        storys[7] = "す て な い で";
        storys[8] = "じゃ あ さ よ う な ら";
        storys[9] = "こ こ か ら だ し て っ ！";
        storys[10] = "ま た ね";

        text.text = storys[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (openChecker.EYE_OPEN || openChecker.EYE_CLOSE)
        {
            i++;
            //OutOfRangeしたので応急処置
            i = (i > 10) ? 10 : 0;
            if (i == storys.Length)
            {
                Invoke("Title", 1.5f);
                audioSource.Play();
            }
            else
            {
                text.text = storys[i];
                if(i % 2 == 1)
                {
                    text.color = new Color(255, 0, 0);
                }
                else
                {
                    text.color = new Color(255, 255, 255);
                }
            }
        }
    }

    void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
