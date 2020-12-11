using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    private PlayerController playerController;
    private float startPosZ;

    private Rigidbody rigit;

    private Vector3 tmp_vel = Vector3.zero;
    private Vector3 shakePos = Vector3.zero;
    // カメラが若干プレイヤーの上に行くように
    private readonly Vector3 ofset = new Vector3(0, -0.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rigit = GetComponent<Rigidbody>();
        startPosZ = transform.position.z;
        playerController = player.GetComponent<PlayerController>();
        Follow();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos_player = new Vector3(player.transform.position.x, player.transform.position.y, startPosZ);

        // FollowFlagがtrueならFollowPosをフォローする
        if (playerController.FollowFlag)
        {
            //transform.position = Vector3.Lerp(transform.position, playerController.FollowPos, Time.deltaTime * 2.0f);
            Vector3.SmoothDamp(transform.position, playerController.FollowPos, ref tmp_vel, 0.7f);
            rigit.velocity = tmp_vel;
        }
        else
        {
            Vector3.SmoothDamp(transform.position + ofset, pos_player, ref tmp_vel, 0.7f);
            rigit.velocity = tmp_vel;
        }

        // 画面揺れ
        if (Mathf.Abs(shakePos.y) > 0.01f)
        {
            shakePos.y *= -0.8f;
        }
        else shakePos.y = 0;
        transform.position = transform.position + shakePos;
        //transform.position = new Vector3(pos_player.x, pos_player.y, transform.position.z);
    }

    public void Follow()
    {
        Vector3 pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z) + ofset;
    }

    public void Shake(float power = 0.1f)
    {
        shakePos.y = power;
    }
}
