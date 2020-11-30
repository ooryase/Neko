using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch4 : SwitchObject
{
    private Animator anim;
    private GameObject colliders;
    private GameObject wall;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        anim = transform.GetChild(1).gameObject.GetComponent<Animator>();
        colliders = transform.GetChild(2).gameObject;
        wall = transform.GetChild(3).gameObject;
        colliders.SetActive(false);

        ZoomPos = new Vector3(2.0f, -2.0f, -18.0f);
        ZoomTime = 4.0f;
    }

    protected override void action_on()
    {
        anim.SetTrigger("RotateCore2");
        colliders.SetActive(true);
        wall.SetActive(false);
    }

    public override void action_off()
    {
        base.action_off();

        colliders.SetActive(false);
        wall.SetActive(true);
        // アニメーションをすべて戻す？
        anim.Rebind();
    }
}
