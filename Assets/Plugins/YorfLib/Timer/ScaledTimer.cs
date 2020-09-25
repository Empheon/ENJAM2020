using System;
using UnityEngine;

namespace YorfLib
{
	/// <summary>
	/// Represent a scaled timer with Time.timeScale
	/// </summary>
	public class ScaledTimer : RawTimer
	{
		public ScaledTimer(float duration, MonoBehaviour owner, Action onEndCallback = null, bool continuous = false) : base(duration, owner, onEndCallback, continuous)
		{
		}

		internal override bool UpdateTimer()
		{
			ElapsedTime += Time.deltaTime;
			return ElapsedTime >= Duration;
		}
	}
}
