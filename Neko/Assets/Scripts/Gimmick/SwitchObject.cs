using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchObject : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private bool switch_on;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tex = transform.GetChild(0).gameObject;
        switch_on = false;
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // 押す前なら表示する
        if (switch_on == false)
        {
            tex.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Action1"))
        {
            action_on();

            switch_on = true;
            tex.SetActive(false);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // ボタンから離れたら非表示
        tex.SetActive(false);
    }

    /// <summary>
    /// スイッチを押したときのアクション
    /// </summary>
    protected abstract void action_on();
    public virtual void action_off()
    {
        switch_on = false;
    }
}
