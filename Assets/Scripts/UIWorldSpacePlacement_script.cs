using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldSpacePlacement_script : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Vector3 worldSpacePlacement = camera.WorldToScreenPoint(target.position + offset);

        if (this.transform.position != worldSpacePlacement)
        {
            this.transform.position = worldSpacePlacement;
        }
    }

}
