using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using FreeDraw;

public class ToolManager_script : MonoBehaviour
{
    public RectTransform[] toolTransforms;
    public DrawingSettings settings;

    private float[] activatedToolsXPositions;
    private float[] deactivatedToolsXPositions;

    [Header("Move In/Out Controls")]
    public float distance = 300;
    public float duration = 1;



    private void OnEnable()
    {
        VideoPlayer_script.onPause += FadeInTools;
        VideoPlayer_script.onPlay += FadeOutTools;
    }

    private void OnDisable()
    {
        VideoPlayer_script.onPause -= FadeInTools;
        VideoPlayer_script.onPlay -= FadeOutTools;
    }

    private void Start()
    {
        SetupToolPositions();
    }

    private void SetupToolPositions()
    {
        activatedToolsXPositions = new float[toolTransforms.Length];
        deactivatedToolsXPositions = new float[toolTransforms.Length];

        for (int i = 0; i < toolTransforms.Length; i++)
        {
            activatedToolsXPositions[i] = toolTransforms[i].position.x;
            deactivatedToolsXPositions[i] = activatedToolsXPositions[i] + distance;
            Vector3 startPosition = new Vector3(deactivatedToolsXPositions[i], toolTransforms[i].position.y, toolTransforms[i].position.z);
            toolTransforms[i].position = startPosition;
        }
    }

    private void FadeInTools()
    {
        for (int i = 1; i < toolTransforms.Length; i++)
        {
            toolTransforms[i].DOMoveX(activatedToolsXPositions[i], duration);
        }
    }

    private void FadeOutTools()
    {
        for (int i = 1; i < toolTransforms.Length; i++)
        {
            toolTransforms[i].DOMoveX(deactivatedToolsXPositions[i], duration);
        }
    }


    //---------------------//
    // UI button functions //
    //---------------------//

    public void EquipZoomTool()
    {

    }

    public void EquipDrawTool()
    {

    }

    public void EquipEraser()
    {
        settings.SetEraser();
    }

    public void ChangeDrawColor(Image inputImage)
    {
        EquipDrawTool();
        Color color = inputImage.color;
        settings.SetColorCustom(color);
    }
}
