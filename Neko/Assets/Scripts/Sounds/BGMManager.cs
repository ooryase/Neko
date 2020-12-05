using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private EyeOpenChecker eyeOpenChecker = null;
    [SerializeField] private GameObject player = null;
    private PlayerController playerController;
    private AudioSource source;
    [SerializeField] private float maxVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (player.gameObject.tag == "Player")
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(eyeOpenChecker.KEEP_EYE_OPEN == false)
        {
            if(source.volume > 0)
            {
                Mathf.Clamp(source.volume -= 0.05f, 0, maxVolume);
            }
        }
        else
        {
            if (source.volume < maxVolume)
            {
                Mathf.Clamp(source.volume += 0.05f, 0, maxVolume);
            }
        }

        // オーディオリスナーのため
        transform.position = player.transform.position;

        if (player.gameObject.tag == "Player")
        {
            if (playerController.State == PlayerState.Dead)
            {
                // 3Dサウンドオブジェクトの音を飛ばす
                transform.position = new Vector3(0, 0, 200.0f);
                source.volume = 0;
            }
        }
    }
}
