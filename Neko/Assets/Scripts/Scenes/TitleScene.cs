using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject trans;
    private Transition transition;

    [SerializeField] private AudioSource se;
    // Start is called before the first frame update
    void Start()
    {
        transition = trans.GetComponent<Transition>();
        transition.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Action1"))
        {
            // 一応フェードインが終わってたら
            if (transition.IsEnd())
            {
                se.Play();
                transition.FadeOut();
                Invoke("LoadScene", 1.0f);
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Stage1");
    }
}
