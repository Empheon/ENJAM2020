using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;

namespace YorfLib
{
    public class SlowmoManager : MonoBehaviour
    {
        private Tweener m_previousTweener;
        private int m_lastSlowmoPriority = int.MinValue;

        private void Awake()
        {
            InitSingleton(this);
        }

        public void SlowMo(float targetTimescale, float attack, float sustain, float decay, int priority = 0)
        {
            if (priority < m_lastSlowmoPriority)
            {
                return;
            }

            m_lastSlowmoPriority = priority;

            if (m_previousTweener != null)
            {
                DOTween.Kill(m_previousTweener);
            }

            m_previousTweener = DOTween.To(() => Time.timeScale, (x) => Time.timeScale = x, targetTimescale, attack).SetUpdate(true).SetEase(Ease.OutCubic);
            m_previousTweener.OnComplete(() => m_previousTweener = DOTween.To(() => Time.timeScale, (x) => Time.timeScale = x, 1.0f, decay).SetDelay(sustain).SetUpdate(true).SetEase(Ease.InCubic).OnComplete(() => m_lastSlowmoPriority = int.MinValue));
        }
    }
}
