using UnityEngine;

namespace YorfLib
{
	public class PerformanceReporter : MonoBehaviour
	{
		private int m_frameCount;
		private float m_startTime;
		private bool m_started;

		public void StartRecording()
		{
			m_frameCount = 0;
			m_startTime = Time.time;
			m_started = true;
		}

		public Report StopRecording()
		{
			m_started = false;

			float duration = (Time.time - m_startTime);
			return new Report(duration, m_frameCount / duration);
		}

		public bool IsRecording()
		{
			return m_started;
		}

		private void Update()
		{
			if (m_started)
			{
				m_frameCount++;
			}
		}

		public struct Report
		{
			public float duration;
			public float avgFPS;

			public Report(float duration, float avgFPS)
			{
				this.duration = duration;
				this.avgFPS = avgFPS;
			}
		}
	}
}
