using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottom : MonoBehaviour
{
    private PlayerController parent;


    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Floor":
                if (parent.State == PlayerState.Fall)
                    parent.Landing();
                break;
            case "Cliff":
                if (parent.State == PlayerState.Fall)
                    parent.Landing();
                else if (parent.State == PlayerState.Nuetral)
                    parent.Cliff();
                break;
        }
    }

        private void OnTriggerStay(Collider other)
    {
        // はしご判定
        switch(other.gameObject.tag)
        {
            case "LadderBottom":
                Debug.Log("Bottom");

                if (parent.State == PlayerState.Ladder)
                    parent.ChangeState(PlayerState.LadderBottom);
                else if (parent.State == PlayerState.Nuetral && Input.GetKey(KeyCode.UpArrow) ||
                    parent.State == PlayerState.Nuetral && Input.GetAxis("Vertical") > 0)
                {
                    StartCoroutine(parent.LadderStart(other.transform.position.x, PlayerState.LadderBottom));
                }
                break;
            case "LadderTop":
                Debug.Log("Top");

                if (parent.State == PlayerState.Ladder)
                    parent.ChangeState(PlayerState.LadderTop);
                else if (parent.State == PlayerState.Nuetral && Input.GetKey(KeyCode.DownArrow) ||
                    parent.State == PlayerState.Nuetral && Input.GetAxis("Vertical") < 0)
                {
                    StartCoroutine(parent.LadderStart(other.transform.position.x, PlayerState.LadderTop));
                }
                break;
            case "Ladder":
                if (parent.State == PlayerState.LadderTop || parent.State == PlayerState.LadderBottom)
                    parent.ChangeState(PlayerState.Ladder);
                break;
            case "Floor":
                Debug.Log("Floor");

                if ((parent.State == PlayerState.LadderTop || parent.State == PlayerState.LadderBottom) &&
                    Input.GetAxis("Horizontal") != 0.0f)
                {
                    if(Input.GetAxis("Horizontal") > 0.8f || Input.GetAxis("Horizontal") < -0.8f)
                    {
                        StartCoroutine(parent.LadderEnd());
                    }
                }
                break;
        }
    }
}
