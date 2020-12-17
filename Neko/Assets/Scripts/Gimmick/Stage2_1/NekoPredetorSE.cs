using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoPredetorSE : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] walk = null;
    [SerializeField] private AudioClip roar = null;
    [SerializeField] private AudioClip cry = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalk()
    {
        audioSource.pitch = 0.56f;
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(walk[(int)Random.Range(0, walk.Length)]);
    }

    public void PlayRoar()
    {
        audioSource.pitch = 1.0f;
        audioSource.volume = 1.0f;
        audioSource.PlayOneShot(roar);
    }

    public void PlayCry()
    {
        audioSource.pitch = 1.0f;
        audioSource.volume = 0.8f;
        audioSource.PlayOneShot(cry);
    }
}
