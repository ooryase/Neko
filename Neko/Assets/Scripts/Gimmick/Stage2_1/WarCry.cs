using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarCry : MonoBehaviour
{
    [SerializeField] private NekoPredetor nekoPredetor = null;
    private PlayerFollow playerFollow;
    [SerializeField] private Transform zoom = null;
    [SerializeField] private BGMManagerBoss bgm = null;

    // Start is called before the first frame update
    void Start()
    {
        playerFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerFollow>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            nekoPredetor.WarCry();
            gameObject.SetActive(false);
            Invoke("Shake", 1.5f);
            Invoke("Zoomoff", 2.5f);
            zoom.localScale = new Vector3(30, 1, 1);
        }
    }

    void Shake()
    {
        playerFollow.Shake(0.3f);
    }

    void Zoomoff()
    {
        zoom.localScale = new Vector3(1, 1, 1);
        bgm.BGMStart();
    }
}
