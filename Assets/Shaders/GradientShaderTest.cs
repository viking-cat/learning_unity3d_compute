using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI; // for RawImage
using System.Collections.Generic;

public class GradientShaderTest : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;

    private RawImage rawImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void onAwake() {
        //rawImage = gameObject.GetComponent<RawImage>();
        List<RawImage> rawImages = new List<RawImage>(GetComponents<RawImage>());
        Debug.Log("RawImage List Size: " + rawImages.Count);

        rawImage = GetComponent<RawImage>();
        if (rawImage == null) {
            Debug.Log("onAwake : RawImage not found");
        } else {
            Debug.Log("onAwake : RawImage was found");
        }
    }

    void Start()
    {
        Debug.Log("Start");
        
        renderTexture = new RenderTexture(256, 256, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        // "0" is the kernel in the computer shader (exists a find kernal by name function for this)
        // "Result" is its name in the compute shader
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);

        // RawImage rawImage = gameObject.GetComponent<RawImage>();
        rawImage = gameObject.GetComponent<RawImage>();
        if (rawImage == null) {
            Debug.Log("RawImage not found");
        } else {
            Debug.Log("RawImage was found");
            rawImage.texture = (Texture)renderTexture;
        }
        
        // rawImage.mainTexture = renderTexture;
        // rawImage.mainTexture = renderTexture;
    }

    /*private void OnGUI()
    {
        rawImage = gameObject.GetComponent<RawImage>();
        if (rawImage == null) {
            Debug.Log("OnGUI : RawImage not found");
        } else {
            Debug.Log("OnGUI : RawImage was found");
        }

    }*/

    // Event function that Unity calls after a Camera has finished rendering, that allows you to modify the Camera's final image.
    // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.OnRenderImage.html
    /*private void OnRenderImage(RenderTexture src, RenderTexture dest) 
    {
        Debug.Log("OnRenderImage");
        
        if (renderTexture == null) {
            renderTexture = new RenderTexture(256, 256, 24);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }

        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);

        Graphics.Blit(renderTexture, dest);
    }*/

    // Update is called once per frame
    // void Update(){}
}
