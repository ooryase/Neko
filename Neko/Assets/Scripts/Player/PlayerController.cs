using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    [SerializeField] private float speed = 3;

    private EyeOpenChecker eyeOpenChecker;

    public PlayerState State { get; private set; }

    public bool FollowFlag { get; private set; } // プレイヤー以外をフォローするときtrue
    public Vector3 FollowPos { get; private set; } // カメラでギミックをフォローする位置
    private float followTime; // フォローする時間
    /// <summary>
    /// 着地時に死亡判定が発生する落下速度
    /// </summary>
    private float fallDeadSpeed = -8.0f;
    /// <summary>
    /// 落下速度を保持するための変数
    /// (rigidbodyから取得だと生成順によっては着地時にvelocityが0になるため)
    /// </summary>
    private float fallSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        eyeOpenChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();
        FollowFlag = false;
        FollowPos = transform.position;

        State = PlayerState.Nuetral;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Hurt());
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif

        switch (State)
        {
            case PlayerState.Nuetral:
                float x = Input.GetAxis("Horizontal");
                animator.SetBool("walk", x != 0.0f);
                if(Time.timeScale != 0)
                {
                    if (x < 0.0f)
                        transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                    else if (x > 0.0f)
                        transform.rotation = Quaternion.Euler(0, 0.0f, 0);
                }

                rb.velocity = new Vector3(x * speed, rb.velocity.y, 0);
                //rigidbody.AddForce(0, -1.5f, 0);
                break;
            case PlayerState.Ladder:
                float y = Input.GetAxis("Vertical");
                animator.SetBool("walk", y != 0.0f);
                rb.velocity = new Vector3(0, y * speed, 0);
                break;
            case PlayerState.LadderTop:
                var down = Input.GetAxis("Vertical") == -1 ? -1.0f : 0.0f;
                animator.SetBool("walk", down != 0.0f);
                rb.velocity = new Vector3(0, down * speed, 0);
                break;
            case PlayerState.LadderBottom:
                var up = Input.GetAxis("Vertical") == 1 ? 1.0f : 0.0f;
                animator.SetBool("walk", up != 0.0f);
                rb.velocity = new Vector3(0, up * speed, 0);
                break;
            case PlayerState.Cliff:
                if (transform.rotation.eulerAngles.y == 0.0f)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") == -1)
                    {
                        StartCoroutine(ReturnFromCliff(-1.0f));
                    }
                }
                else if (transform.rotation.eulerAngles.y == 180.0f)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Horizontal") == 1)
                    {
                        StartCoroutine(ReturnFromCliff(1.0f));
                    }
                }
                break;
            case PlayerState.Fall:
                fallSpeed = rb.velocity.y;
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
        for (int i = 0; i < 10; i++)
        {
            transform.position += new Vector3(lenght * 0.1f, 0, 0);

            yield return new WaitForSeconds(0.1f);
        }
        ChangeState(state);
        rb.useGravity = false;
        capsuleCollider.isTrigger = true;
    }
    public IEnumerator LadderEnd()
    {
        ChangeState(PlayerState.Transition);
        rb.velocity = Vector3.zero;
        animator.SetTrigger("landWalk");
        yield return new WaitForSeconds(1.0f);
        ChangeState(PlayerState.Nuetral);
        rb.useGravity = true;
        capsuleCollider.isTrigger = false;
    }
    public IEnumerator ReturnFromCliff(float direction)
    {
        ChangeState(PlayerState.Transition);
        animator.SetTrigger("cliff");
        rb.velocity = new Vector3(direction * speed, rb.velocity.y, 0);
        transform.rotation = Quaternion.Euler(0, 90.0f - 90.0f * direction, 0);

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("fall", false);
        animator.ResetTrigger("cliff");
        ChangeState(PlayerState.Nuetral);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Switch")
        {
            if (State == PlayerState.Nuetral && Input.GetKeyDown(KeyCode.Space) ||
                State == PlayerState.Nuetral && Input.GetButtonDown("Action1"))
            {
                var sw = other.gameObject.GetComponent<SwitchObject>();
                if (sw.StartAnim == false && sw.Type == SwitchType.Push)
                {
                    // スイッチ押した後に歩き続けるのを防ぐ
                    animator.SetBool("walk", false);

                    StartCoroutine(OnSwitch(sw.AnimName));
                }
            }
        }

        // 目が閉じているときは判定なし
        if (eyeOpenChecker.KEEP_EYE_OPEN)
        {
            if (other.gameObject.tag == "Enemy" &&
                State != PlayerState.Hurt &&
                State != PlayerState.Dead)
            {
                StartCoroutine(Hurt());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Switch")
        {
            var sw = other.gameObject.GetComponent<SwitchObject>();
            FollowPos = sw.ZoomPos;
            followTime = sw.ZoomTime; // 拘束時間になった

            FollowFlag = true;

            if (sw.StartAnim == false && sw.Type == SwitchType.Area)
            {
                if(sw.AnimName != "None")
                {
                    // スイッチ押した後に歩き続けるのを防ぐ
                    animator.SetBool("walk", false);

                    StartCoroutine(OnSwitch(sw.AnimName));
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Switch")
        {
            FollowFlag = false;
        }
    }

    private IEnumerator OnSwitch(string name)
    {
        //FollowFlag = true;
        animator.SetTrigger(name);
        ChangeState(PlayerState.Transition);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(followTime); // 拘束

        //FollowFlag = false;
        if (State == PlayerState.Transition)
            ChangeState(PlayerState.Nuetral);

        animator.ResetTrigger(name);
    }

    public void FallFunc()
    {
        var direction = (transform.rotation.eulerAngles.y == 0.0f) ? 1.0f : -1.0f;
        rb.velocity = new Vector3(0.75f * speed * direction, 0.5f * speed, 0);
        ChangeState(PlayerState.Fall);
        animator.SetBool("fall", true);
        Debug.Log("Fall");
    }


    public void Cliff()
    {
        animator.SetTrigger("cliff");
        ChangeState(PlayerState.Cliff);
        rb.velocity = Vector3.zero;
        //StartCoroutine(CliffStart());
    }

    public void Landing()
    {
        animator.SetBool("fall", false);
        //目を開けて
        //落下速度が死亡速度を超える場合、死ぬ
        if (eyeOpenChecker.KEEP_EYE_OPEN &&
            fallSpeed < fallDeadSpeed)
            StartCoroutine(Hurt());
        //そうでなければ着地
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
        ChangeState(PlayerState.Hurt);

        yield return new WaitForSeconds(0.1f);
        ChangeState(PlayerState.Dead);
    }

    public void ChangeState(PlayerState newState)
    {
        State = newState;
    }

    /// <summary>
    /// リスタート時に呼ぶ処理
    /// </summary>
    public void ResetStatus()
    {
        ChangeState(PlayerState.Nuetral);
        animator.Play("Wait");
        animator.SetBool("fall", false);
        animator.ResetTrigger("landing");
        animator.ResetTrigger("die");
    }
}
