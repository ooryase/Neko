using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmClaw : MonoBehaviour
{
    [SerializeField] private GameObject clawPos = null;

    private Vector3 ofset;
    // Start is called before the first frame update
    void Start()
    {
        ofset = transform.position - clawPos.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = clawPos.transform.position + ofset;
    }
}
