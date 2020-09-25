using System;
using UnityEngine;

namespace YorfLib
{
	public abstract class RawTimer : Timer
	{
		/// <summary>
		/// Handle elapsed time. Timer will not potentially end until next update
		/// </summary>
		public float ElapsedTime { get; set; }

		/// <summary>
		/// Handle duration. Timer will not potentially end until next update
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// If true, if the ower is disabled the timer will continue
		/// </summary>
		public bool Continuous { get; set; }

		private MonoBehaviour m_owner;

		private Action m_onEndCallback;

		public RawTimer(float duration, MonoBehaviour owner, Action onEndCallback = null, bool continuous = false)
		{
			Duration = duration;
			this.m_owner = owner;
			this.m_onEndCallback = onEndCallback;
			Continuous = continuous;
		}

		/// <summary>
		/// Return current timer progression
		/// </summary>
		public override float ProgressionUnclamped()
		{
			return ElapsedTime / Duration;
		}

		/// <summary>
		/// Return current timer progression clamped 0-1
		/// </summary>
		public override float Progression()
		{
			return Mathf.Clamp01(ProgressionUnclamped());
		}

		/// <summary>
		/// Reset the timer
		/// </summary>
		public override void Reset()
		{
			ElapsedTime = 0;
		}

		/// <summary>
		/// Stop the timer (ie. pause and reset)
		/// </summary>
		public override void Stop()
		{
			Pause();
			Reset();
		}

		internal override void OnEnd()
		{
			base.OnEnd();

			if (m_onEndCallback != null)
			{
				m_onEndCallback();
			}
		}

		internal override abstract bool UpdateTimer();

		internal override bool CanUpdate()
		{
			return m_owner == null || Continuous || m_owner.isActiveAndEnabled;
		}

		internal override bool ShallDestroy()
		{
			bool isNull = ReferenceEquals(m_owner, null);
			return !isNull && m_owner == null; // Destroy if the object is not null & destroyed
		}

		internal override void UpdateOwner(MonoBehaviour owner)
		{
			this.m_owner = owner;
		}

		internal override void UpdateEndCallback(Action endAction)
		{
			m_onEndCallback = endAction;
		}
	}
}
