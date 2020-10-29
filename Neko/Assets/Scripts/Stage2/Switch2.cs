using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2 : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private GameObject wall1;
    private GameObject wall2;
    // ブロックが落ちるアニメ
    private Animator block_anim;

    // Start is called before the first frame update
    void Start()
    {
        tex = transform.GetChild(0).gameObject;
        wall1 = transform.GetChild(1).gameObject;
        wall2 = transform.GetChild(2).gameObject;
        block_anim = transform.GetChild(3).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        if(wall1.activeSelf)
        {
            tex.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && wall1.activeSelf)
        {
            tex.SetActive(false);
            wall1.SetActive(false);
            wall2.SetActive(true);

            block_anim.SetTrigger("Block");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // ボタンから離れたら非表示
        tex.SetActive(false);
    }
}
