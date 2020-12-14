using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スイッチの種類
/// 
/// Push 押す必要あるスイッチ /
/// Area 入ったら即アクション /
/// ZoomOnly ズームするだけ / 
/// Other それ以外
/// </summary>
public enum SwitchType
{
    Push,
    Area,
    ZoomOnly,
    Other
}
public abstract class SwitchObject : MonoBehaviour
{
    private GameObject tex; // ボタンの上のビックリマーク
    private Animator anim_tex;
    private bool switch_on;

    public SwitchType Type { get; protected set; }
    //public bool PuchRequired { get; protected set; } // スイッチを押す必要があるのか
    public bool StartAnim { get; private set; } // Actionがスタートしてるか
    public string AnimName { get; protected set; }

    public Vector3 ZoomPos { get; protected set; }
    public float ZoomTime { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tex = transform.GetChild(0).gameObject;
        anim_tex = tex.GetComponent<Animator>();
        tex.SetActive(false);
        switch_on = false;

        ZoomTime = 1.83f;
        AnimName = "switch";

        Type = SwitchType.Push;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        // 押す前なら
        if (switch_on == false)
        {
            switch (Type)
            {
                case SwitchType.Push:
                    tex.SetActive(true);
                    anim_tex.SetTrigger("popUp");
                    break;
                case SwitchType.Area:
                    Invoke("action_on", 0.7f);
                    switch_on = true;
                    break;
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        if (switch_on == false && Type == SwitchType.Push)
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
        if (Type == SwitchType.Push)
        {
            anim_tex.SetTrigger("popDown");
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
        //anim_tex.Rebind();
    }
}
