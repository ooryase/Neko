using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomArea1 : SwitchObject
{
    [SerializeField] private Vector3 ofset = Vector3.zero;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Type = SwitchType.ZoomOnly;
        ZoomPos = transform.position + ofset;
    }
}
