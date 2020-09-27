using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;

public class WwiseMusicManager : MonoBehaviour
{
    public AK.Wwise.Event Play_MainMusic;

    public static EventHandler OnMusicBeat;
    public static EventHandler OnMusicCue;

    public static WwiseMusicManager instance;

    private void Awake()
    {
        InitSingleton(this);
        if (instance)
            Destroy(gameObject);
        else
            instance = null;
    }

    private void Start()
    {
        //Play_MainMusic.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncAll, CallbackFunction);
        //AkSoundEngine.SetState("STATES_MainMusic", "MainMenu");
    }

    public void StartMainMusic()
    {
        Play_MainMusic.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncAll, CallbackFunction);
        AkSoundEngine.SetState("STATES_MainMusic", "MainMenu");
    }

    private void CallbackFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (in_type == AkCallbackType.AK_MusicSyncBeat)
            OnMusicBeat?.Invoke(this, null);
        else if (in_type == AkCallbackType.AK_MusicSyncUserCue)
            OnMusicCue?.Invoke(this, null);
    }
}
