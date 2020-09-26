using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;

public class MusicManager : MonoBehaviour
{
    public Action OnMusicBeat;
    public Action OnCustomCue;
    public Action OnNewCombination;

    void Start()
    {
        InitSingleton(this);
    }

    void Update()
    {
        
    }
}
