using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private Transition transition = null;

    private AudioSource se;
    // Start is called before the first frame update
    void Start()
    {
        se = GetComponent<AudioSource>();
        transition.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        // エスケープで終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Action1"))
        {
            if (IsInvoking() == false)
            {
                se.Play();
                transition.FadeOut();
                Invoke("LoadScene", 1.0f);
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Stage0_1");
    }
}
