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
    // 目の閾値
    public readonly string key_eye = "Eye";
    public float eyeOpenThreshold = 0.35f;

    // フルスクリーンかどうか
    public readonly string key_full = "Full";
    public int fullScreen = 1; // 仕方なくint

    // Start is called before the first frame update
    void Start()
    {
        // データがなかったら0.35fを入れる
        eyeOpenThreshold = PlayerPrefs.GetFloat(key_eye, 0.35f);

        fullScreen = PlayerPrefs.GetInt(key_full, 1);
        Screen.fullScreen = fullScreen == 1 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
