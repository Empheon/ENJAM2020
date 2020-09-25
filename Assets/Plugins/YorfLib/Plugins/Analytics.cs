using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Analytics;
using System;

namespace YorfLib
{
	public class Analytics
	{
#if UNITY_EDITOR
		private static List<FirebaseDebugEvent> s_firebaseEvents = new List<FirebaseDebugEvent>();
		public static List<FirebaseDebugEvent> FirebaseEvents { get { return s_firebaseEvents; } }

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		static void Reset()
		{
			s_firebaseEvents.Clear();
		}
#endif

		public static void LogEvent(string name, params Pair[] parameters)
		{
#if UNITY_EDITOR
			s_firebaseEvents.Add(new FirebaseEvent(DateTime.Now, name, parameters));
#endif

			/*Parameter[] fbParameters = new Parameter[parameters.Length];
			for(int i = 0; i < fbParameters.Length; i++)
			{
				fbParameters[i] = new Parameter(parameters[0].m_first.ToString(), parameters[0].m_second.ToString());
			}

			FirebaseAnalytics.LogEvent(name, fbParameters);*/
		}

		public static void SetUserProperty(string name, string value)
		{
#if UNITY_EDITOR
			s_firebaseEvents.Add(new FirebaseUserProperties(DateTime.Now, name, value));
#endif
			//FirebaseAnalytics.SetUserProperty(name, value);
		}

#if UNITY_EDITOR
		public class FirebaseDebugEvent
		{
			public DateTime Time { get; }
			public string Name { get; }
			public bool Open { get; set; }

			public FirebaseDebugEvent(DateTime time, string name)
			{
				Time = time;
				Name = name;
			}
		}

		public class FirebaseEvent : FirebaseDebugEvent
		{
			public Pair[] Parameters { get; }

			public FirebaseEvent(DateTime time, string name, params Pair[] parameters) : base(time, name)
			{
				Parameters = parameters;
			}
		}

		public class FirebaseUserProperties : FirebaseDebugEvent
		{
			public string Value { get; }

			public FirebaseUserProperties(DateTime time, string name, string value) : base(time, name)
			{
				Value = value;
			}
		}
#endif
	}
}
