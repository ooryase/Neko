using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch1 :SwitchObject
{
    private GameObject ladder2; // ボタンで出現するはしご
    private Animator drop;

    protected override void Start()
    {
        base.Start();

        ladder2 = transform.GetChild(1).gameObject;
        drop = transform.GetChild(3).gameObject.GetComponent<Animator>();
    }

    protected override void action_on()
    {
        ladder2.SetActive(true);

        drop.SetTrigger("Drop");
    }

    public override void action_off()
    {
        base.action_off();

        ladder2.SetActive(false);
        drop.Rebind();
    }
}