using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PaintBrush_script : MonoBehaviour
{

    public GameObject strokePrefab;

    [Range(0.5f, 1.5f)]
    public float strokeSize;

    private Camera camera;
    private VideoPlayer videoPlayer;

    // For stabilizing when drawing fast
    private Vector3 previousStrokePosition = new Vector3();
    public int maxStrokes;

    private void Awake()
    {
        camera = Camera.main;
        videoPlayer = camera.GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        VideoPlayer_script.onPlay += ClearDrawing;
    }

    private void OnDisable()
    {
        VideoPlayer_script.onPlay -= ClearDrawing;
    }

    void Update()
    {
        CheckForInput();
    }



    private void CheckForInput()
    {
        // Controls for play/pause video
        if (Input.GetMouseButton(0) && videoPlayer.isPaused)
        {
            Vector3 mouseInWorldCoordinates = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 strokePosition = new Vector3(mouseInWorldCoordinates.x, mouseInWorldCoordinates.y, -1);

            Draw(strokePosition);



            // Code for drawing on a collider
            //-----

            /*
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Draw(hit.point);
            }
            */
        }
    }

    private void Draw(Vector3 point)
    {
        //previousStrokePositions.(point);

        //if (previousStrokePositions.Count > maxStrokes)
        //{
        //    previousStrokePositions.Dequeue();
        //}

        //for (int i = 0; i < previousStrokePositions.Count; i++)
        //{
        //}
        GameObject stroke = Instantiate(strokePrefab, point, Quaternion.identity, this.transform);
        stroke.transform.localScale *= strokeSize;
    }



    // Could be optimized with for instance object pooling
    public void ClearDrawing()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }
}
