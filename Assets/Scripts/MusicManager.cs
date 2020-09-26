using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;

public class MusicManager : MonoBehaviour
{
    public Action OnMusicBeat;
    public Action OnCustomCue;
    public Action<BeatCombination, float> OnActivateCombination;

    private MusicData m_musicData;

    void Start()
    {
        InitSingleton(this);
        m_musicData = MusicDataContainer.Music1();
    }

    void Update()
    {
        
    }
}
