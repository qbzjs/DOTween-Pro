using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoProjection_script : MonoBehaviour
{
    Camera camera;
    RenderTexture projectionTexture;
    Renderer renderer;

    private void Awake()
    {
        camera = Camera.main;
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        SetupQuadSize();
    }

    private void SetupQuadSize()
    {
        float width = ScreenSize.GetScreenToWorldWidth;
        float height = ScreenSize.GetScreenToWorldHeight;
        transform.localScale = new Vector3(width, height, 1);
    }

    private void SetupRenderTexture()
    {
        if (camera.targetTexture != null)
        {
            camera.targetTexture.Release();
        }

        projectionTexture = new RenderTexture(Screen.width, Screen.height, 24);
        projectionTexture.Create();
        renderer.material.mainTexture = projectionTexture;
    }

    public RenderTexture GetProjectionTexture()
    {
        SetupRenderTexture();
        return projectionTexture;
    }
}
