using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPointController_script : MonoBehaviour
{
    public Canvas inputCanvas;
    public TextMeshProUGUI worldSpaceText;
    public TMP_InputField inputField;

    private Camera camera;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragged;
    private CameraFlyController_script movementScript;

    private void Awake()
    {
        camera = Camera.main;
        movementScript = camera.GetComponent<CameraFlyController_script>();
    }

    // Lock and unlock control point in drag state
    void OnMouseDown()
    {
        isDragged = true;
    }

    private void OnMouseUp()
    {
        isDragged = false;
    }

    //Raycast from mouse to football field. Ignore other layers
    //When the football field is hit, move control point here
    private void FixedUpdate()
    {
        if (isDragged)
        {
            RaycastHit hit;
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("FootballField")))
            {
                this.transform.position = hit.point;
            }
        }
    }


    /////////////////////////
    /// UI Button Functions
    /// 

    public void EditText()
    {
        inputCanvas.gameObject.SetActive(true);
        movementScript.SetActive(false);
        inputField.text = worldSpaceText.text;
    }

    public void DoneEditText()
    {
        inputCanvas.gameObject.SetActive(false);
        movementScript.SetActive(true);
        worldSpaceText.text = inputField.text;
    }
}
