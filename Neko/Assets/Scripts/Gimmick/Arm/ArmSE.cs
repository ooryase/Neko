using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSE : MonoBehaviour
{
    private AudioSource source;
    //[SerializeField] private AudioClip start = null;
    [SerializeField] private AudioClip end = null;
    [SerializeField] private AudioClip move = null;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStart()
    {
        //source.PlayOneShot(start);
    }

    public void PlayEnd()
    {
        source.Stop();
        source.PlayOneShot(end);
    }

    public void PlayMove()
    {
        source.PlayOneShot(move);
    }
}
