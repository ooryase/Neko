using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody rigit;
    [SerializeField] private float followSpeed = 5;
    private float limitVel = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rigit = GetComponent<Rigidbody>();
        Follow();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos_player = player.transform.position;
        if (Mathf.Abs(transform.position.x - pos_player.x) > 1)
        {
            if (pos_player.x - transform.position.x > 0)
            {
                rigit.AddForce(new Vector3(followSpeed, 0, 0));
            }
            else
            {
                rigit.AddForce(new Vector3(-followSpeed, 0, 0));
            }
        }
        else if (Mathf.Abs(rigit.velocity.x) > 0.1f)
        {
            rigit.velocity = new Vector3(rigit.velocity.x * 0.85f, rigit.velocity.y, 0);
        }
        else
        {
            rigit.velocity = new Vector3(0, rigit.velocity.y, 0);
        }
        //transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        if (rigit.velocity.x > limitVel) rigit.velocity = new Vector3(limitVel, rigit.velocity.y, 0);
        if (rigit.velocity.x < -limitVel) rigit.velocity = new Vector3(-limitVel, rigit.velocity.y, 0);

        //Debug.Log(rigit.velocity.x);

        transform.position = new Vector3(transform.position.x, pos_player.y, transform.position.z);
    }

    public void Follow()
    {
        Vector3 pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void VelocityReset()
    {
        rigit.velocity = Vector3.zero;
    }
}
