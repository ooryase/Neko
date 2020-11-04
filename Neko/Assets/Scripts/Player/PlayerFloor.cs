using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : MonoBehaviour
{
    private PlayerControll parent;
    private Rigidbody parent_rigit;

    [SerializeField] private EyeOpenChecker eyeOpenChecker = null;

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
            if (Input.GetAxis("Vertical") > 0)
            {
                parent.ChangeState(State.Ladder);
            }
            else parent.ChangeState(State.Nuetral);
        }
        if (other.gameObject.tag == "LadderTop")
        {
            if (Input.GetAxis("Vertical") < 0)
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
