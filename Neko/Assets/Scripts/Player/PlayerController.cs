using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Nuetral,
    LadderTransition,
    LadderTop,
    Ladder,
    LadderBottom,
    Dead,
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private CapsuleCollider collider;
    private Animator animator;
    [SerializeField] private float speed = 3;

    private PlayerState state;
    public PlayerState State { get => state; private set => state = value; }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();

        State = PlayerState.Nuetral;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            State = PlayerState.Dead;
        }

        switch (State)
        {
            case PlayerState.Nuetral:
                float x = Input.GetAxis("Horizontal");
                animator.SetBool("walk", x != 0.0f);
                if(x < 0.0f)
                    transform.rotation = Quaternion.Euler(0,180.0f,0);
                else if(x > 0.0f)
                    transform.rotation = Quaternion.Euler(0, 0.0f, 0);
                rigidbody.velocity = new Vector3(x * speed, rigidbody.velocity.y, 0);
                //rigidbody.AddForce(0, -1.5f, 0);
                rigidbody.useGravity = true;

                collider.isTrigger = false;
                break;
            case PlayerState.Ladder:
                float y = Input.GetAxis("Vertical");
                animator.SetBool("walk", y != 0.0f);
                //transform.position = new Vector3(transform.position.x, transform.position.y + (y / 20.0f), transform.position.z);
                rigidbody.velocity = new Vector3(0, y * speed, 0);
                rigidbody.useGravity = false;
                collider.isTrigger = true;

                break;
            case PlayerState.LadderTop:
                var down = (Input.GetKey(KeyCode.DownArrow)) ? -1.0f : 0.0f;
                animator.SetBool("walk", down != 0.0f);
                //transform.position = new Vector3(transform.position.x, transform.position.y + (y / 20.0f), transform.position.z);
                rigidbody.velocity = new Vector3(0, down * speed, 0);
                rigidbody.useGravity = false;
                collider.isTrigger = true;

                break;
            case PlayerState.LadderBottom:
                var up = (Input.GetKey(KeyCode.UpArrow)) ? 1.0f : 0.0f;
                animator.SetBool("walk", up != 0.0f);
                //transform.position = new Vector3(transform.position.x, transform.position.y + (y / 20.0f), transform.position.z);
                rigidbody.velocity = new Vector3(0, up * speed, 0);
                rigidbody.useGravity = false;
                collider.isTrigger = true;

                break;



            case PlayerState.Dead:
                //rigidbody.constraints = RigidbodyConstraints.FreezePosition;

                break;
        }
    }

    public IEnumerator LadderStart(float ladderPosX, PlayerState state)
    {
        ChangeState(PlayerState.LadderTransition);
        animator.SetTrigger("ladder");
        float lenght = ladderPosX - transform.position.x;
        for(int i = 0; i < 10; i++)
        {
            transform.position += new Vector3(lenght * 0.1f, 0, 0);

            yield return new WaitForSeconds(0.1f);
        }
        ChangeState(state);
    }
    public IEnumerator LadderEnd()
    {
        ChangeState(PlayerState.LadderTransition);
        animator.SetTrigger("landWalk");
        yield return new WaitForSeconds(1.0f);
        ChangeState(PlayerState.Nuetral);
    }

    public void ChangeState(PlayerState newState)
    {
        State = newState;
    }
}
