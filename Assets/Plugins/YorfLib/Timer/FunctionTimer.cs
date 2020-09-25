using System;
using UnityEngine;

namespace YorfLib
{
	public class FunctionTimer : Timer
	{
		public Timer InternalTimer { get; private set; }
		private Action<Timer> m_updateCallback;

		public FunctionTimer(Timer timer, Action<Timer> updateCallback)
		{
			InternalTimer = timer;
			this.m_updateCallback = updateCallback;
		}

		public override float ProgressionUnclamped()
		{
			return InternalTimer.ProgressionUnclamped();
		}

		public override float Progression()
		{
			return InternalTimer.Progression();
		}

		public override void Reset()
		{
			InternalTimer.Reset();
		}

		public override void Stop()
		{
			Pause();
			InternalTimer.Reset();
		}

		internal override bool UpdateTimer()
		{
			bool finished = InternalTimer.UpdateTimer();
			m_updateCallback(InternalTimer);

			return finished;
		}

		internal override void OnEnd()
		{
			base.OnEnd();
			InternalTimer.OnEnd();
		}

		internal override bool CanUpdate()
		{
			return InternalTimer.CanUpdate();
		}

		internal override bool ShallDestroy()
		{
			return InternalTimer.ShallDestroy();
		}

		internal override void UpdateOwner(MonoBehaviour owner)
		{
			InternalTimer.UpdateOwner(owner);
		}

		internal override void UpdateEndCallback(Action endAction)
		{
			InternalTimer.UpdateEndCallback(endAction);
		}
    }
}
