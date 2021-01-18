using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance
    {
        get; private set;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public readonly string key_eye = "Eye";
    public float eyeOpenThreshold = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        // データがなかったら0.35fを入れる
        //if (PlayerPrefs.HasKey(key_eye))
        //{
        //    PlayerPrefs.SetFloat(key_eye, 0.35f);
        //}

        eyeOpenThreshold = PlayerPrefs.GetFloat(key_eye, 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
