using System;
using UnityEditor;
using UnityEngine;

namespace YorfLib
{
	public class I18nEditor : IYorfLibWindowTab
	{
		private SystemLanguage editorLanguage;

		public void OnEnable()
		{
			editorLanguage = (SystemLanguage) EditorPrefs.GetInt("i18n_lang", (int) SystemLanguage.English);
		}

		public void OnGUI()
		{
			SystemLanguage newLanguage = (SystemLanguage) EditorGUILayout.EnumPopup("Language", editorLanguage);
			if (newLanguage != editorLanguage)
			{
				EditorPrefs.SetInt("i18n_lang", (int) newLanguage);
				ReloadLang();

				editorLanguage = newLanguage;
			}

			if (GUILayout.Button("Reload lang"))
			{
				ReloadLang();
			}
		}

		private static void ReloadLang()
		{
			I18n.LoadLanguage();
			Array.ForEach(Resources.FindObjectsOfTypeAll<TranslateTMP>(), x => x.Refresh());
		}

		public string GetTabName()
		{
			return "Localization";
		}
	}
}
