using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    protected GameObject tex;
    private Animator anim_tex;

    // Start is called before the first frame update
    void Start()
    {
        tex = transform.GetChild(0).gameObject;
        anim_tex = tex.GetComponent<Animator>();
        tex.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            tex.SetActive(true);
            anim_tex.SetTrigger("popUp");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim_tex.SetTrigger("popDown");
        }

    }
}
