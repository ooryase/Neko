using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoZombie : MonoBehaviour
{
    private EyeOpenChecker eyeOpenChecker;
    private Renderer objectRenderer;
    private Animator animator;
    private Transform playerTransform;
    private PlayerController playerController;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private Vector3 startPos;

    [SerializeField] private float distance = 0.3f;

    public enum ZombieState
    {
        Freeze,
        Awake,
    }
    public ZombieState State { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        State = ZombieState.Freeze;
        eyeOpenChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();
        objectRenderer = transform.Find("Drawables").gameObject.transform.Find("ArtMesh6").gameObject.GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("eyeOpen", !(!eyeOpenChecker.KEEP_EYE_OPEN && objectRenderer.isVisible));

        if(State == ZombieState.Awake)
        {
            var length = playerTransform.position.x - gameObject.transform.position.x;
            var walk = Mathf.Abs(length) > distance;
            animator.SetBool("walk", walk);
            if (walk)
            {
                var speedX = (length > 0) ? 0.5f : -0.5f;
                rb.velocity = new Vector3(speedX, 0, 0);
            }
            else
                rb.velocity = new Vector3(0, 0, 0);

            var direction = (length > 0.0f) ? 0.0f : 180.0f;
            gameObject.transform.rotation = Quaternion.Euler(0.0f, direction, 0.0f);
        }

        if (playerController.State == PlayerState.Hurt && IsInvoking() == false)
        {
            Invoke("ResetStatus", 1.2f);
        }
    }

    public void SetAwake()
    {
        State = ZombieState.Awake;
        capsuleCollider.enabled = true;
    }

    public void SetFreeze()
    {
        gameObject.tag = "Untagged";
        rb.velocity = new Vector3(0, 0, 0);
        State = ZombieState.Freeze;
        capsuleCollider.enabled = false;
    }

    public void SetEnemyTag()
    {
        gameObject.tag = "Enemy";
    }

    public void ResetStatus()
    {
        gameObject.transform.position = startPos;
        animator.Play("Freeze");
        animator.SetBool("walk", false);
    }
}
