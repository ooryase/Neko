using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Nuetral,
    Transition,
    LadderTop,
    Ladder,
    LadderBottom,
    Cliff,
    Fall,
    Hurt,
    Dead,
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider collider;
    private Animator animator;
    [SerializeField] private float speed = 3;

    private PlayerState state;
    public PlayerState State { get => state; private set => state = value; }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();

        State = PlayerState.Nuetral;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Hurt());
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
                rb.velocity = new Vector3(x * speed, rb.velocity.y, 0);
                //rigidbody.AddForce(0, -1.5f, 0);
                break;
            case PlayerState.Ladder:
                float y = Input.GetAxis("Vertical");
                animator.SetBool("walk", y != 0.0f);
                rb.velocity = new Vector3(0, y * speed, 0);
                break;
            case PlayerState.LadderTop:
                var down = (Input.GetKey(KeyCode.DownArrow)) ? -1.0f : 0.0f;
                animator.SetBool("walk", down != 0.0f);
                rb.velocity = new Vector3(0, down * speed, 0);
                break;
            case PlayerState.LadderBottom:
                var up = (Input.GetKey(KeyCode.UpArrow)) ? 1.0f : 0.0f;
                animator.SetBool("walk", up != 0.0f);
                rb.velocity = new Vector3(0, up * speed, 0);
                break;
            case PlayerState.Cliff:
                if (transform.rotation.eulerAngles.y == 0.0f)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        StartCoroutine(ReturnFromCliff(-1.0f));
                    }
                    else if(Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        rb.velocity = new Vector3(2.0f * speed, 2.0f * speed, 0);
                        ChangeState(PlayerState.Fall);
                        animator.SetBool("fall", true);
                    }
                }
                else if (transform.rotation.eulerAngles.y == 180.0f)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        StartCoroutine(ReturnFromCliff(1.0f));
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        rb.velocity = new Vector3(-2.0f * speed, -2.0f * speed, 0);
                        ChangeState(PlayerState.Fall);
                        animator.SetBool("fall", true);
                    }
                }

                break;
            case PlayerState.Dead:
                //rigidbody.constraints = RigidbodyConstraints.FreezePosition;

                break;
        }
    }

    public IEnumerator LadderStart(float ladderPosX, PlayerState state)
    {
        ChangeState(PlayerState.Transition);
        rb.velocity = Vector3.zero;
        animator.SetTrigger("ladder");
        float lenght = ladderPosX - transform.position.x;
        for(int i = 0; i < 10; i++)
        {
            transform.position += new Vector3(lenght * 0.1f, 0, 0);

            yield return new WaitForSeconds(0.1f);
        }
        ChangeState(state);
        rb.useGravity = false;
        collider.isTrigger = true;
    }
    public IEnumerator LadderEnd()
    {
        ChangeState(PlayerState.Transition);
        rb.velocity = Vector3.zero;
        animator.SetTrigger("landWalk");
        yield return new WaitForSeconds(1.0f);
        ChangeState(PlayerState.Nuetral);
        rb.useGravity = true;
        collider.isTrigger = false;
    }
    public IEnumerator ReturnFromCliff(float direction)
    {
        ChangeState(PlayerState.Transition);
        animator.SetTrigger("cliff");
        rb.velocity = new Vector3(direction * speed, rb.velocity.y, 0);
        transform.rotation = Quaternion.Euler(0, 90.0f - 90.0f * direction, 0);

        yield return new WaitForSeconds(1.0f);
        ChangeState(PlayerState.Nuetral);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Switch") 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(OnSwitch());
            }
        }
        // 崩壊オブジェに触れたときの処理
        //else if()
    }

    private IEnumerator OnSwitch()
    {
        animator.SetTrigger("switch");
        ChangeState(PlayerState.Transition);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.83f);

        if(State == PlayerState.Transition)
            ChangeState(PlayerState.Nuetral);
    }

    public void Cliff()
    {
        animator.SetTrigger("cliff");
        ChangeState(PlayerState.Cliff);
        rb.velocity = Vector3.zero;
    }

    public void Landing()
    {
        Debug.Log(rb.velocity.y);
        animator.SetBool("fall", false);
        if (rb.velocity.y < -5.0f)
            StartCoroutine(Hurt());
        else
            StartCoroutine(LandSuccess());
    }

    private IEnumerator LandSuccess()
    {
        animator.SetTrigger("landing");
        ChangeState(PlayerState.Transition);

        yield return new WaitForSeconds(0.66f);
        ChangeState(PlayerState.Nuetral);

    }
    private IEnumerator Hurt()
    {
        animator.SetTrigger("die");
        ChangeState(PlayerState.Transition);

        yield return new WaitForSeconds(0.66f);
        ChangeState(PlayerState.Dead);
    }

    public void ChangeState(PlayerState newState)
    {
        State = newState;
    }
}
