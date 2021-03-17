using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayer_script : MonoBehaviour
{
    string URL = "Assets/Videos/The Goal - 27070.mp4";
    VideoPlayer videoPlayer;

    private void Start()
    {
        Init();           
    }

    private void Init()
    {
        GameObject camera = Camera.main.gameObject;
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = true;
        videoPlayer.url = URL;
        videoPlayer.isLooping = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else { videoPlayer.Play(); }
        }
    }
}
