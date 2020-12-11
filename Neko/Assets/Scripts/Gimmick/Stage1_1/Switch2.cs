using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2 : SwitchObject
{
    private Animator anim;
    private GameObject block;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        anim = transform.GetChild(1).gameObject.GetComponent<Animator>();
        block = transform.GetChild(2).gameObject;

        ZoomPos = new Vector3(-2.0f, -2.0f, -20.0f);
        ZoomTime = 5.5f;
    }

    protected override void action_on()
    {
        base.action_on();

        anim.SetTrigger("RotateCore");
        block.SetActive(true);
    }

    public override void action_off()
    {
        base.action_off();

        // アニメーションをすべて戻す？
        anim.Rebind();
    }
}
