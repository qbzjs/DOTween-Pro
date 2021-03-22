using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using FreeDraw;
using System;

public class ToolManager_script : MonoBehaviour
{
    [Header("Public references")]
    public RectTransform[] drawToolTransforms;
    public DrawingSettings settings;
    public Image markerImage;
    public Image eraserImage;
    public Image zoomImage;
    public ZoomToolController_script zoomTool;

    private Color activeColor;

    private float[] activatedToolsXPositions;
    private float[] deactivatedToolsXPositions;

    [Header("Move In/Out Controls")]
    public float distance = 300;
    public float duration = 1;

    private enum Tools
    {
        zoom, marker, eraser
    }

    private Tools tool = new Tools();


    private void OnEnable()
    {
        VideoPlayer_script.onPause += FadeInTools;
        VideoPlayer_script.onPlay += FadeOutTools;
    }

    private void OnDisable()
    {
        VideoPlayer_script.onPause -= FadeInTools;
        VideoPlayer_script.onPlay -= FadeOutTools;
        ResetOutlineMaterials();
    }

    private void ResetOutlineMaterials()
    {
        zoomImage.material.SetFloat("_OutlineEnabled", 0);
        markerImage.material.SetFloat("_OutlineEnabled", 0);
        eraserImage.material.SetFloat("_OutlineEnabled", 0);
    }

    private void Start()
    {
        SetupToolPositions();
        SetupInitialDrawColor();
    }

    private void SetupInitialDrawColor()
    {
        //EquipDrawTool();
        activeColor = Color.green;
        settings.SetColorCustom(activeColor);
        markerImage.color = activeColor;
    }

    // Store to and from tween positions of the draw tools
    private void SetupToolPositions()
    {
        activatedToolsXPositions = new float[drawToolTransforms.Length];
        deactivatedToolsXPositions = new float[drawToolTransforms.Length];

        for(int i = 0; i < drawToolTransforms.Length; i++)
        {
            activatedToolsXPositions[i] = drawToolTransforms[i].position.x;
            deactivatedToolsXPositions[i] = activatedToolsXPositions[i] + distance;
            Vector3 startPosition = new Vector3(deactivatedToolsXPositions[i], drawToolTransforms[i].position.y, drawToolTransforms[i].position.z);
            drawToolTransforms[i].position = startPosition;
        }
    }

    // Function for tweening in the tools on pause, zoom tool excluded
    private void FadeInTools()
    {
        for(int i = 0; i < drawToolTransforms.Length; i++)
        {
            drawToolTransforms[i].DOMoveX(activatedToolsXPositions[i], duration);
        }
    }

    // Function for tweening out the tools on play, zoom tool excluded
    private void FadeOutTools()
    {
        for(int i = 0; i < drawToolTransforms.Length; i++)
        {
            drawToolTransforms[i].DOMoveX(deactivatedToolsXPositions[i], duration);
        }
    }


    //---------------------//
    // UI button functions //
    //---------------------//

    // Method for equiping the zoom tool by using the UI button
    public void EquipZoomTool()
    {
        print("PRESSED ZOOM");
        SetEquipedTool(Tools.zoom);
    }

    // Method for equiping the draw tool by using the UI button
    public void EquipDrawTool()
    {
        SetEquipedTool(Tools.marker);
    }

    // Method for equiping the eraser by using the UI button
    public void EquipEraser()
    {
        SetEquipedTool(Tools.eraser);
    }

    // Method for UI buttons for picking colors 
    public void ChangeDrawColor(Image inputImage)
    {
        EquipDrawTool();
        activeColor = inputImage.color;
        settings.SetColorCustom(activeColor);
        markerImage.color = activeColor;
    }

    // Overload method for activating draw tool with last selected color
    public void ChangeDrawColor()
    {
        settings.SetColorCustom(activeColor);
        markerImage.color = activeColor;
    }

    private void SetEquipedTool(Tools equipedTool)
    {
        //play sound
        SoundFX.instance.playSound(ref SoundFX.instance.click, 0.5f, true);

        // Deactivate all tools
        Drawable.drawable.SetActivation(false);
        zoomTool.SetActive(false);

        // Remove all selection outlines
        ResetOutlineMaterials();

        // Activate selected tool and outline
        switch(equipedTool)
        {
            case Tools.zoom:
                zoomTool.SetActive(true);
                zoomImage.material.SetFloat("_OutlineEnabled", 1);

                break;

            case Tools.marker:
                markerImage.material.SetFloat("_OutlineEnabled", 1);
                ChangeDrawColor();
                Drawable.drawable.SetActivation(true);

                break;

            case Tools.eraser:
                eraserImage.material.SetFloat("_OutlineEnabled", 1);
                settings.SetEraser();
                Drawable.drawable.SetActivation(true);
                break;
        }

    }
}
