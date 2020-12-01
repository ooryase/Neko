using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private EyeOpenChecker eyeOpenChecker = null;
    private AudioSource source;
    [SerializeField] private float maxVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(eyeOpenChecker.KEEP_EYE_OPEN == false)
        {
            if(source.volume > 0)
            {
                Mathf.Clamp(source.volume -= 0.05f, 0, maxVolume);
            }
        }
        else
        {
            if (source.volume < maxVolume)
            {
                Mathf.Clamp(source.volume += 0.05f, 0, maxVolume);
            }
        }
    }
}
