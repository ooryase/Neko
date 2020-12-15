using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3_2 : SwitchObject
{
    private Animator drop;
    private GameObject wall;

    protected override void Start()
    {
        base.Start();

        drop = transform.GetChild(1).gameObject.GetComponent<Animator>();
        wall = transform.GetChild(2).gameObject;

        ZoomPos = new Vector3(transform.position.x, transform.position.y, -16.0f);
        AnimName = "push";
    }

    protected override void action_on()
    {
        base.action_on();

        drop.SetTrigger("Third");
        wall.SetActive(false);
    }

    public override void action_off()
    {
        base.action_off();

        drop.Rebind();
        wall.SetActive(true);
    }
}
