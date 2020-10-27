using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3 : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private GameObject arm;
    private GameObject arm_after;

    // Start is called before the first frame update
    void Start()
    {
        tex = transform.GetChild(0).gameObject;
        arm = transform.GetChild(1).gameObject;
        arm_after = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        if (arm.activeSelf)
        {
            tex.SetActive(true);
        }

        // スペースキーではしご出現（一旦）
        if (Input.GetKey(KeyCode.Space))
        {
            tex.SetActive(false);
            arm.SetActive(false);
            arm_after.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // ボタンから離れたら非表示
        tex.SetActive(false);
    }
}
