using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip footsteps = null;
    [SerializeField] private AudioClip ladder = null;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootsteps()
    {
        source.PlayOneShot(footsteps);
    }

    public void PlayLadder()
    {
        source.PlayOneShot(ladder);
    }
}
