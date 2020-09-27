using System.Collections.Generic;
using UnityEngine;
using YorfLib;

public class MusicData : MonoBehaviour
{
    public Dictionary<int, List<ButtonType>> Inputs = new Dictionary<int, List<ButtonType>>();
    public Dictionary<int, string> BeatUpdate = new Dictionary<int, string>();

    public Dictionary<int, BeatCombination> BeatDict;

    public int FinishBeat = 110;

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


        //musicData.BeatUpdate.Add(80, "Transition_110_to_120");
        //musicData.BeatUpdate.Add(80, "CoreGame_120BPM");
        //musicData.BeatUpdate.Add(160, "Transition_120_to_130");
        //musicData.BeatUpdate.Add(176, "CoreGame_130BPM");
        musicData.BeatUpdate.Add(110, "Ending");

        musicData.ParseData();
        return musicData;
    }
    public MusicData Music2()
    {
        var musicData = new MusicData();

        musicData.Inputs.Add(15, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.NONE,
                    ButtonType.B,
                    ButtonType.DOG1
        });

        musicData.Inputs.Add(23, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.B,
                    ButtonType.NONE,
                    ButtonType.X,
                    ButtonType.DOG1
        });

        musicData.Inputs.Add(31, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.L,
                    ButtonType.Y,
                    ButtonType.R,
                    ButtonType.DOG1
        });

        musicData.Inputs.Add(39, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.U,
                    ButtonType.B,
                    ButtonType.U,
                    ButtonType.A,
                    ButtonType.NONE,
                    ButtonType.D,
                    ButtonType.DOG1
        });

        musicData.Inputs.Add(51, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.U,
                    ButtonType.B,
                    ButtonType.D,
                    ButtonType.L,
                    ButtonType.X,
                    ButtonType.R,
                    ButtonType.Y,
                    ButtonType.DOG1
        });

        musicData.Inputs.Add(63, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.U,
                    ButtonType.A,
                    ButtonType.D,
                    ButtonType.B,
                    ButtonType.L,
                    ButtonType.B,
                    ButtonType.R,
                    ButtonType.NONE,
                    ButtonType.X,
                    ButtonType.DOG1
        });

        musicData.Inputs.Add(75, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.X,
                    ButtonType.U,
                    ButtonType.Y,
                    ButtonType.L,
                    ButtonType.A,
                    ButtonType.NONE,
                    ButtonType.Y,
                    ButtonType.NONE,
                    ButtonType.B,
                    ButtonType.U,
                    ButtonType.X,
                    ButtonType.D,
                    ButtonType.DOG1
        });

        musicData.Inputs.Add(89, new List<ButtonType>{
                    ButtonType.NONE,
                    ButtonType.B,
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.U,
                    ButtonType.Y,
                    ButtonType.R,
                    ButtonType.A,
                    ButtonType.NONE,
                    ButtonType.B,
                    ButtonType.L,
                    ButtonType.U,
                    ButtonType.L,
                    ButtonType.U,
                    ButtonType.D,
                    ButtonType.NONE,
                    ButtonType.B,
                    ButtonType.NONE,
                    ButtonType.A,
                    ButtonType.U,
                    ButtonType.D,
                    ButtonType.DOG1
        });


        //musicData.BeatUpdate.Add(80, "Transition_110_to_120");
        //musicData.BeatUpdate.Add(80, "CoreGame_120BPM");
        //musicData.BeatUpdate.Add(160, "Transition_120_to_130");
        //musicData.BeatUpdate.Add(176, "CoreGame_130BPM");
        musicData.BeatUpdate.Add(110, "Ending");

        musicData.ParseData();
        return musicData;
    }
}
