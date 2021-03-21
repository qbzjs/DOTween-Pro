using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ZoomToolController_script : MonoBehaviour
{
    private Camera camera;
    private bool isActive = false;
    private Vector3 initialScale;
    public float tweenDuration = 0.3f;

    private void Awake()
    {
        camera = Camera.main;
        initialScale = this.transform.localScale;
    }

    private void Start()
    {
        this.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            print("MouseDOwn");
            if (this.transform.localScale.x != initialScale.x)
            {
                print("Tweening. Initialscale: " + initialScale);
                TweenLenseScale(initialScale);
            }
        }
        else if (Input.GetMouseButton(0) && isActive)
        {
            SetZoomTool();
            print("Mouse hold");
        }
    }

    private void SetZoomTool()
    {

        Vector3 mouseInWorldCoordinates = camera.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mouseInWorldCoordinates.x, mouseInWorldCoordinates.y, this.transform.position.z);
    }

    private void TweenLenseScale(Vector3 endValue)
    {
        this.transform.DOScale(endValue, tweenDuration);
    }

    // Could be part of an interface for a system with more tools
    public void SetActive(bool _isActive)
    {
        print("SetActive called with: " + _isActive);
        isActive = _isActive;
        if (isActive == false)
        {
            TweenLenseScale(Vector3.zero);
        }
    }

    public bool GetActive()
    {
        return isActive;
    }
}
