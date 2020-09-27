using System.Collections.Generic;
using UnityEngine;
using YorfLib;

public class MusicData : MonoBehaviour
{
    public Dictionary<int, List<ButtonType>> Inputs = new Dictionary<int, List<ButtonType>>();
    public Dictionary<int, float> BeatUpdate = new Dictionary<int, float>();

    public Dictionary<int, BeatCombination> BeatDict;

    public void ParseData()
    {
        var dict = new Dictionary<int, BeatCombination>();

        foreach (var kv in Inputs)
        {
            dict.Add(kv.Key, new BeatCombination(kv.Value, kv.Key));
        }

        BeatDict = dict;
    }
}

public class MusicDataContainer
{
    public MusicData Music1()
    {
        var musicData = new MusicData();

        musicData.Inputs.Add(15, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.X,
                    ButtonType.B,
                    ButtonType.DOG1
        });
        for (int i = 0; i < 50; i++)
        {
            musicData.Inputs.Add(20 + i * 10, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.B,
                    ButtonType.L,
                    ButtonType.NONE,
                    ButtonType.B,
                    ButtonType.DOG1
        });

        }



        musicData.ParseData();
        return musicData;
    }
}
