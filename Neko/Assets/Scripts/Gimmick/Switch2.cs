using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2 : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private GameObject wall;
    private GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        tex = transform.GetChild(0).gameObject;
        wall = transform.GetChild(1).gameObject;
        block = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        if(block.GetComponent<Rigidbody>().useGravity == false)
        {
            tex.SetActive(true);
        }

        // スペースキーではしご出現（一旦）
        if (Input.GetKey(KeyCode.Space))
        {
            block.GetComponent<Rigidbody>().useGravity = true;
            tex.SetActive(false);
            wall.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // ボタンから離れたら非表示
        tex.SetActive(false);
    }
}
