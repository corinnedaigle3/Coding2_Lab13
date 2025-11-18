using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BillboardManager : MonoBehaviour
{
    private const string webImage = "https://cdn.pixabay.com/photo/2014/09/29/17/33/chihuahua-466236_1280.jpg"; // Chihuahua
    private const string webImage1 = "https://cdn.pixabay.com/photo/2015/12/23/22/39/minecraft-1106261_1280.png"; // Minecraft
    private const string webImage2 = "https://cdn.pixabay.com/photo/2024/03/03/05/39/ai-generated-8609813_1280.png"; // Cerberus

    private Texture2D texture;
    private Texture2D texture1;
    private Texture2D texture2;

    private int currentIndex;

    public Renderer cube1Renderer;
    public Renderer cube2Renderer;
    public Renderer cube3Renderer;

    public void Start()
    {
        GetWebImage(t => cube1Renderer.material.mainTexture = t);
        GetWebImage(t => cube2Renderer.material.mainTexture = t);
        GetWebImage(t => cube3Renderer.material.mainTexture = t);
    }

    public void GetWebImage(Action<Texture2D> callback)
    {
        // Decide which texture to serve
        switch (currentIndex)
        {
            case 0:
                if (texture != null) 
                { 
                    callback(texture); 
                    break; 
                }
                StartCoroutine(DownloadImage(webImage, t => { texture = t; callback(t); }));
                break;

            case 1:
                if (texture1 != null) 
                { 
                    callback(texture1); 
                    break; 
                }
                StartCoroutine(DownloadImage(webImage1, t => { texture1 = t; callback(t); }));
                break;

            case 2:
                if (texture2 != null) 
                { 
                    callback(texture2); 
                    break; 
                }
                StartCoroutine(DownloadImage(webImage2, t => { texture2 = t; callback(t); }));
                break;
        }

        // Advance to next picture
        currentIndex++;

        if (currentIndex > 2)
        {
            currentIndex = 0;
        }
    }

    public IEnumerator DownloadImage(string url, Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}