using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBillboard_script : MonoBehaviour
{
    private Transform camera;
    private void Awake()
    {
        camera = Camera.main.transform;
    }
    void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + camera.forward);
    }
}
