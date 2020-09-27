﻿using System;
using System.Collections.Generic;
using UnityEngine;
using YorfLib;
using static YorfLib.SingletonHelper;

public class BeatCombination
{
    public static float CursorXPosition = -4;
    public static float ButtonsOffset = 1;

    public Action OnValidAction;
    public Action OnFailedAction;
    public Action OnPressFailedAction;
    public Action OnMissedAction;

    public List<Pair<ButtonType, BeatState>> m_beats;
    public Dictionary<Pair<ButtonType, BeatState>,Token> m_tokens;
    public float m_beatDuration;
    public float m_inputAcceptanceDuration;

    public float m_timeAtLastBeat;

    public int m_currentBeatIndex;

    public BeatCombination(List<ButtonType> inputs, int offsetIndex)
    {
        m_beats = new List<Pair<ButtonType, BeatState>>();
        m_tokens = new Dictionary<Pair<ButtonType, BeatState>, Token>();

        foreach (ButtonType bt in inputs)
        {
            m_beats.Add(new Pair<ButtonType, BeatState>(bt, BeatState.WAITING));
        }

        GizmosHelper.AddBox(new Vector3(CursorXPosition, -3, 0), Vector3.one * 0.5f, Color.red, 50);
        // Init tokens
        int i = 0;
        foreach (var pair in m_beats)
        {
            if (pair.m_first != ButtonType.NONE)
            {
                Token t = Get<GameController>().InstantiateDelegate<Token>(Get<GameSettings>().TokenPrefab, new Vector3(CursorXPosition + offsetIndex + i * ButtonsOffset, -3, 0), Quaternion.identity);
                t.Init(pair.m_first, this);
                m_tokens.Add(pair, t);
            }
            i++;
        }
    }

    public void Init(float beatDuration)
    {
        m_beatDuration = beatDuration;

        // We set a minimum duration to allow the player to react
        m_inputAcceptanceDuration = Mathf.Max(beatDuration / 3f, 0.4f);
    }

    public void ReceiveInput(ButtonType button)
    {
        if (m_beats[m_currentBeatIndex].m_second != BeatState.WAITING)
        {
            return;
        }

        float timeSinceLastBeat = Time.time - m_timeAtLastBeat;
        if (timeSinceLastBeat < m_inputAcceptanceDuration / 2f ||
            timeSinceLastBeat > m_beatDuration - (m_inputAcceptanceDuration / 2f))
        {
            if (m_beats[m_currentBeatIndex].m_first == button)
            {
                OnValidAction?.Invoke();
                m_tokens[m_beats[m_currentBeatIndex]].ValidAction();
                m_beats[m_currentBeatIndex].m_second = BeatState.VALID;
                GizmosHelper.AddSphere(Vector3.zero, 3, Color.green, m_beatDuration / 2);
                if (timeSinceLastBeat < m_inputAcceptanceDuration / 2f)
                {
                    GizmosHelper.AddSphere(Vector3.zero, 4, Color.blue, m_beatDuration / 2);
                } else
                {
                    GizmosHelper.AddSphere(Vector3.zero, 4, Color.yellow, m_beatDuration / 2);
                }
                Debug.Log("Valid");
                FinishCombination(1);
            } else
            {
                OnFailedAction?.Invoke();
                m_tokens[m_beats[m_currentBeatIndex]].FailedAction();
                m_beats[m_currentBeatIndex].m_second = BeatState.FAILED;
                GizmosHelper.AddSphere(Vector3.zero, 3, Color.red, m_beatDuration / 2);
                Debug.Log("Fail");
                FinishCombination(-1);
            }
        } else
        {
            OnPressFailedAction?.Invoke();
            m_tokens[m_beats[m_currentBeatIndex]].FailedPressAction();
            m_beats[m_currentBeatIndex].m_second = BeatState.FAILED;
            GizmosHelper.AddSphere(Vector3.zero, 3, Color.cyan, m_beatDuration / 2);
            FinishCombination(-1);
        }
    }

    public void BeatAction()
    {
        // We check the input after it is not accepted anymore
        new ScaledTimer(m_inputAcceptanceDuration / 2f, null, () => {
            // if state is waiting, trigger event for some kind of "neutral" failed event
            if (m_beats[m_currentBeatIndex].m_second == BeatState.WAITING)
            {
                OnMissedAction?.Invoke();
                GizmosHelper.AddSphere(Vector3.zero, 3, Color.gray, m_beatDuration / 2);
                Debug.Log("Missed");
                FinishCombination(-1);
                m_tokens[m_beats[m_currentBeatIndex]].MissedAction();
            }

            m_currentBeatIndex++;
        }).Start();
        m_timeAtLastBeat = Time.time;
    }

    private void FinishCombination(float points)
    {
        if (m_currentBeatIndex == m_beats.Count - 1)
        {
            Get<GameController>().CombinationFinished(points);
        }
    }
}

public enum BeatState
{
    WAITING, VALID, FAILED
}
