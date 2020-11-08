using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private GameObject trans;
    private Transition transition;

    // Start is called before the first frame update
    void Start()
    {
        transition = trans.GetComponent<Transition>();
        transition.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // 1秒後にシーン遷移
        transition.FadeOut();
        Invoke("LoadScene", 1.0f);
    }

    private void LoadScene()
    {
        string name = SceneManager.GetActiveScene().name;

        switch (name)
        {
            case "Stage1":
                SceneManager.LoadScene("Stage2");
                break;
            case "Stage2":
                SceneManager.LoadScene("Title");
                break;

            default:
                SceneManager.LoadScene("Stage1");
                break;
        }
    }
}
