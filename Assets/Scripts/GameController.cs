using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YorfLib;
using static YorfLib.SingletonHelper;

public class GameController : MonoBehaviour
{
    public BeatCombination CurrentBeatCombination;

    void Start()
    {
        InitSingleton(this);
        MusicManager musicManager = Get<MusicManager>();
        musicManager.OnMusicBeat += NewBeat;
        musicManager.OnActivateCombination += ActivateCombination;
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

                GizmosHelper.AddText(new Vector3(0, 0, -5), CurrentBeatCombination.m_beats[CurrentBeatCombination.m_currentBeatIndex].m_first.ToString(), Color.red, Time.deltaTime * 2);
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
    }
}
