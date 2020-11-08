using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class CameraNoiseEditer : MonoBehaviour
{
    [SerializeField]
    EyeOpenChecker eyeOpenChecker = null;

    [SerializeField]
    Material material = null;

    [SerializeField]
    [Range(0, 1)]
    float noiseX = 0.0f;
    public float NoiseX { get { return noiseX; } set { noiseX = value; } }

    [SerializeField]
    [Range(0, 1)]
    float rgbNoise;
    public float RGBNoise { get { return rgbNoise; } set { rgbNoise = value; } }

    [SerializeField]
    Vector2 offset;
    public Vector2 Offset { get { return offset; } set { offset = value; } }

    [SerializeField]
    [Range(0, 2)]
    float scanLineTail = 2.0f;
    public float ScanLineTail { get { return scanLineTail; } set { scanLineTail = value; } }

    [SerializeField]
    [Range(-10, 10)]
    float scanLineSpeed = 10;
    public float ScanLineSpeed { get { return scanLineSpeed; } set { scanLineSpeed = value; } }

    [SerializeField]
    [Range(0, 1)]
    float brightness = 1.0f;
    public float Brightness { get { return brightness; } set { brightness = value; } }

    private float noiseValue = 0.0f;


    // Use this for initialization
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        material.SetFloat("_NoiseX", noiseX);
        material.SetFloat("_RGBNoise", rgbNoise);
        material.SetFloat("_ScanLineSpeed", scanLineSpeed);
        material.SetFloat("_ScanLineTail", scanLineTail);
        material.SetVector("_Offset", offset);
        material.SetFloat("_Brightness", brightness);
        Graphics.Blit(src, dest, material);
    }

    private void Update()
    {
        if (eyeOpenChecker.EYE_OPEN)
            StartCoroutine(eyeOpen());
        else if (eyeOpenChecker.EYE_CLOSE)
            StartCoroutine(eyeClose());

        brightness = 1.0f - noiseValue * 0.5f;
        NoiseX = 0.01f * noiseValue;
        RGBNoise = 0.2f * noiseValue;
    }

    private IEnumerator eyeClose()
    {
        for (int i = 0; i < 16; i++)
        {
            noiseValue = i * 0.0625f;
            yield return null;
        }
        noiseValue = 1.0f;
    }

    private IEnumerator eyeOpen()
    {
        for (int i = 0; i < 16; i++)
        {
            noiseValue = 1.0f - i * 0.0625f;
            yield return null;
        }
        noiseValue = 0.0f;
    }

}