using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
public class CheatManager : MonoBehaviour
{
    private static List<ICheatManager> s_cheatManagers = new List<ICheatManager>();

    private bool m_cheatManagerOpen;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void OnStart()
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        s_cheatManagers.Clear();
        foreach (Assembly assembly in assemblies)
        {
            Type[] supportedTypes = assembly.GetTypes().Where(t => typeof(ICheatManager).IsAssignableFrom(t) && t != typeof(ICheatManager)).ToArray();
            for (int i = 0; i < supportedTypes.Length; i++)
            {
                s_cheatManagers.Add((ICheatManager)Activator.CreateInstance(supportedTypes[i]));
            }
        }

        DontDestroyOnLoad(new GameObject("CheatManager", typeof(CheatManager)));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.touchCount == 4)
        {
            m_cheatManagerOpen = true;
        }
    }

    private void OnGUI()
    {
        if (m_cheatManagerOpen)
        {
            GUILayout.BeginVertical("", "box", GUILayout.MinWidth(200), GUILayout.MaxWidth(600));

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Close Cheat Manager"))
            {
                m_cheatManagerOpen = false;
            }
            GUILayout.EndHorizontal();

            foreach (ICheatManager cm in s_cheatManagers)
            {
                cm.OnCheatGUI();
            }

            GUILayout.EndVertical();
        }
    }
}

public interface ICheatManager
{
    void OnCheatGUI();
}

#endif
