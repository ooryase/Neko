using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2 : SwitchObject
{
    private GameObject wall1;
    private GameObject wall2;
    // ブロックが落ちるアニメ
    private Animator block_anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        wall1 = transform.GetChild(1).gameObject;
        wall2 = transform.GetChild(2).gameObject;
        block_anim = transform.GetChild(3).gameObject.GetComponent<Animator>();

        ZoomPos = new Vector3(wall1.transform.position.x, wall1.transform.position.y, -7.0f);
    }

    protected override void action_on()
    {
        wall1.SetActive(false);
        wall2.SetActive(true);

        block_anim.SetTrigger("Block");
    }

    public override void action_off()
    {
        base.action_off();

        wall1.SetActive(true);
        wall2.SetActive(false);

        // アニメーションをすべて戻す
        block_anim.Rebind();
    }
}
