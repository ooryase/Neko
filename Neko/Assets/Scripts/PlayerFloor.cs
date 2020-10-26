using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : MonoBehaviour
{
    private PlayerControll parent;
    private GameObject ladder2; // ボタンで出現するはしご
    private GameObject switch1; // ボタンの上のビックリマーク

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerControll>();

        // 非アクティブだとFindできない
        ladder2 = GameObject.Find("Ladder2");
        ladder2.SetActive(false);
        switch1 = GameObject.Find("Switch_Tex");
        switch1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // はしご判定
        if (other.gameObject.tag == "LadderBottom")
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                parent.ChangeState(State.Ladder);
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                parent.ChangeState(State.Nuetral);
            }
        }
        if (other.gameObject.tag == "LadderTop")
        {
            parent.ChangeState(State.Nuetral);
        }
        if (other.gameObject.tag == "Ladder")
        {
            parent.ChangeState(State.Ladder);
        }

        // ボタン判定
        if (other.gameObject.tag == "Switch")
        {
            // はしごが非アクティブなら表示する
            if (ladder2.activeSelf == false)
            {
                switch1.SetActive(true);
            }

            // スペースキーではしご出現（一旦）
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ladder2.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Switch")
        {
            // ボタンから離れたら非表示
            switch1.SetActive(false);
        }
    }
}
