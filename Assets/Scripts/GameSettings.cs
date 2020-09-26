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
}

[Serializable]
public class ButtonSpriteDictionary : SerializableDictionary<ButtonType, Sprite> {}