using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SwitchObject : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private Animator animator;
    private bool switch_on;
    public bool PuchRequired { get; protected set; } // スイッチを押す必要があるのか
    public bool StartAnim { get; private set; } // Actionがスタートしてるか
    public string AnimName { get; protected set; }

    public Vector3 ZoomPos { get; protected set; }
    public float ZoomTime { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tex = transform.GetChild(0).gameObject;
        animator = tex.GetComponent<Animator>();
        tex.SetActive(false);
        switch_on = false;

        PuchRequired = true;
        ZoomTime = 1.83f;
        AnimName = "switch";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // 押す前なら表示する
        if (switch_on == false && PuchRequired)
        {
            tex.SetActive(true);
            animator.SetTrigger("popUp");
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        if (switch_on == false && PuchRequired)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Action1"))
            {
                Invoke("action_on", 0.7f);

                switch_on = true;
                tex.SetActive(false);
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // ボタンから離れたら非表示
        //tex.SetActive(false);
        if (PuchRequired)
        {
            animator.SetTrigger("popDown");
        }
    }

    /// <summary>
    /// スイッチを押したときのアクション
    /// </summary>
    protected virtual void action_on()
    {
        StartAnim = true;
    }
    public virtual void action_off()
    {
        StartAnim = false;
        switch_on = false;
        tex.SetActive(false);
        //animator.Rebind();
    }
}
