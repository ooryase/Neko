using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : SwitchObject
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Type = SwitchType.TexOnly;
        ZoomPos = tex.transform.position + new Vector3(0.0f, 0.0f, -13.0f);
    }
}
