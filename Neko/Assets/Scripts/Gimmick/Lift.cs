using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private AudioSource source;
    //[SerializeField] private AudioClip end = null;
    [SerializeField] private AudioClip move = null;
    [SerializeField] private AudioClip drop = null;

    private GameObject player;
    private bool bind = false;
    private PlayerFollow playerFollow;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerFollow>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (bind)
        {
            player.transform.position = new Vector3(player.transform.position.x, transform.position.y - 0.31f, player.transform.position.z);
            player.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void PlayerBind()
    {
        bind = true;
        playerFollow.SetFollowSpeed(0.3f);
    }

    public void PlayerFree()
    {
        bind = false;
        player.GetComponent<Rigidbody>().useGravity = true;

        playerFollow.Shake(1.2f);
        playerFollow.SetFollowSpeed(0.7f);

        if (source.isPlaying) source.Stop();
        source.PlayOneShot(drop);
    }

    public void PlayMove()
    {
        source.PlayOneShot(move);
    }
}
