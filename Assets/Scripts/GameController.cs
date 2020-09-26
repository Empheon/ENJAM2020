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
        Get<MusicManager>().OnMusicBeat += BeatTransition;
    }

    void Update()
    {
        
    }

    private void BeatTransition()
    {

    }

    public void ValidateBeat()
    {

    }
}
