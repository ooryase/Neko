﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position += Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
