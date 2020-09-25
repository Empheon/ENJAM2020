using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YorfLib
{
	public class FirebaseEditor : IYorfLibWindowTab
	{
		private string m_searchString = "";
		private Vector2 m_scrollPos;

		public void OnEnable() { }

		public void OnGUI()
		{
			m_searchString = YEditorUtil.SearchField(m_searchString).ToLower();

			YEditorUtil.DrawBackgroundRect(YEditorUtil.GetHighLightColor(), 20);
			GUILayout.BeginHorizontal();
			GUILayout.Label("", GUILayout.Width(16));
			GUILayout.Label("Event Name", GUILayout.MaxWidth(150));
			GUILayout.Label("Time");
			GUILayout.EndHorizontal();

			m_scrollPos = GUILayout.BeginScrollView(m_scrollPos);

			int i = 0;
			foreach (Analytics.FirebaseDebugEvent debugEvent in Analytics.FirebaseEvents)
			{
				bool isUserEvent = debugEvent is Analytics.FirebaseUserProperties;
				string rowName = isUserEvent ? "Set UP: " + debugEvent.Name : debugEvent.Name;

				if (m_searchString.Length == 0 || rowName.ToLower().Contains(m_searchString))
				{
					YEditorUtil.DrawBackgroundRect(YEditorUtil.GetTableBackgroundColor(i % 2 == 0), 20);		

					Rect r = EditorGUILayout.GetControlRect(false, -3);
					r.x += 2;
					r.width = 16;
					r.height = 16;

					debugEvent.Open = EditorGUI.Foldout(r, debugEvent.Open, "");

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("", GUILayout.Width(16));
					EditorGUILayout.LabelField(rowName, GUILayout.MaxWidth(150));
					EditorGUILayout.LabelField(debugEvent.Time.ToShortTimeString());
					EditorGUILayout.EndHorizontal();


					if (debugEvent.Open)
					{
						EditorGUI.indentLevel++;
						if (isUserEvent)
						{
							YEditorUtil.DrawBackgroundRect(YEditorUtil.GetTableBackgroundColor(i % 2 == 0), 24);
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Value");
							EditorGUILayout.LabelField(((Analytics.FirebaseUserProperties) debugEvent).Value);
							EditorGUILayout.EndHorizontal();
						}
						else
						{
							Analytics.FirebaseEvent fbEvent = (Analytics.FirebaseEvent) debugEvent;
							YEditorUtil.DrawBackgroundRect(YEditorUtil.GetTableBackgroundColor(i % 2 == 0), fbEvent.Parameters.Length * 20 + 4);

							foreach(Pair parameter in fbEvent.Parameters)
							{
								EditorGUILayout.BeginHorizontal();
								EditorGUILayout.LabelField(parameter.m_first.ToString());
								EditorGUILayout.LabelField(parameter.m_second.ToString());
								EditorGUILayout.EndHorizontal();
							}
						}

						EditorGUI.indentLevel--;
					}

					i++;
				}
			}

			GUILayout.EndScrollView();
		}

		public string GetTabName()
		{
			return "Firebase";
		}
	}
}
