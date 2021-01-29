using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinemaScope : MonoBehaviour
{
    private PlayerController player;

    private Image[] label;
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        label = GetComponentsInChildren<Image>();
        alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.CinemaOn)
        {
            alpha = Mathf.Clamp(alpha + 0.05f, 0, 1.0f);
        }
        else
        {
            alpha = Mathf.Clamp(alpha - 0.01f, 0, 1.0f);
        }

        label[0].color = new Color(0, 0, 0, alpha);
        label[1].color = new Color(0, 0, 0, alpha);
    }
}
