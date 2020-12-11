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
                break;
            }
        }


        private void OnTriggerStay(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Floor":
                //はしごに乗ってる状態で床と当たった場合、降りる
                if ((parent.State == PlayerState.LadderTop || parent.State == PlayerState.LadderBottom || parent.State == PlayerState.Ladder) &&
                   Input.GetAxis("Horizontal") != 0.0f)
                {
                    //if(Input.GetAxis("Horizontal") > 0.8f || Input.GetAxis("Horizontal") < -0.8f)
                    StartCoroutine(parent.LadderEnd());
                }
                break;
            case "Cliff":
                if (parent.State == PlayerState.Nuetral)
                    parent.Cliff();
                break;


            // はしご判定
            case "LadderBottom":

                if (parent.State == PlayerState.Ladder)
                    parent.ChangeState(PlayerState.LadderBottom);
                else if (parent.State == PlayerState.Nuetral && 
                    (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0))
                {
                    StartCoroutine(parent.LadderStart(other.transform.position.x, PlayerState.LadderBottom));
                }
                break;
            case "LadderTop":

                if (parent.State == PlayerState.Ladder)
                    parent.ChangeState(PlayerState.LadderTop);
                else if (parent.State == PlayerState.Nuetral && 
                    (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0))
                {
                    StartCoroutine(parent.LadderStart(other.transform.position.x, PlayerState.LadderTop));
                }
                break;
            case "Ladder":
                if (parent.State == PlayerState.LadderTop || parent.State == PlayerState.LadderBottom)
                    parent.ChangeState(PlayerState.Ladder);
                break;
        }
    }
}
