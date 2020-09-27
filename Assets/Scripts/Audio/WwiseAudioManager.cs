using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseAudioManager : MonoBehaviour
{
    public static WwiseAudioManager instance;

    private void OnEnable()
    {
        WwiseMusicManager.OnMusicBeat += OnMusicBeat;
        WwiseMusicManager.OnMusicCue += OnMusicCue;
    }

    private void OnDisable()
    {
        WwiseMusicManager.OnMusicBeat -= OnMusicBeat;
        WwiseMusicManager.OnMusicCue -= OnMusicCue;
    }

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void OnMusicBeat(object sender, EventArgs e)
    {
        Debug.Log("On beat");
    }

    public void OnMusicCue(object sender, EventArgs e)
    {
        Debug.Log("On cue");
    }
}
