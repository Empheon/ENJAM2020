using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace YorfLib
{
    public class ScriptableObjectCreator : EditorWindow
    {
        private static System.Type[] s_allScriptableObjects;
        private static string[] s_allScriptableObjectsName;
        private static int[] s_nameToType;
        private static int s_selectedType = 0;
        private static string s_filter;

        [MenuItem("Assets/Create/ScriptableObject")]
        static void Init()
        {
            s_allScriptableObjects = GetAllDerivedTypes(System.AppDomain.CurrentDomain, typeof(ScriptableObject)).Where(type => !type.IsSubclassOf(typeof(EditorWindow)) && !type.IsAbstract).ToArray();
            RecreateStringList();

            ScriptableObjectCreator window = CreateInstance<ScriptableObjectCreator>();

            window.position = new Rect(0, 0, 250, 115);
            window.CenterOnMainWin();

            window.ShowPopup();
        }

        void OnGUI()
        {
            if(s_allScriptableObjectsName == null)
            {
                Close();
                return;
            }

            GUILayout.Label("Create New Scriptable Object:", EditorStyles.boldLabel);
            string newFilter = GUILayout.TextField(s_filter, EditorStyles.toolbarSearchField);
            if(newFilter != s_filter)
            {
                s_selectedType = 0;
                s_filter = newFilter;
                RecreateStringList();
            }

            if (s_allScriptableObjectsName.Length > 0)
            {
                GUIContent label = new GUIContent("Class");
                var labelSize = GUI.skin.label.CalcSize(label);
                EditorGUIUtility.labelWidth = labelSize.x;
                s_selectedType = EditorGUILayout.Popup(label, s_selectedType, s_allScriptableObjectsName);

                GUILayout.Space(10);

                if (GUILayout.Button("Create"))
                {
                    string className = s_allScriptableObjectsName[s_selectedType];
                    string path = EditorUtility.SaveFilePanelInProject($"Save {className}", $"New {className}", "asset", string.Empty);
                    if (string.IsNullOrEmpty(path))
                    {
                        return;
                    }

                    AssetDatabase.CreateAsset(CreateInstance(s_allScriptableObjects[s_nameToType[s_selectedType]]), path);
                    Close();
                }
            }
            else
            {
                GUILayout.Label("No class found");
            }

            if (GUILayout.Button("Cancel"))
            {
                Close();
            }
        }

        private static void RecreateStringList()
        {
            int maxLength = Mathf.Min(s_allScriptableObjects.Length, 10);
            s_allScriptableObjectsName = new string[maxLength];
            s_nameToType = new int[maxLength];

            int writeIndex = 0;
            for (int i = 0; i < s_allScriptableObjects.Length && writeIndex < maxLength; i++)
            {
                string name = s_allScriptableObjects[i].Name;
                if (s_filter == null || s_filter.Length == 0 || name.StartsWith(s_filter, System.StringComparison.OrdinalIgnoreCase))
                {
                    s_nameToType[writeIndex] = i;
                    s_allScriptableObjectsName[writeIndex] = name;
                    writeIndex++;
                }
            }

            if (writeIndex == 0)
            {
                s_allScriptableObjectsName = new string[0];
                s_nameToType = new int[0];
            }
            else
            {
                System.Array.Resize(ref s_allScriptableObjectsName, writeIndex);
                System.Array.Resize(ref s_nameToType, writeIndex);
            }
        }

        public static System.Type[] GetAllDerivedTypes(System.AppDomain aAppDomain, System.Type aType)
        {
            var result = new List<System.Type>();
            var assemblies = aAppDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(aType))
                        result.Add(type);
                }
            }
            return result.ToArray();
        }

        public static Rect GetEditorMainWindowPos()
        {
            var containerWinType = GetAllDerivedTypes(System.AppDomain.CurrentDomain, (typeof(ScriptableObject))).Where(t => t.Name == "ContainerWindow").FirstOrDefault();
            if (containerWinType == null)
                throw new System.MissingMemberException("Can't find internal type ContainerWindow. Maybe something has changed inside Unity");
            var showModeField = containerWinType.GetField("m_ShowMode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var positionProperty = containerWinType.GetProperty("position", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (showModeField == null || positionProperty == null)
                throw new System.MissingFieldException("Can't find internal fields 'm_ShowMode' or 'position'. Maybe something has changed inside Unity");
            var windows = Resources.FindObjectsOfTypeAll(containerWinType);
            foreach (var win in windows)
            {
                var showmode = (int)showModeField.GetValue(win);
                if (showmode == 4) // main window
                {
                    var pos = (Rect)positionProperty.GetValue(win, null);
                    return pos;
                }
            }
            throw new System.NotSupportedException("Can't find internal main window. Maybe something has changed inside Unity");
        }

        public void CenterOnMainWin()
        {
            var main = GetEditorMainWindowPos();
            var pos = position;
            float w = (main.width - pos.width) * 0.5f;
            float h = (main.height - pos.height) * 0.5f;
            pos.x = main.x + w;
            pos.y = main.y + h;
            position = pos;
        }
    }
}
