using System;
using System.Collections;
using UnityEngine;

namespace YorfLib
{
	public static class MonoBehaviourExt
	{
		public static Coroutine InvokeEndOfFrame(this MonoBehaviour monoBehaviour, Action onEnd)
		{
			return monoBehaviour.StartCoroutine(WaitEndOfFrameRoutine(onEnd));
		}

		private static IEnumerator WaitEndOfFrameRoutine(Action onEnd)
		{
			yield return new WaitForEndOfFrame();
			onEnd();
		}

		public static Coroutine InvokeAfter(this MonoBehaviour monoBehaviour, float time, Action onEnd)
		{
			return monoBehaviour.StartCoroutine(WaitForTime(time, onEnd));
		}

		private static IEnumerator WaitForTime(float time, Action onEnd)
		{
			yield return new WaitForSeconds(time);
			onEnd();
		}

		public static Coroutine InvokeNextFrame(this MonoBehaviour monoBehaviour, Action onEnd)
		{
			return monoBehaviour.StartCoroutine(InvokeNextFrame(onEnd));
		}

		private static IEnumerator InvokeNextFrame(Action onEnd)
		{
			yield return null;
			onEnd();
		}
	}
}
