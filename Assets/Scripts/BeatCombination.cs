using System;
using System.Collections.Generic;
using UnityEngine;
using YorfLib;

public class BeatCombination
{
    public Action OnValidAction;
    public Action OnFailedAction;
    public Action OnMissedAction;

    private List<Pair<ButtonType, BeatState>> m_beats;
    private float m_beatDuration;
    private float m_inputAcceptanceDuration;

    private float m_timeAtLastBeat;

    private int m_currentBeatIndex;

    public BeatCombination(List<ButtonType> inputs)
    {
        m_beats = new List<Pair<ButtonType, BeatState>>();
        foreach (ButtonType bt in inputs)
        {
            m_beats.Add(new Pair<ButtonType, BeatState>(bt, BeatState.WAITING));
        }
    }

    public void Init(float beatDuration)
    {
        m_beatDuration = beatDuration;

        // We set a minimum duration to allow the player to react
        m_inputAcceptanceDuration = Mathf.Max(beatDuration / 3f, 0.1f);
    }

    public void ReceiveInput(ButtonType button)
    {
        float timeSinceLastBeat = Time.time - m_timeAtLastBeat;
        if (timeSinceLastBeat < m_inputAcceptanceDuration / 2f ||
            timeSinceLastBeat > m_beatDuration - (m_inputAcceptanceDuration / 2f))
        {
            if (m_beats[m_currentBeatIndex].m_first == button)
            {
                OnValidAction?.Invoke();
                Debug.Log("Valid");
            } else
            {
                OnFailedAction?.Invoke();
                Debug.Log("Fail");
            }
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
                Debug.Log("Missed");
            }

            m_currentBeatIndex++;
        }).Start();

        m_timeAtLastBeat = Time.time;
    }
}

public enum BeatState
{
    WAITING, VALID, FAILED
}
