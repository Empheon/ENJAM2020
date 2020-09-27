using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using YorfLib;
using System;

public class GameSettings : ScriptableSingleton<GameSettings>
{
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