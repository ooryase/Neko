using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : SwitchObject
{
    private Animator anim;
    private GameObject wall;

    protected override void Start()
    {
        base.Start();
        anim = transform.GetChild(1).gameObject.GetComponent<Animator>();
        wall = transform.GetChild(2).gameObject;

        Type = SwitchType.Push;
        AnimName = "push";
        ZoomPos = transform.position + new Vector3(0, 0, -11.0f);
    }

    protected override void action_on()
    {
        base.action_on();
        anim.SetTrigger("Break");
        wall.SetActive(false);
    }

    public override void action_off()
    {
        base.action_off();
        wall.SetActive(true);
        anim.Rebind();
    }
}
