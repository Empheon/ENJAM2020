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
    public AudioClip beat_tmp;

    void Start()
    {
        InitSingleton(this);
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.clip = beat_tmp;
        m_musicData = new MusicDataContainer().Music1();
        float beatDuration = bpm / 60f / 4f;
        counter = beatDuration + BpmOffset + Mathf.Epsilon;
    }

    void FixedUpdate()
    {
        float beatDuration = bpm / 60f / 4f;
        Token.Speed = beatDuration * 4 * BeatCombination.ButtonsOffset;

        counter += Time.deltaTime;
        if (counter >= beatDuration + BpmOffset)
        {
            //Token.Speed = (bpm / 60f) * BeatCombination.ButtonsOffset * Time.deltaTime;
            counter = 0 + BpmOffset;
            OnMusicBeat?.Invoke();
            m_audioSource.Play();
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
