using System;
using UnityEngine;

namespace YorfLib
{
	/// <summary>
	/// Represent a fixed update timer
	/// </summary>
	public class FixedUpdateTimer : RawTimer
	{
		public FixedUpdateTimer(float duration, MonoBehaviour owner, Action onEndCallback = null, bool continuous = false) : base(duration, owner, onEndCallback, continuous)
		{
		}

		internal override bool UpdateTimer()
		{
			ElapsedTime += Time.fixedDeltaTime;
			return ElapsedTime >= Duration;
		}
	}
}
