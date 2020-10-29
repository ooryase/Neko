using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : MonoBehaviour
{
    private PlayerControll parent;
    private Rigidbody parent_rigit;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerControll>();
        parent_rigit = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // はしご判定
        if (other.gameObject.tag == "LadderBottom")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                parent.ChangeState(State.Ladder);
            }
            else parent.ChangeState(State.Nuetral);
        }
        if (other.gameObject.tag == "LadderTop")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                parent.ChangeState(State.Ladder);
            }
            else parent.ChangeState(State.Nuetral);

            // はしごを登った後にジャンプするのを防ぐ
            parent_rigit.velocity = new Vector3(parent_rigit.velocity.x, 0, 0);
        }
        if (other.gameObject.tag == "Ladder")
        {
            parent.ChangeState(State.Ladder);
        }
    }
}
