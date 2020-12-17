using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarCry : MonoBehaviour
{
    [SerializeField] private NekoPredetor nekoPredetor = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            nekoPredetor.WarCry();
            gameObject.SetActive(false);
        }
    }
}
