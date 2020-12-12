using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncertainObject : MonoBehaviour
{
    private EyeOpenChecker eyeOpenChecker;
    private Renderer objectRenderer;
    private Material material;
    private Animator animator;
    private Collider objectCollider;
    private AudioSource audioSource;

    private enum State
    {
        Certain,
        Uncertain,
        Vanish,
    }

    private State state;

    public float shaderRGBNoise = 0.0f;
    public float shaderNoiseX = 0.0f;
    public float shaderSinNoiseWidth = 0.0f;
    public float shaderSinNoiseScale = 0.0f;
    public float shaderSinNoiseOffset = 0.0f;


    [SerializeField] private Texture2D mainTex = null;
    [SerializeField] private Texture2D normalMap = null;

    private bool hasFloorTag;

    public bool IsVanish { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Certain;
        eyeOpenChecker = GameObject.FindGameObjectWithTag("WebCam").GetComponent<EyeOpenChecker>();
        objectRenderer = GetComponent<Renderer>();
        material = objectRenderer.material;
        animator = GetComponent<Animator>();
        objectCollider = GetComponent<Collider>();

        hasFloorTag = (gameObject.tag == "Floor");

        material.SetTexture("_MainTex", mainTex);
        material.SetTexture("_NormalMap", normalMap);
        IsVanish = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Certain:
                if(eyeOpenChecker.EYE_CLOSE &&
                    objectRenderer.isVisible)
                {
                    Uncertain();
                }
                break;
            case State.Uncertain:
                if (eyeOpenChecker.EYE_OPEN)
                {
                    Vanish();
                }
                break;
            case State.Vanish:
                break;
        }

        ShaderSet();
    }

    private void Uncertain()
    {
        animator.SetTrigger("Uncertain");
        state = State.Uncertain;
    }

    private void Vanish()
    {
        audioSource.Play();
        IsVanish = true;
        animator.SetTrigger("Vanish");
        state = State.Vanish;
        if (hasFloorTag)
            gameObject.tag = "Cliff";
    }

    private void VanishComplete()
    {
        objectCollider.enabled = false;
        objectRenderer.enabled = false;
    }

    private void ShaderSet()
    {
        material.SetFloat("_RGBNoise", shaderRGBNoise);
        material.SetFloat("_NoiseX", shaderNoiseX);
        material.SetFloat("_SinNoiseWidth", shaderSinNoiseWidth);
        material.SetFloat("_SinNoiseScale", shaderSinNoiseScale);
        material.SetFloat("_SinNoiseOffset", shaderSinNoiseOffset);
    }

    public void ResetObject()
    {
        IsVanish = false;
        objectCollider.enabled = true;
        objectRenderer.enabled = true;
        animator.Play("Certain");
        state = State.Certain;
        if (hasFloorTag)
            gameObject.tag = "Floor";
    }
}