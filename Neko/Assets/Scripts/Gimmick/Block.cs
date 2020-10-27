using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private MeshCollider coll;
    private Rigidbody rigit;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<MeshCollider>();
        rigit = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            coll.isTrigger = true;
            rigit.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
