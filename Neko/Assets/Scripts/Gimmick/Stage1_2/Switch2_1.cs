using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2_1 : SwitchObject
{
    private Animator drop;

    protected override void Start()
    {
        base.Start();

        var lift = transform.GetChild(1).gameObject;
        drop = lift.GetComponent<Animator>();

        ZoomPos = new Vector3(lift.transform.position.x, lift.transform.position.y, -12.0f);
    }

    protected override void action_on()
    {
        base.action_on();

        drop.SetTrigger("Drop");
    }

    public override void action_off()
    {
        base.action_off();

        drop.Rebind();
    }
}
