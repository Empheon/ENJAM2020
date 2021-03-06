﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using YorfLib;
using static YorfLib.SingletonHelper;

public class GameController : MonoBehaviour
{
    public BeatCombination CurrentBeatCombination;

    public CombinationEndScreen SuccessScreen;
    public CombinationEndScreen FailScreen;

    private float m_totalPoints = 0;
    public bool hasFinished = false;

    void Start()
    {
        InitSingleton(this);
        MusicManager musicManager = Get<MusicManager>();
        musicManager.OnMusicBeat += NewBeat;
        musicManager.OnActivateCombination += ActivateCombination;

        SuccessScreen = Instantiate(SuccessScreen);
        FailScreen = Instantiate(FailScreen);
    }

    void Update()
    {
        if (CurrentBeatCombination != null)
        {
            float timeSinceLastBeat = Time.time - CurrentBeatCombination.m_timeAtLastBeat;
            //Debug.Log(timeSinceLastBeat + " " + (timeSinceLastBeat < CurrentBeatCombination.m_inputAcceptanceDuration / 2f) + " " + (timeSinceLastBeat > CurrentBeatCombination.m_beatDuration - (CurrentBeatCombination.m_inputAcceptanceDuration / 2f)));
            if (timeSinceLastBeat < CurrentBeatCombination.m_inputAcceptanceDuration / 2f ||
                timeSinceLastBeat > CurrentBeatCombination.m_beatDuration - (CurrentBeatCombination.m_inputAcceptanceDuration / 2f))
            {

                GizmosHelper.AddText(new Vector3(-4, -2.5f, 0), CurrentBeatCombination.m_beats[CurrentBeatCombination.m_currentBeatIndex].m_first.ToString(), Color.red, Time.deltaTime * 2);
            }
        }
    }

    private void NewBeat()
    {
        if (CurrentBeatCombination != null)
        {
            CurrentBeatCombination.BeatAction();
        }
    }

    private void ActivateCombination(BeatCombination beatCombination, float beatDuration)
    {
        beatCombination.Init(beatDuration);
        CurrentBeatCombination = beatCombination;
        CurrentBeatCombination.BeatAction();
    }

    public void CombinationFinished(float points)
    {
        CurrentBeatCombination = null;
        if (points > 0)
        {
            SuccessScreen.PlayAnim();
        } else
        {
            FailScreen.PlayAnim();
        }
        m_totalPoints += points;
    }

    // oui c'est sale
    public T InstantiateDelegate<T>(T go, Vector3 pos, Quaternion q) where T : Object
    {
        return Instantiate(go, pos, q);
    }

    public void Loose()
    {
        hasFinished = true;
        if (CurrentBeatCombination != null)
        {
            m_totalPoints += CurrentBeatCombination.ForceFinish();
        }
        Get<EndScreen>().AnimateEndScreen(true, Mathf.RoundToInt(m_totalPoints * 100) + "\n\nEssayez encore !");
        Get<InputController>().gameObject.SetActive(false);
    }

    public void Win()
    {
        hasFinished = true;
        if (CurrentBeatCombination != null)
        {
            m_totalPoints += CurrentBeatCombination.ForceFinish();
        }
        Get<EndScreen>().AnimateEndScreen(true, Mathf.RoundToInt(m_totalPoints * 100) + "\n\nBien joué !");
        Get<InputController>().gameObject.SetActive(false);

    }
}
