using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : SwitchObject
{
    private Animator anim;
    private GameObject wall;

    private AudioSource source;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = transform.GetChild(1).gameObject.GetComponent<Animator>();
        wall = transform.GetChild(2).gameObject;
        source = GetComponent<AudioSource>();

        ZoomPos = new Vector3(wall.transform.position.x - 1.0f, wall.transform.position.y + 0.5f, -9);
        AnimName = "push";
    }

    protected override void action_on()
    {
        base.action_on();

        wall.SetActive(false);

        anim.SetTrigger("Open");

        playDoorOpen();
    }

    private void playDoorOpen()
    {
        source.PlayOneShot(source.clip);
    }
}
