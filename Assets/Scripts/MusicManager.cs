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

    public float BeatDuration;

    public float BpmOffset;
    private float bpm = 120;
    private float counter = 0;

    private AudioSource m_audioSource;
    public AudioClip beat_tmp;

    private float m_prevBeatTime;

    void Start()
    {
        InitSingleton(this);
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.clip = beat_tmp;
        m_musicData = new MusicDataContainer().Music1();
        float beatDuration = bpm / 60f / 4f;
        counter = beatDuration + BpmOffset + Mathf.Epsilon;

        m_prevBeatTime = -1;
    }

    private void OnEnable()
    {
        Get<WwiseMusicManager>().StartMainMusic();
        WwiseMusicManager.OnMusicBeat += MusicBeat;
        WwiseMusicManager.OnMusicCue += MusicCue;
    }

    private void OnDisable()
    {
        WwiseMusicManager.OnMusicBeat -= MusicBeat;
        WwiseMusicManager.OnMusicCue -= MusicCue;
    }

    void FixedUpdate()
    {
        //BeatDuration = bpm / 60f / 4f;
        //Token.Speed = BeatDuration * 4 * BeatCombination.ButtonsOffset;

        //counter += Time.deltaTime;
        //if (counter >= BeatDuration + BpmOffset)
        //{
        //    //Token.Speed = (bpm / 60f) * BeatCombination.ButtonsOffset * Time.deltaTime;
        //    counter = 0 + BpmOffset;
        //    OnMusicBeat?.Invoke();
        //    m_audioSource.Play();
        //    GizmosHelper.AddBox(Vector3.zero, Vector3.one * 54, Color.blue, BeatDuration / 2f);

        //    m_currentBeat++;
        //    BeatCombination bc;
        //    if (m_musicData.BeatDict.TryGetValue(m_currentBeat, out bc))
        //    {
        //        OnActivateCombination?.Invoke(bc, BeatDuration);
        //        GizmosHelper.AddBox(Vector3.zero, Vector3.one * 5, Color.red, BeatDuration / 2f);
        //    }
        //}
    }

    public void MusicBeat(object sender, EventArgs e)
    {
        if (m_prevBeatTime < 0)
        {
            m_prevBeatTime = Time.time;
            return;
        }
        BeatDuration = Time.time - m_prevBeatTime;
        m_prevBeatTime = Time.time;

        Token.Speed = 1 / BeatDuration;
        Debug.Log(Token.Speed);

        OnMusicBeat?.Invoke();
        m_audioSource.Play();
        GizmosHelper.AddBox(Vector3.zero, Vector3.one * 54, Color.blue, BeatDuration / 2f);

        m_currentBeat++;
        BeatCombination bc;
        if (m_musicData.BeatDict.TryGetValue(m_currentBeat, out bc))
        {
            OnActivateCombination?.Invoke(bc, BeatDuration);
            GizmosHelper.AddBox(Vector3.zero, Vector3.one * 5, Color.red, BeatDuration / 2f);
        }
    }

    public void MusicCue(object sender, EventArgs e)
    {
        Debug.Log("On cue");
    }
}
