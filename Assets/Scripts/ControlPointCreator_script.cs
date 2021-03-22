using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointCreator_script : MonoBehaviour
{
    public GameObject prefab;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpawnControlPoint();
        }
    }

    private void SpawnControlPoint()
    {
        RaycastHit hit;
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("FootballField")))
        {
            SoundFX.instance.playSound(ref SoundFX.instance.click2, 0.6f, true);
            Instantiate(prefab, hit.point, Quaternion.identity);
        }
    }
}
