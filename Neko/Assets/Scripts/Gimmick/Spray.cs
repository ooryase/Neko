using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 0.0f;

    Collider sprayCollider = null;

    ParticleSystem sprayParticleSystem = null;

    AudioSource audio = null;

    // Start is called before the first frame update
    void Start()
    {
        sprayCollider = GetComponent<Collider>();
        sprayParticleSystem = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();

        Debug.Log(sprayParticleSystem);

        StartCoroutine(SprayCoroutine());
    }


    IEnumerator SprayCoroutine()
    {
        yield return new WaitForSeconds(delayTime);

        while(true)
        {
            sprayCollider.enabled = true;

            if (sprayParticleSystem != null)
            {
                sprayParticleSystem.Play();
                audio.Play();
            }

            yield return new WaitForSeconds(2.0f);

            sprayCollider.enabled = false;
            audio.Pause();

            yield return new WaitForSeconds(3.0f);
        }
    }
}
