using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3 : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private Animator arm_anim;
    private GameObject coll;

    // Start is called before the first frame update
    void Start()
    {
        tex = transform.GetChild(0).gameObject;
        // Armのアニメーター取得
        arm_anim = transform.GetChild(1).gameObject.GetComponent<Animator>();

        coll = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        if (coll.activeSelf == false)
        {
            tex.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coll.activeSelf == false ||
            Input.GetButtonDown("Action1") && coll.activeSelf == false)
        {
            tex.SetActive(false);
            arm_anim.SetTrigger("Move"); // アニメ実行
            coll.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // ボタンから離れたら非表示
        tex.SetActive(false);
    }
}
