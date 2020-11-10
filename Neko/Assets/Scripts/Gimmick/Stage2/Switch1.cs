using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch1 :SwitchObject
{
    [SerializeField] private GameObject ladder2 = null; // ボタンで出現するはしご

    protected override void action_on()
    {
        ladder2.SetActive(true);
    }

    public override void action_off()
    {
        base.action_off();

        ladder2.SetActive(false);
    }
}