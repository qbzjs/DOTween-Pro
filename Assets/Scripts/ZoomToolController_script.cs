using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToolController_script : MonoBehaviour
{
    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
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
