using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderSE : MonoBehaviour
{
    private AudioSource source;
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

    public void PlayLadder()
    {
        source.Stop();
        source.PlayOneShot(ladder);
    }
}
