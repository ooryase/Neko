using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3_1 : SwitchObject
{
    private Animator drop;
    private GameObject wall;

    protected override void Start()
    {
        base.Start();

        drop = transform.GetChild(1).gameObject.GetComponent<Animator>();
        wall = transform.GetChild(2).gameObject;

        ZoomPos = new Vector3(transform.position.x - 1.4f, transform.position.y, -17.0f);
        AnimName = "push";
    }

    protected override void action_on()
    {
        base.action_on();

        drop.SetTrigger("First");
        wall.SetActive(false);
    }

    public override void action_off()
    {
        base.action_off();

        drop.Rebind();
        wall.SetActive(true);
    }
}
