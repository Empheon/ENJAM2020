using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YorfLib
{
	public class YorfLibWindow : EditorWindow
	{
		private static YorfLibWindow s_instance;
		private bool requireClose;

		private IYorfLibWindowTab[] tabs;
		private string[] tabNames;
		private int currentTab;
		private Vector2 scrollPosition;

		public static void Toggle(Rect rect)
		{
			if (s_instance == null)
			{
				s_instance = GetWindow<YorfLibWindow>();
				s_instance.Init(rect);
			}
			else
			{
				s_instance.Close();
			}
		}

		private void Init(Rect rect)
		{
			Vector2 startPos = GUIUtility.GUIToScreenPoint(rect.min);
			float windowWidth = 500;

			// Check window is not open using a hacky way
			if (s_instance.position.x == 0)
			{
				position = new Rect(startPos.x + 16 - windowWidth * 0.5f, startPos.y + 26, windowWidth, 300);

				requireClose = false;
				ShowPopup();
				Focus();
			}
			else
			{
				Debug.Log("Closing");
				s_instance.position = new Rect(0, 0, 1, 1);
				s_instance.Close();
			}
		}

		private void OnEnable()
		{
			s_instance = this;

			Reload();
		}

		public void OnGUI()
		{
			if (requireClose)
			{
				Close();
				return;
			}

			currentTab = GUILayout.Toolbar(currentTab, tabNames);
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
			tabs[currentTab].OnGUI();
			EditorGUILayout.EndScrollView();
		}

		private void Reload()
		{
			Type[] supportedTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IYorfLibWindowTab).IsAssignableFrom(t) && t != typeof(IYorfLibWindowTab)).ToArray();
			tabs = new IYorfLibWindowTab[supportedTypes.Length];
			tabNames = new string[tabs.Length];
			for (int i = 0; i < tabs.Length; i++)
			{
				tabs[i] = (IYorfLibWindowTab) Activator.CreateInstance(supportedTypes[i]);
				tabs[i].OnEnable();
			}

			RefreshTabNames();
		}

		private void RefreshTabNames()
		{
			for (int i = 0; i < tabs.Length; i++)
			{
				tabNames[i] = tabs[i].GetTabName();
			}
		}
	}
}
