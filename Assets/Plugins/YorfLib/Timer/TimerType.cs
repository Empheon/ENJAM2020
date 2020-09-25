using System;
using UnityEngine;

namespace YorfLib
{
	public enum RawTimerType
	{
		SCALED,
		UNSCALED,
		FIXED
	}

	public class RawTimerHelper
	{
		private delegate RawTimer TimerMaker(float duration, MonoBehaviour owner, Action endCallback, bool continuous);

		private static readonly TimerMaker[] s_rawTimerFactories =
		{
			CreateScaledTimer,
			CreateUnscaledTimer,
			CreateFixedTimer
		};

		private static RawTimer CreateScaledTimer(float duration, MonoBehaviour owner, Action endCallback, bool continuous)
		{
			return new ScaledTimer(duration, owner, endCallback);
		}

		private static RawTimer CreateUnscaledTimer(float duration, MonoBehaviour owner, Action endCallback, bool continuous)
		{
			return new UnscaledTimer(duration, owner, endCallback);
		}

		private static RawTimer CreateFixedTimer(float duration, MonoBehaviour owner, Action endCallback, bool continuous)
		{
			return new FixedUpdateTimer(duration, owner, endCallback);
		}

		public static RawTimer InstantiateTimer(RawTimerType type, float duration, MonoBehaviour owner, Action endCallback = null, bool continuous = false)
		{
			return s_rawTimerFactories[(int) type](duration, owner, endCallback, continuous);
		}
	}
}
