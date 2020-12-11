using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3 : SwitchObject
{
    private Animator arm_anim;
    private GameObject coll;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Armのアニメーター取得
        arm_anim = transform.GetChild(1).gameObject.GetComponent<Animator>();
        coll = transform.GetChild(2).gameObject;

        ZoomPos = new Vector3(coll.transform.position.x, coll.transform.position.y, -9.0f);
    }

    protected override void action_on()
    {
        base.action_on();

        arm_anim.SetTrigger("Move"); // アニメ実行
        coll.SetActive(true);
    }

    public override void action_off()
    {
        base.action_off();

        coll.SetActive(false);

        // アニメーションをすべて戻す
        arm_anim.Rebind();
    }
}
