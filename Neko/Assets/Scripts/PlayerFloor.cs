using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : MonoBehaviour
{
    private PlayerControll parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerControll>();
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
            if (Input.GetAxis("Vertical") != 0)
            {
                parent.ChangeState(State.Ladder);
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                parent.ChangeState(State.Nuetral);
            }
        }
        if (other.gameObject.tag == "LadderTop")
        {
            parent.ChangeState(State.Nuetral);
        }
        if (other.gameObject.tag == "Ladder")
        {
            parent.ChangeState(State.Ladder);
        }
    }
}
