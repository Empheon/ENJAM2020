using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

namespace YorfLib
{
	[InitializeOnLoad]
	public class ToolbarTools
	{
		private static GUIContent s_timeLabel;
		private static GUIContent s_confirmTimeButton;
		private static GUIContent s_playButtonFast;
		private static bool s_timeEditorOpenned;
		private static int s_timeScaleEdit;

		private static class ToolbarStyles
		{
			public static readonly GUIStyle s_boldLabelStyle;
			public static readonly GUIStyle s_smallButtonStyle;
			public static readonly GUIStyle s_buttonStyle;
			public static readonly GUIStyle s_textFieldStyle;

			static ToolbarStyles()
			{
				s_boldLabelStyle = new GUIStyle("BoldLabel")
				{
					fontSize = 12,
					alignment = TextAnchor.MiddleCenter,
					imagePosition = ImagePosition.ImageLeft,
					fontStyle = FontStyle.Bold,
					margin = new RectOffset(2, 0, 2, 0)
				};

				s_smallButtonStyle = new GUIStyle("Command")
				{
					fontSize = 12,
					alignment = TextAnchor.LowerCenter,
					imagePosition = ImagePosition.ImageAbove,
					fontStyle = FontStyle.Bold,
					fixedHeight = 16,
					margin = new RectOffset(2, 0, 0, 0)
				};

				s_buttonStyle = new GUIStyle("Command")
				{
					fontSize = 17,
					alignment = TextAnchor.MiddleCenter,
					imagePosition = ImagePosition.ImageAbove,
					fontStyle = FontStyle.Bold,
					margin = new RectOffset(0, 0, 0, 0),
					padding = new RectOffset(1, 0, 0, 0),
				};

				s_textFieldStyle = new GUIStyle("toolbarTextField")
				{
					margin = new RectOffset(4, 0, 0, 0)
				};
			}
		}

		static ToolbarTools()
		{
			ToolbarExtender.s_rightToolbarGUI.Add(OnToolbarRightGUI);
			ToolbarExtender.s_leftToolbarGUI.Add(OnToolbalLeftGUI);

			EditorApplication.playModeStateChanged += OnPlayModeChanged;
		}

		private static void OnPlayModeChanged(PlayModeStateChange change)
		{
			if(change == PlayModeStateChange.EnteredPlayMode)
			{
				EditorSettings.enterPlayModeOptionsEnabled = false;
			}
		}

		private static void OnToolbalLeftGUI()
		{
			if(s_playButtonFast == null)
			{
				s_playButtonFast = EditorGUIUtility.IconContent("d_PlayButtonProfile On");
				s_playButtonFast.tooltip = "Launch without domain & scene reload";
			}

			GUILayout.FlexibleSpace();
			if(GUILayout.Button(s_playButtonFast, ToolbarStyles.s_buttonStyle))
			{
				if (!EditorApplication.isPlaying)
				{
					EditorSettings.enterPlayModeOptionsEnabled = true;
				}

				EditorApplication.ExecuteMenuItem("Edit/Play");
			}
		}

		private static void OnToolbarRightGUI()
		{
			if (GUILayout.Button("Y", ToolbarStyles.s_buttonStyle))
			{
				YorfLibWindow.Toggle(EditorGUILayout.GetControlRect(false, 0, GUILayout.Width(0)));
			}

			if (s_timeLabel == null)
			{
				s_timeLabel = EditorGUIUtility.IconContent("d_UnityEditor.AnimationWindow");
				s_confirmTimeButton = EditorGUIUtility.IconContent("d_P4_CheckOutRemote");
			}

			GUILayout.BeginVertical();
			GUILayout.Space(s_timeEditorOpenned ? 4 : 2);
			s_timeLabel.text = (int) (Time.timeScale * 100) + "%";
			if (!s_timeEditorOpenned)
			{
				if (GUILayout.Button(s_timeLabel, ToolbarStyles.s_boldLabelStyle))
				{
					s_timeEditorOpenned = true;
					s_timeScaleEdit = (int) Time.timeScale * 100;
				}
			}
			else
			{
				GUILayout.BeginHorizontal();
				s_timeScaleEdit = EditorGUILayout.IntField(s_timeScaleEdit, ToolbarStyles.s_textFieldStyle, GUILayout.Width(50.0f));
				if (GUILayout.Button(s_confirmTimeButton, ToolbarStyles.s_smallButtonStyle))
				{
					s_timeEditorOpenned = false;
					Time.timeScale = Mathf.Clamp(s_timeScaleEdit / 100.0f, 0, 10000);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();

			GUILayout.FlexibleSpace();
		}
	}
}
