using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoPredetor : MonoBehaviour
{
    private EyeOpenChecker eyeOpenChecker;
    private Animator animator;
    private Transform playerTransform;
    private PlayerController playerController;
    private Rigidbody rb;
    private CapsuleCollider attackCollider;

    private Vector3 resetPos;

    [SerializeField] private float distance = 1.5f;
    [SerializeField] private float speed = 0.9f;

    public bool DeadFlag { get; private set; }


    enum PredetorState
    {
        Walk,
        Prey,
        WarCry,
        Hurt,
        Die
    }
    private PredetorState state;

    // Start is called before the first frame update
    void Start()
    {
        state = PredetorState.Walk;
        eyeOpenChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        attackCollider = transform.Find("Collider").gameObject.GetComponent<CapsuleCollider>();
        attackCollider.enabled = false;
        resetPos = gameObject.transform.position;
        DeadFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PredetorState.Walk:
                var length = playerTransform.position.x - gameObject.transform.position.x;
                var walk = Mathf.Abs(length) > distance;
                animator.SetBool("walk", walk);
                animator.SetBool("prey", !walk && eyeOpenChecker.KEEP_EYE_OPEN);
                if (walk)
                {
                    var speedX = speed * ((length > 0) ? 1.0f : -1.0f);
                    rb.velocity = new Vector3(speedX, 0, 0);
                }
                else
                    rb.velocity = new Vector3(0, 0, 0);

                var direction = (length > 0.0f) ? 0.0f : 180.0f;
                gameObject.transform.rotation = Quaternion.Euler(0.0f, direction, 0.0f);

                break;
            case PredetorState.Prey:
                break;
            case PredetorState.Hurt:
                break;
            case PredetorState.Die:
                break;
        }

        if(playerController.State == PlayerState.Hurt && IsInvoking() == false)
        {
            Invoke("ResetStatus", 1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyKiller")
        {
            if(state != PredetorState.Hurt)
                animator.SetTrigger("hurt");
        }
        else if(other.gameObject.tag == "Fire")
        {
            if (state != PredetorState.Hurt)
                animator.SetTrigger("hurt");
            animator.SetBool("die", true);
            DeadFlag = true;
        }
    }

    public void AttackStart()
    {
        state = PredetorState.Prey;
    }


    public void Attack()
    {
        attackCollider.enabled = true;
    }

    public void AttackEnd()
    {
        state = PredetorState.Walk;
        attackCollider.enabled = false;
    }

    public void Hurt()
    {
        state = PredetorState.Hurt;
    }

    public void Recover()
    {
        state = PredetorState.Walk;
    }

    public void SetResetPos(Vector3 pos)
    {
        resetPos = pos;
        transform.position = pos;
        //speed += 0.1f;
    }

    public void WarCry()
    {
        animator.SetTrigger("warCry");
        state = PredetorState.WarCry;
    }

    public void ResetStatus()
    {
        transform.position = resetPos;
        animator.Play("Wait");
        animator.SetBool("die", false);
        Recover();
    }
}
