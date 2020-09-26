using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using YorfLib;
using static YorfLib.SingletonHelper;

public class GameController : MonoBehaviour
{
    public BeatCombination CurrentBeatCombination;

    public Token token;

    void Start()
    {
        InitSingleton(this);
        MusicManager musicManager = Get<MusicManager>();
        musicManager.OnMusicBeat += NewBeat;
        musicManager.OnActivateCombination += ActivateCombination;

        MasterPool.RegisterObject(token, false, 30);
        int offset = 2;
        for (int i = 0; i < 30; i++)
        {
            PoolTokenSetup(Vector2(10 + i * offset, 0), null);
        }
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

    private void PoolTokenSetup(Vector2 pos, Sprite sprite)
    {
        MasterPool.SpawnObject(token, (Vector3)pos, Quaternion.identity);
    }

    public void PoolRetrieveToken()
    {

    }
}
