using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
