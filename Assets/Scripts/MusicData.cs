using System.Collections.Generic;
using YorfLib;

public class MusicData
{
    public Dictionary<int, List<ButtonType>> Inputs = new Dictionary<int, List<ButtonType>>();
    public Dictionary<int, float> BeatUpdate = new Dictionary<int, float>();

    public Dictionary<int, BeatCombination> BeatDict;

    public void ParseData()
    {
        var dict = new Dictionary<int, BeatCombination>();

        foreach(var kv in Inputs)
        {
            dict.Add(kv.Key, new BeatCombination(kv.Value));
        }

        BeatDict = dict;
    }
}

public class MusicDataContainer
{
    public static MusicData Music1()
    {
        var musicData = new MusicData();

        musicData.Inputs.Add(5, new List<ButtonType>{
                    ButtonType.X,
                    ButtonType.B
        });
        musicData.Inputs.Add(10, new List<ButtonType>{
                    ButtonType.A,
                    ButtonType.B
        });

        musicData.ParseData();
        return musicData;
    }
}
