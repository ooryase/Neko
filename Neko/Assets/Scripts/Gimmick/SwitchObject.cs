using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchObject : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private Animator animator;
    private bool switch_on;

    public Vector3 ZoomPos { get; protected set; }
    public float ZoomTime { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tex = transform.GetChild(0).gameObject;
        animator = tex.GetComponent<Animator>();
        tex.SetActive(false);
        switch_on = false;
        ZoomTime = 1.83f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // 押す前なら表示する
        if (switch_on == false)
        {
            tex.SetActive(true);
            animator.SetTrigger("popUp");
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        if (switch_on == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Action1"))
            {
                Invoke("action_on", 1.0f);

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
        animator.SetTrigger("popDown");
    }

    /// <summary>
    /// スイッチを押したときのアクション
    /// </summary>
    protected abstract void action_on();
    public virtual void action_off()
    {
        switch_on = false;
        tex.SetActive(false);
        //animator.Rebind();
    }
}
