using System;
using UnityEngine;
using static YorfLib.SingletonHelper;

namespace YorfLib
{
	public abstract class Timer
	{
		protected bool m_isTimerRegistered;

		/// <summary>
		/// Return current timer progression
		/// </summary>
		public abstract float ProgressionUnclamped();

		/// <summary>
		/// Return current timer progression clamped 0-1
		/// </summary>
		public abstract float Progression();

		/// <summary>
		/// Start timer
		/// </summary>
		/// <returns>It self</returns>
		public virtual Timer Start()
		{
			if (!m_isTimerRegistered)
			{
				Get<TimerManager>().RegisterTimer(this);
				m_isTimerRegistered = true;
			}
			Reset();

			return this;
		}

		/// <summary>
		/// Pause the timer
		/// </summary>
		public virtual void Pause()
		{
			if (m_isTimerRegistered)
			{
                TimerManager timerManager = Get<TimerManager>();
                if (timerManager != null)
                {
                    timerManager.RemoveTimer(this);
                    m_isTimerRegistered = false;
                }
			}
		}

		/// <summary>
		/// Reset the timer
		/// </summary>
		public abstract void Reset();

		/// <summary>
		/// Stop the timer (ie. pause and reset)
		/// </summary>
		public abstract void Stop();

		/// <summary>
		/// Is the timer elapsed ?
		/// </summary>
		public bool IsElapsed()
		{
			return Progression() >= 1.0F;
		}

		internal virtual void OnEnd()
		{
			m_isTimerRegistered = false;
		}

        public virtual bool IsRunning()
        {
            return m_isTimerRegistered;
        }

		internal abstract bool UpdateTimer();

		internal abstract bool CanUpdate();

		internal abstract bool ShallDestroy();

		internal abstract void UpdateOwner(MonoBehaviour owner);

		internal abstract void UpdateEndCallback(Action endAction);

	}
}
