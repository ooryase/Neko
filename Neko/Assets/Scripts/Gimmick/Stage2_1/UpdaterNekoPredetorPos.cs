using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdaterNekoPredetorPos : MonoBehaviour
{
    [SerializeField] private Transform resetPos = null;
    [SerializeField] private NekoPredetor nekoPredetor = null;

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            nekoPredetor.SetResetPos(resetPos.position);
            boxCollider.enabled = false;
        }
    }
}
