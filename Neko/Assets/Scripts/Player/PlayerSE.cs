using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip[] footsteps = null;
    [SerializeField] private AudioClip ladder = null;
    [SerializeField] private AudioClip beat = null;
    [SerializeField] private AudioClip dead = null;
    [SerializeField] private AudioClip switchOn = null;

    float initVolume;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        initVolume = source.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootsteps()
    {
        source.PlayOneShot(footsteps[(int)Random.Range(0, footsteps.Length)]);
    }

    public void PlayLadder()
    {
        source.PlayOneShot(ladder);
    }

    public void PlayBeat()
    {
        source.volume = 1;
        source.PlayOneShot(beat);
        source.volume = initVolume;
    }

    public void PlayDead()
    {
        source.PlayOneShot(dead);
    }

    public void PlaySwitch()
    {
        source.PlayOneShot(switchOn);
    }
}
