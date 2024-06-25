using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurManager : MonoBehaviour
{
    private static Camera blurCamera;
    public static RenderTexture renderTexture;
    [SerializeField] private Material blurMaterial;
    
    void Awake()
    {
        blurCamera = Camera.main.transform.GetChild(0).GetComponent<Camera>();
        
        if (blurCamera.targetTexture != null)
        {
            blurCamera.targetTexture.Release();
        }

        renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurCamera.targetTexture = renderTexture;
        blurMaterial.SetTexture("_RenderTexture", renderTexture);

        blurCamera.gameObject.SetActive(false);
    }

    public static void SetBlur(bool value)
    {
        blurCamera.gameObject.SetActive(value);
    }
    
}
