using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RenderTexture myRenderTexture;
    
    void Start()
    {
        myRenderTexture = new RenderTexture(255, 255, 8);

        Camera.main.targetTexture = myRenderTexture;
        Camera.main.Render();
        Camera.main.targetTexture = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
