using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayer_script : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private VideoProjection_script projection;

    private string URL = "Assets/Videos/The Goal - 27070.mp4";
    public VideoClip videoClip;

    // Broadcast pause to other classes
    public delegate void OnPlayPlause();
    public static event OnPlayPlause onPlay;
    public static event OnPlayPlause onPause;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        projection = GetComponentInChildren<VideoProjection_script>();
    }

    private void Start()
    {
        SetupVideoPlayer();
    }

    private void SetupVideoPlayer()
    {
        videoPlayer.targetTexture = projection.GetProjectionTexture();
        videoPlayer.url = (videoClip == null) ? URL : videoClip.originalPath;


        videoPlayer.playOnAwake = false;
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
