using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DengerBlock : SwitchObject
{
    [SerializeField] private UncertainObject uncertain = null;
    private Animator anim;
    private bool flag = false;

    protected override void Start()
    {
        anim = GetComponent<Animator>();

        Type = SwitchType.Other;
    }

    void Update()
    {
        if (uncertain.IsVanish && flag == false)
        {
            anim.SetTrigger("Drop");

            flag = true;
        }
    }


    protected override void action_on()
    {
    }

    public override void action_off()
    {
        flag = false;
        anim.Rebind();
    }
}
