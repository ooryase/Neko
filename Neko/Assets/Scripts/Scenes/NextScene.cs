using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private Transition transition;
    private bool next;

    // Start is called before the first frame update
    void Start()
    {
        next = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        next = true;

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
