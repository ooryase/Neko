using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : SwitchObject
{
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = transform.GetChild(1).gameObject.GetComponent<Animator>();

        Type = SwitchType.Area;
        AnimName = "push";
        ZoomPos = transform.position + new Vector3(0, 0, -11.0f);
    }

    protected override void action_on()
    {
        base.action_on();
        anim.SetTrigger("Break");
    }

    public override void action_off()
    {
        base.action_off();
        anim.Rebind();
    }
}
