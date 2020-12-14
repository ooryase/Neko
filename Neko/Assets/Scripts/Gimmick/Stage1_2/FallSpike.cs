using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpike : SwitchObject
{
    private Vector3 initPos;

    protected override void Start()
    {
        initPos = transform.position;

        Type = SwitchType.Other;
    }

    protected override void action_on()
    {
    }

    public override void action_off()
    {
        transform.position = initPos;
    }
}
