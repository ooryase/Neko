using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesData : MonoBehaviour
{
    public static EyesData Instance
    {
        get; private set;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float EyeSizeL = 0.1f;
    public float EyeSizeR = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
