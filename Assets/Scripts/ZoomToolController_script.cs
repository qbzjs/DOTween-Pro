using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomToolController_script : MonoBehaviour
{
    Camera camera;

    public Image zoomButton;

    private void Awake()
    {
        camera = Camera.main;
    }

    public void Toggle()
    {
        float value = (zoomButton.material.GetFloat("_OutlineEnabled") > 0) ? 0 : 1;
        zoomButton.material.SetFloat("_OutlineEnabled", value);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseInWorldCoordinates = camera.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(mouseInWorldCoordinates.x, mouseInWorldCoordinates.y, this.transform.position.z);
        }
    }
}
