using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        se.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
