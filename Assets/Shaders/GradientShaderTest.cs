using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI; // for RawImage
using System.Collections.Generic;

public class GradientShaderTest : MonoBehaviour
{
    public ComputeShader computeTriangleShader;
    public RenderTexture renderTriangleTexture;
    
    public ComputeShader computeGradientShader;
    public RenderTexture renderGradientTexture;

    public enum ShaderType {
        TRIANGLE,
        GRADIENT
    };
    public ShaderType shaderType;

    private RawImage rawImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    /*void onAwake() {
        //rawImage = gameObject.GetComponent<RawImage>();
        List<RawImage> rawImages = new List<RawImage>(GetComponents<RawImage>());
        Debug.Log("RawImage List Size: " + rawImages.Count);

        rawImage = GetComponent<RawImage>();
        if (rawImage == null) {
            Debug.Log("onAwake : RawImage not found");
        } else {
            Debug.Log("onAwake : RawImage was found");
        }
    }*/

    void Start()
    {
        Debug.Log("Start");

        if (shaderType == ShaderType.TRIANGLE) {
            renderTriangleTexture = new RenderTexture(256, 256, 24);
            renderTriangleTexture.enableRandomWrite = true;
            renderTriangleTexture.Create();

            // "0" is the kernel in the computer shader (exists a find kernal by name function for this)
            // "Result" is its name in the compute shader
            computeTriangleShader.SetTexture(0, "Result", renderTriangleTexture);
            computeTriangleShader.Dispatch(0, renderTriangleTexture.width / 8, renderTriangleTexture.height / 8, 1);

            rawImage = gameObject.GetComponent<RawImage>();
            if (rawImage == null) {
                Debug.Log("RawImage not found");
            } else {
                Debug.Log("RawImage was found");
                rawImage.texture = (Texture)renderTriangleTexture;
            }

        } else if (shaderType == ShaderType.GRADIENT) {
            renderGradientTexture = new RenderTexture(256, 256, 24);
            renderGradientTexture.enableRandomWrite = true;
            renderGradientTexture.Create();

            computeGradientShader.SetTexture(0, "Result", renderGradientTexture);
            computeGradientShader.SetFloat("Resolution", renderGradientTexture.width);
            computeGradientShader.Dispatch(0, renderGradientTexture.width / 8, renderGradientTexture.height / 8, 1);

            rawImage = gameObject.GetComponent<RawImage>();
            if (rawImage == null){
                Debug.Log("RawImage not found");
            } else {
                Debug.Log("RawImage was found");
                rawImage.texture = (Texture)renderGradientTexture;
            }
        }

        

        

        

        

        // RawImage rawImage = gameObject.GetComponent<RawImage>();
        
        
        // rawImage.mainTexture = renderTexture;
        // rawImage.mainTexture = renderTexture;
    }
}
