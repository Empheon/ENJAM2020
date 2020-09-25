using UnityEngine;
using System.Collections.Generic;
using static YorfLib.SingletonHelper;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace YorfLib
{
	public class GizmosHelper : MonoBehaviour
	{
#if UNITY_EDITOR
		private List<GizmosInstance> m_gizmosInstances = new List<GizmosInstance>();
#endif

		public static void AddSphere(Vector3 position, float radius, Color color, float duration = 0.001F)
		{
#if UNITY_EDITOR
			Get<GizmosHelper>().m_gizmosInstances.Add(new SphereGizmos(color, position, radius, duration));
#endif
		}

        public static void AddBox(Vector3 position, Vector3 size, Color color, float duration = 0.001F)
        {
#if UNITY_EDITOR
            Get<GizmosHelper>().m_gizmosInstances.Add(new BoxGizmos(color, position, size, duration));
#endif
        }

        public static void AddText(Vector3 position, string text, Color color, float duration = 0.001F)
		{
#if UNITY_EDITOR
			Get<GizmosHelper>().m_gizmosInstances.Add(new TextGizmos(color, position, text, duration));
#endif
		}

#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			for (int i = m_gizmosInstances.Count - 1; i >= 0; i--)
			{
				// Gizmos is finished
				if (m_gizmosInstances[i].timer.IsElapsed())
				{
					m_gizmosInstances.RemoveAt(i);
				}
				else
				{
					m_gizmosInstances[i].DrawGizmos();
				}
			}
		}

		/// <summary>
		/// Base class for all gizmos
		/// </summary>
		internal abstract class GizmosInstance
		{
			internal Timer timer;

			protected Color color;
			protected Vector3 position;

			internal GizmosInstance(Color color, Vector3 position, float duration)
			{
				this.color = color;
				this.position = position;
				timer = new ScaledTimer(duration, Get<GizmosHelper>());
				timer.Start();
			}

			internal virtual void DrawGizmos()
			{
				Gizmos.color = color;
			}
		}

		/// <summary>
		/// Sphere gizmos
		/// </summary>
		internal class SphereGizmos : GizmosInstance
		{
			protected float radius;

			public SphereGizmos(Color color, Vector3 position, float radius, float duration) : base(color, position, duration)
			{
				this.radius = radius;
			}

			internal override void DrawGizmos()
			{
				base.DrawGizmos();
				Gizmos.DrawWireSphere(position, radius);
			}
		}

        /// <summary>
        /// Sphere gizmos
        /// </summary>
        internal class BoxGizmos : GizmosInstance
        {
            protected Vector3 size;

            public BoxGizmos(Color color, Vector3 position, Vector3 size, float duration) : base(color, position, duration)
            {
                this.size = size;
            }

            internal override void DrawGizmos()
            {
                base.DrawGizmos();
                Gizmos.DrawWireCube(position, size);
            }
        }

        /// <summary>
        /// Text gizmos
        /// </summary>
        internal class TextGizmos : GizmosInstance
		{
			protected string text;
			protected GUIStyle style;

			public TextGizmos(Color color, Vector3 position, string text, float duration) : base(color, position, duration)
			{
				this.text = text;
				style = new GUIStyle(EditorStyles.boldLabel);
				style.normal.textColor = color;
				style.alignment = TextAnchor.MiddleCenter;
			}

			internal override void DrawGizmos()
			{
				base.DrawGizmos();
				Handles.BeginGUI();
				Handles.Label(position, text, style);
				Handles.EndGUI();
			}
		}

#endif
	}
}
