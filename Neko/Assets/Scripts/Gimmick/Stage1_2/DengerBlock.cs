using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DengerBlock : SwitchObject
{
    [SerializeField] private UncertainObject uncertain = null;
    private GameObject child1;
    private GameObject child2;
    private Animator anim;
    private bool flag = false;

    protected override void Start()
    {
        child1 = transform.GetChild(0).gameObject;
        child2 = transform.GetChild(1).gameObject;

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

        child1.gameObject.tag = "EnemyKiller";
        child2.gameObject.tag = "Enemy";
    }

    public void TagChange()
    {
        child1.gameObject.tag = "Untagged";
        child2.gameObject.tag = "Untagged";
    }
}
