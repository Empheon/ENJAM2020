using System.Collections.Generic;
using UnityEngine;

namespace YorfLib
{
	internal class TimerManager : MonoBehaviour
	{
		// Contains all active timers
		private LinkedList<Timer> m_activeTimers = new LinkedList<Timer>();

		// Add a timer
		internal void RegisterTimer(Timer timer)
		{
			m_activeTimers.AddFirst(timer);
		}

		// Remove a timer
		internal void RemoveTimer(Timer timer)
		{
			m_activeTimers.Remove(timer);
		}

		private void Update()
		{
			// Update all timers
			var node = m_activeTimers.First;

			while (node != null)
			{
				if (node.Value == null || node.Value.ShallDestroy()) // The timer has been destroyed / should destroy due to destroyed owner
				{
					var nextNode = node.Next;
					m_activeTimers.Remove(node);
					node = nextNode;
                    continue;
				}
				else if (node.Value.CanUpdate()) // The timer exists and can update due to active owner (or missing one)
				{
					bool finished = UpdateTimer(node.Value);
					if (finished)
					{
						node.Value.OnEnd();

						var nextNode = node.Next;
						m_activeTimers.Remove(node);
						node = nextNode;
						continue;
					}
				}
				node = node.Next;
			}

			float timeValue = Time.deltaTime;
		}

		/// <summary>
		/// Update a timer
		/// </summary>
		/// <returns>True if timer has finished</returns>
		private bool UpdateTimer(Timer timer)
		{
			return timer.UpdateTimer();
		}
	}
}
