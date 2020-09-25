using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;

namespace YorfLib
{
	public class PerformanceDebugger : MonoBehaviour
	{
		private float m_minFPS;
		private float m_avgFPS;
		private float m_maxFPS;
		private string m_lastMinFPS;
		private string m_lastAvgFPS;
		private string m_lastMaxFPS;
		private int m_currentFPSIndex;

		public void Awake()
		{
			Application.targetFrameRate = Get<YorfLibSettings>().m_targetFPS;
		}

		public void Update()
		{
			if (!Get<YorfLibSettings>().m_displayPerformance)
			{
				enabled = false;
				return;
			}

			if (m_currentFPSIndex == 0)
			{
				m_lastMinFPS = m_minFPS.ToString("n2");
				m_lastMaxFPS = m_maxFPS.ToString("n2");
				m_lastAvgFPS = m_avgFPS.ToString("n2");
				m_minFPS = float.PositiveInfinity;
				m_maxFPS = float.NegativeInfinity;
				m_avgFPS = 0.0f;
			}

			float fps = 1.0f / Time.deltaTime;
			m_minFPS = Mathf.Min(m_minFPS, fps);
			m_maxFPS = Mathf.Max(m_maxFPS, fps);
			m_avgFPS += fps / Application.targetFrameRate;

			m_currentFPSIndex = (int) Mathf.Repeat(m_currentFPSIndex + 1, Application.targetFrameRate);
		}

		public void OnGUI()
		{
			if(!Get<YorfLibSettings>().m_displayPerformance)
			{
				return;
			}

			Rect rect = new Rect(0.0f, 0.0f, 100.0f, 24.0f);
			GUI.Label(rect, m_lastAvgFPS);

			rect.y += 16.0f;
			GUI.Label(rect, m_lastMinFPS);

			rect.y += 16.0f;
			GUI.Label(rect, m_lastMaxFPS);
		}
	}
}
