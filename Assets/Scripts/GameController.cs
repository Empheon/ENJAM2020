using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
}
