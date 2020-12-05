using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WebCamFaceLandmarkGetterの目の開閉情報から
/// 実際にゲーム上で使用する開閉状態を判断するクラス
/// (そのまま使うと誤認識やブレによる影響が出る為)
/// </summary>
public class EyeOpenChecker : MonoBehaviour
{
    private WebCam webCam = null;


    [Range(0, 10)]
    [SerializeField, TooltipAttribute("目の開閉状態を変えるまでに判定するフレーム数")]
    private int changeEyeOpenFrameCount = 8;

    // 目の開閉の閾値
    private float eyeOpenThreshold = 0.8f;

    /// <summary>
    /// 左目が開く(閉じる)までのカウンタ
    /// </summary>
    private int eyeOpenCounterL = 0;

    /// <summary>
    /// 右目が開く(閉じる)までのカウンタ
    /// </summary>
    private int eyeOpenCounterR = 0;

    /// <summary>
    /// 左目が開いていることにするかどうかの値
    /// </summary>
    private bool eyeOpenL = true;

    /// <summary>
    /// 右目が開いていることにするかどうかの値
    /// </summary>
    private bool eyeOpenR = true;

    /// <summary>
    /// 両目が閉じているかどうか
    /// </summary>
    private bool keepEyeOpen = true;
    public bool KEEP_EYE_OPEN { get => keepEyeOpen; private set => keepEyeOpen = value; }

    /// <summary>
    /// 目が開いたフレームならtrue
    /// </summary>
    private bool eyeOpen;
    public bool EYE_OPEN { get => eyeOpen; private set => eyeOpen = value; }

    /// <summary>
    /// 目が閉じたフレームならtrue
    /// </summary>
    private bool eyeClose;
    public bool EYE_CLOSE { get => eyeClose; private set => eyeClose = value; }

    [SerializeField]
    private TextMesh text = null;


    // Start is called before the first frame update
    void Start()
    {
        webCam = GetComponent<WebCam>();

        // 目の大きさによって閾値変えたいんですけどなにか？
        eyeOpenThreshold = ((EyesData.Instance.EyeSizeL + EyesData.Instance.EyeSizeR) / 2.0f) * 0.6f;
        Debug.Log("閾値 : " + eyeOpenThreshold);
    }

    // Update is called once per frame
    void Update()
    {
        eyeOpenCounterL = Mathf.Clamp(eyeOpenCounterL + eyeOpenCounterCheck(eyeOpenL, webCam.EYE_OPEN_L), 0, changeEyeOpenFrameCount);
        eyeOpenCounterR = Mathf.Clamp(eyeOpenCounterR + eyeOpenCounterCheck(eyeOpenR, webCam.EYE_OPEN_R), 0, changeEyeOpenFrameCount);

        if (eyeOpenCounterL >= changeEyeOpenFrameCount)
        {
            eyeOpenL = !eyeOpenL;
            eyeOpenCounterL = 0;
        }
        if (eyeOpenCounterR >= changeEyeOpenFrameCount)
        {
            eyeOpenR = !eyeOpenR;
            eyeOpenCounterR = 0;
        }

        //両目が閉じていれば目を閉じる
        EYE_OPEN = (!keepEyeOpen && (eyeOpenL || eyeOpenR));
        EYE_CLOSE = (keepEyeOpen && !(eyeOpenL || eyeOpenR));
        keepEyeOpen = (eyeOpenL || eyeOpenR);

        string outputText = "";
        outputText += webCam.EYE_OPEN_L.ToString("F2");
        outputText += " ";
        outputText += webCam.EYE_OPEN_R.ToString("F2");
        outputText += "\n";
        if (eyeOpenL&& eyeOpenR)
            outputText += "( 0 o 0 )";
        else if (eyeOpenL)
            outputText += "( - w 0 )";
        else if (eyeOpenR)
            outputText += "( 0 m - )";
        else
            outputText += "( - p - )";
        text.text = outputText;

    }

    /// <summary>
    /// 現在の開閉状態とカメラの情報が異なる場合カウンタを進める
    /// </summary>
    /// <param name="eyeOpen"></param>
    /// <param name="webCamEyeOpen"></param>
    /// <returns></returns>
    private int eyeOpenCounterCheck(bool eyeOpen, float webCamEyeOpen)
    {

        //現在の開閉状態とカメラの情報が異なる場合カウンタを進める
        return (eyeOpen != (webCamEyeOpen > eyeOpenThreshold)) ? 1 : -1;
    }

    public float GetOpenL()
    {
        return webCam.EYE_OPEN_L;
    }
    public float GetOpenR()
    {
        return webCam.EYE_OPEN_R;
    }
}
