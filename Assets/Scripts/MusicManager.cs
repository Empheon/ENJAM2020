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
    public float Speed;

    public float BpmOffset;
    private float bpm = 120;
    private float counter = 0;

    private AudioSource m_audioSource;
    public AudioClip beat_tmp;

    private float m_prevBeatTime;
    private bool m_acceptBeat = true;

    private int m_cueCounter = 10;

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
        //Get<WwiseMusicManager>().StartMainMusic();
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
        m_cueCounter++;
        if (m_cueCounter < 2)
        {
            return;
        }
        Debug.Log(m_currentBeat);
        string musicKey;
        if (m_musicData.BeatUpdate.TryGetValue(m_currentBeat - 1, out musicKey))
        {
            AkSoundEngine.SetState("STATES_MainMusic", musicKey);
            m_acceptBeat = !musicKey.Contains("Transition");
        }

        UpdateBeat();
    }

    public void MusicCue(object sender, EventArgs e)
    {
        m_cueCounter = 0;
        m_acceptBeat = false;
        string musicKey;
        if (m_musicData.BeatUpdate.TryGetValue(m_currentBeat - 1, out musicKey))
        {
            AkSoundEngine.SetState("STATES_MainMusic", musicKey);
            m_acceptBeat = !musicKey.Contains("Transition");
        }
        UpdateBeat();
        Debug.Log("On cue");
    }

    private void UpdateBeat()
    {
        if (m_prevBeatTime < 0)
        {
            m_prevBeatTime = Time.time;
            return;
        }
        BeatDuration = Time.time - m_prevBeatTime;
        m_prevBeatTime = Time.time;

        if (1 / BeatDuration > 3f)
        {
            BeatDuration = 2.02877f;
        }

        Speed = 1 / BeatDuration;
        Token.Speed = Speed;
        BackgroundManager.Speed = Speed;
        Get<ActionButton>().CharacterAnimator.speed = Speed / 2;
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
}
