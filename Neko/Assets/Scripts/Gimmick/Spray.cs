using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 0.0f;

    Collider sprayCollider = null;

    ParticleSystem sprayParticleSystem = null;

    AudioSource audioSource = null;
    [SerializeField] float maxVolume = 0.39f;

    // Start is called before the first frame update
    void Start()
    {
        sprayCollider = GetComponent<Collider>();
        sprayParticleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.volume = 0;

        //Debug.Log(sprayParticleSystem);

        StartCoroutine(SprayCoroutine());
    }

    void Update()
    {
        if (sprayCollider == null)
            return;

        if (sprayCollider.enabled)
        {
            if(audioSource != null)
                Mathf.Clamp(audioSource.volume += 0.07f, 0, maxVolume);
        }
        else
        {
            if (audioSource != null)
                Mathf.Clamp(audioSource.volume -= 0.07f, 0, maxVolume);
        }
    }

    IEnumerator SprayCoroutine()
    {
        yield return new WaitForSeconds(delayTime);

        while(true)
        {
            if (sprayCollider != null)
                sprayCollider.enabled = true;

            if (sprayParticleSystem != null)
            {
                sprayParticleSystem.Play();
            }

            yield return new WaitForSeconds(2.0f);

            if (sprayCollider != null)
                sprayCollider.enabled = false;

            yield return new WaitForSeconds(3.0f);
        }
    }
}
