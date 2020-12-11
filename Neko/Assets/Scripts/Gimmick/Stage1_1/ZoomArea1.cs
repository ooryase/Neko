using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomArea1 : SwitchObject
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        PuchRequired = false;
        ZoomPos = transform.position + new Vector3(0.4f, -0.8f, -14.0f);
    }
}
