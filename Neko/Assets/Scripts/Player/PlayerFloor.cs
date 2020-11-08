using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : MonoBehaviour
{
    private PlayerController parent;
    private Rigidbody parent_rigit;

    [SerializeField] private EyeOpenChecker eyeOpenChecker = null;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerController>();
        parent_rigit = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // はしご判定
        switch(other.gameObject.tag)
        {
            case "LadderBottom":
                if (parent.State == PlayerState.Ladder)
                    parent.ChangeState(PlayerState.LadderBottom);
                else if (parent.State == PlayerState.Nuetral &&
                    Input.GetKey(KeyCode.UpArrow))
                {
                    StartCoroutine(parent.LadderStart(other.transform.position.x, PlayerState.LadderBottom));
                }
                break;
            case "LadderTop":
                if (parent.State == PlayerState.Ladder)
                    parent.ChangeState(PlayerState.LadderTop);
                else if (parent.State == PlayerState.Nuetral &&
                    Input.GetKey(KeyCode.DownArrow))
                {
                    StartCoroutine(parent.LadderStart(other.transform.position.x, PlayerState.LadderTop));
                }
                // はしごを登った後にジャンプするのを防ぐ
                parent_rigit.velocity = new Vector3(parent_rigit.velocity.x, 0, 0);
                break;
            case "Ladder":
                if (parent.State == PlayerState.LadderTop || parent.State == PlayerState.LadderBottom)
                    parent.ChangeState(PlayerState.Ladder);
                break;
            case "Floor":
                if (parent.State == PlayerState.Fall)
                    parent.Landing();
                else if((parent.State == PlayerState.LadderTop || parent.State == PlayerState.LadderBottom) &&
                    Input.GetAxis("Horizontal") != 0.0f)
                {
                    StartCoroutine(parent.LadderEnd());
                }
                break;
            case "Cliff":
                if (parent.State == PlayerState.Fall)
                    parent.Landing();
                else if (parent.State == PlayerState.Nuetral)
                    parent.Cliff();
                break;
        }


        // 目が閉じているときは判定なし
        if (eyeOpenChecker.KEEP_EYE_OPEN)
        {
            if (other.gameObject.tag == "Enemy")
            {
                parent.ChangeState(State.Dead);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
