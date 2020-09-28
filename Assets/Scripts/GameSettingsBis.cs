
using static YorfLib.SingletonHelper;
using UnityEngine;
using System;
using YorfLib;

public class GameSettingsBis : MonoBehaviour
{
    private void Awake()
    {
        InitSingleton(this);
    }

    public ButtonSpriteDictionary XboxButtonSprite;
    public ButtonSpriteDictionary PlayStationButtonSprite;

    public ButtonColorDictionary XboxButtonColor;
    public ButtonColorDictionary PlayStationButtonColor;
    public Color ArrowButtonsColor;

    public Token TokenPrefab;
}

[Serializable]
public class ButtonSpriteDictionary : SerializableDictionary<ButtonType, Sprite> { }
[Serializable]
public class ButtonColorDictionary : SerializableDictionary<ButtonType, Color> { }