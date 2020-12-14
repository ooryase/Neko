using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private Transition transition = null;

    // Start is called before the first frame update
    void Start()
    {
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
            case "Stage0_1":
                SceneManager.LoadScene("Stage1_1");
                break;
            case "Stage1_1":
                SceneManager.LoadScene("Stage1_2");
                break;

            default:
                SceneManager.LoadScene("Title");
                break;
        }
    }
}
