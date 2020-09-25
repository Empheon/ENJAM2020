using System;
using UnityEngine;

namespace YorfLib
{
	/// <summary>
	/// Represent a realtime timer
	/// </summary>
	public class UnscaledTimer : RawTimer
	{
		public UnscaledTimer(float duration, MonoBehaviour owner, Action onEndCallback = null, bool continuous = false) : base(duration, owner, onEndCallback, continuous)
		{
		}

		internal override bool UpdateTimer()
		{
			ElapsedTime += Time.unscaledDeltaTime;
			return ElapsedTime >= Duration;
		}
	}
}
