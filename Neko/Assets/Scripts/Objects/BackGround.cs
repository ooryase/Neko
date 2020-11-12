using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode()]
public class BackGround : MonoBehaviour
{
    [SerializeField]
    private Material material = null;

    [SerializeField]
    Vector2 tiling = new Vector2(1, 1);

    [SerializeField]
    Vector2 offset = new Vector2(0, 0);

    [SerializeField]
    Texture2D Texture = null;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_MainTex");
        material.EnableKeyword("_MainTex_ST");

        material.SetTexture("_MainTex", Texture);
        Vector4 vector4 = new Vector4(tiling.x, tiling.y, offset.x, offset.y);
        material.SetVector("_MainTex_ST", vector4);
        material.SetVector("_Tiling", tiling);
        material.EnableKeyword("_Offset");
        material.SetVector("_Offset", offset);

    }

    //void OnRenderImage(RenderTexture src, RenderTexture dest)
    //{
    //    material.EnableKeyword("_MainTex");
    //    material.EnableKeyword("_MainTex_ST");

    //    material.SetTexture("_MainTex", Texture);
    //    Vector4 vector4 = new Vector4(tiling.x, tiling.y, offset.x, offset.y);
    //    material.SetVector("_MainTex_ST", vector4);
    //    Graphics.Blit(src, dest, material);
    //}

}
