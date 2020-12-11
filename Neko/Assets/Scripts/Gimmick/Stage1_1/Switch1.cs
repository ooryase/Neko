using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch1 :SwitchObject
{
    private GameObject ladderCollider; // ボタンで出現するはしご
    private Animator drop;

    protected override void Start()
    {
        base.Start();

        ladderCollider = transform.GetChild(1).transform.GetChild(0).gameObject;
        drop = transform.GetChild(1).transform.GetChild(2).gameObject.GetComponent<Animator>();

        ladderCollider.SetActive(false);
        ZoomPos = new Vector3(ladderCollider.transform.position.x, ladderCollider.transform.position.y, -10.0f);
    }

    protected override void action_on()
    {
        base.action_on();

        ladderCollider.SetActive(true);

        drop.SetTrigger("Drop");
    }

    public override void action_off()
    {
        base.action_off();

        ladderCollider.SetActive(false);
        drop.Rebind();
    }
}