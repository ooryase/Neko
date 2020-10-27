using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch1 : MonoBehaviour
{
    public GameObject ladder2; // ボタンで出現するはしご
    private GameObject tex; // ボタンの上のビックリマーク

    // Start is called before the first frame update
    void Start()
    {
        tex = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // はしごが非アクティブなら表示する
        if (ladder2.activeSelf == false)
        {
            tex.SetActive(true);
        }

        // スペースキーではしご出現（一旦）
        if (Input.GetKey(KeyCode.Space))
        {
            ladder2.SetActive(true);
            tex.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // ボタンから離れたら非表示
        tex.SetActive(false);
    }
}
