using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayer_script : MonoBehaviour
{
    VideoPlayer videoPlayer;

    string URL = "Assets/Videos/The Goal - 27070.mp4";
    public VideoClip videoClip;

    // Broadcast pause to other classes
    public delegate void OnPlay();
    public static event OnPlay onPlay;

    private void Awake()
    {
        GameObject camera = Camera.main.gameObject;
        videoPlayer = camera.GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        videoPlayer.url = (videoClip == null) ? URL : videoClip.originalPath;

        videoPlayer.playOnAwake = true;
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
            else
            {
                onPlay?.Invoke();
                videoPlayer.Play();
            }
        }
    }
}
