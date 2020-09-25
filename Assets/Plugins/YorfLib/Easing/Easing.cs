using UnityEngine;

namespace YorfLib
{
	public class Easing
	{
		public static float EaseUnclamped(EasingType type, float time)
		{
			return EasingTypeHelper.DELEGATES[(int) type](time);
		}

		public static float Ease(EasingType type, float time)
		{
			return EaseUnclamped(type, Mathf.Clamp01(time));
		}
	}
}
