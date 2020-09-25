using System;
using UnityEngine;

namespace YorfLib
{
	public class EasingTimer : Timer
	{
		public Timer InternalTimer { get; private set; }
		private EasingType m_easingType;

		public EasingTimer(Timer timer, EasingType easingType)
		{
			InternalTimer = timer;
			this.m_easingType = easingType;
		}

		public override float ProgressionUnclamped()
		{
			return Easing.EaseUnclamped(m_easingType, InternalTimer.ProgressionUnclamped());
		}

		public override float Progression()
		{
			return Easing.Ease(m_easingType, InternalTimer.Progression());
		}

		public float ProgressionRemapped(float min, float max)
		{
			float progression = Progression();
			return (max - min) * progression + min;
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
			return InternalTimer.UpdateTimer();
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
