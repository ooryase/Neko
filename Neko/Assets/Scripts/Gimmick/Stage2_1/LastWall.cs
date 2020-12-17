using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastWall : MonoBehaviour
{
    [SerializeField] private NekoPredetor predetor = null;
    [SerializeField] private BGMManagerBoss bgm = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (predetor.DeadFlag)
        {
            bgm.BGMStop();
            Destroy(gameObject);
        }
    }
}
