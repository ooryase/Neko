using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManagerBoss : MonoBehaviour
{
    [SerializeField] private AudioClip intro = null;
    //[SerializeField] private AudioClip loop = null;

    private AudioSource source;
    private PlayerController playerController;

    private enum BGMState
    {
        None,
        Intro,
        Loop
    }
    private BGMState state;

    // Start is called before the first frame update
    void Start()
    {
        source = transform.GetChild(0).GetComponent<AudioSource>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        state = BGMState.None;
    }

    // Update is called once per frame
    void Update()
    {
        // イントロが終わったらメインループに
        if(source.isPlaying == false && state == BGMState.Intro)
        {
            state = BGMState.Loop;
            source.Play();
        }

        // 曲が流れててプレイヤーが死んだらリセット
        if(state != BGMState.None && playerController.State == PlayerState.Dead)
        {
            BGMStop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && state == BGMState.None)
        {
            BGMStart();
        }
    }

    public void BGMStart()
    {
        source.PlayOneShot(intro);
        state = BGMState.Intro;
    }

    public void BGMStop()
    {
        source.Stop();
        state = BGMState.None;
    }
}
