using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	[InitializeOnLoad]
	public static class ToolbarExtender
	{
		private static int s_toolCount = -1;
		private static GUIStyle s_commandStyle = null;

		public static readonly List<Action> s_leftToolbarGUI = new List<Action>();
		public static readonly List<Action> s_rightToolbarGUI = new List<Action>();

		static ToolbarExtender()
		{
			ToolbarCallback.OnToolbarGUI -= OnGUI;
			ToolbarCallback.OnToolbarGUI += OnGUI;
		}

		private static void OnGUI()
		{
			if (s_toolCount == -1)
			{
				Type toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
				FieldInfo toolIcons = toolbarType.GetField("s_ShownToolIcons", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

				GUIContent[] icons = (GUIContent[]) toolIcons.GetValue(null);
				s_toolCount = icons == null ? -1 : icons.Length;
			}

			// Create two containers, left and right
			// Screen is whole toolbar

			if (s_commandStyle == null)
			{
				s_commandStyle = new GUIStyle("CommandLeft");
			}

			var screenWidth = EditorGUIUtility.currentViewWidth;

			// Following calculations match code reflected from Toolbar.OldOnGUI()
			float playButtonsPosition = (screenWidth - 100) / 2;

			Rect leftRect = new Rect(0, 0, screenWidth, Screen.height);
			leftRect.xMin += 10; // Spacing left
			leftRect.xMin += 32 * s_toolCount; // Tool buttons
			leftRect.xMin += 40; // Spacing between tools and pivot
			leftRect.xMin += 64 * 2; // Pivot buttons
			leftRect.xMax = playButtonsPosition - 20;

			Rect rightRect = new Rect(0, 0, screenWidth, Screen.height);
			rightRect.xMin = playButtonsPosition;
			rightRect.xMin += s_commandStyle.fixedWidth * 3; // Play buttons
			rightRect.xMax = screenWidth;
			rightRect.xMax -= 10; // Spacing right
			rightRect.xMax -= 80; // Layout
			rightRect.xMax -= 10; // Spacing between layout and layers
			rightRect.xMax -= 80; // Layers
			rightRect.xMax -= 20; // Spacing between layers and account
			rightRect.xMax -= 80; // Account
			rightRect.xMax -= 10; // Spacing between account and cloud
			rightRect.xMax -= 32; // Cloud
			rightRect.xMax -= 10; // Spacing between cloud and collab
			rightRect.xMax -= 78; // Colab

			// Add spacing around existing controls
			leftRect.xMin += 10;
			leftRect.xMax -= 10;
			rightRect.xMin += 10;
			rightRect.xMax -= 10;

			// Add top and bottom margins
			leftRect.y = 5;
			leftRect.height = 24;
			rightRect.y = 5;
			rightRect.height = 24;

			if (leftRect.width > 0)
			{
				GUILayout.BeginArea(leftRect);
				GUILayout.BeginHorizontal();
				foreach (var handler in s_leftToolbarGUI)
				{
					handler();
				}

				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}

			if (rightRect.width > 0)
			{
				GUILayout.BeginArea(rightRect);
				GUILayout.BeginHorizontal();
				foreach (var handler in s_rightToolbarGUI)
				{
					handler();
				}

				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}
		}
	}
}
