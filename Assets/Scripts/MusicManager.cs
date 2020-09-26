using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;
using YorfLib;

public class MusicManager : MonoBehaviour
{
    public Action OnMusicBeat;
    public Action OnCustomCue;
    public Action<BeatCombination, float> OnActivateCombination;

    private MusicData m_musicData;
    private int m_currentBeat = 0;

    public float BpmOffset;
    private float bpm = 120;
    private float counter = 0;
    
    private AudioSource m_audioSource;

    void Start()
    {
        InitSingleton(this);
        //m_audioSource = GetComponent<AudioSource>();
        //m_audioSource.clip = beat_tmp;
        //m_audioSource.Play();
        GetComponent<AudioSource>().Play();
        m_musicData = new MusicDataContainer().Music1();
    }

    void Update()
    {
        float beatDuration = bpm / 60f / 4f;

        counter += Time.deltaTime;
        if (counter > beatDuration + BpmOffset)
        {
            counter = 0 + BpmOffset;
            OnMusicBeat?.Invoke();
            GizmosHelper.AddBox(Vector3.zero, Vector3.one * 54, Color.blue, beatDuration / 2f);

            m_currentBeat++;
            BeatCombination bc;
            if (m_musicData.BeatDict.TryGetValue(m_currentBeat, out bc))
            {
                OnActivateCombination?.Invoke(bc, beatDuration);
                GizmosHelper.AddBox(Vector3.zero, Vector3.one * 5, Color.red, beatDuration / 2f);
            }
        }
    }
}
