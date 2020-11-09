﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    private GameObject mask;
    private Animator fade;

    private void Awake()
    {
        mask = transform.GetChild(0).GetChild(0).gameObject;

        fade = mask.GetComponent<Animator>();
        //FadeIn();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(fade.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    public void FadeIn()
    {
        // これやんないと黒い画像邪魔
        mask.GetComponent<Image>().enabled = true;
        fade.SetTrigger("In");
    }

    public void FadeOut()
    {
        // これやんないと黒い画像邪魔
        mask.GetComponent<Image>().enabled = true;
        fade.SetTrigger("Out");
    }

    public void FadeIn_Dead()
    {
        mask.GetComponent<Image>().enabled = true;
        fade.SetTrigger("In_Dead");
    }


    // 再生が終わったらtrue（これすごい使い勝手悪い）
    public bool IsEnd()
    {
        if(fade.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            return true;
        }
        return false;
    }
}