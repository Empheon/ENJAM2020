using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static YorfLib.SingletonHelper;
using YorfLib;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace YorfLib
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : Object
    {
    #if UNITY_EDITOR
        private void Awake()
        {
            List<Object> preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            if(!preloadedAssets.Contains(this))
            {
                Debug.Log($"Adding: {name} to preloaded assets!");
                preloadedAssets.Add(this);
                PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            }

            SetSingleton();
        }
    #endif

        private void OnEnable()
        {
            SetSingleton();
        }

        private void SetSingleton()
        {
            Singleton<T>.InitialiseGeneric(this);
            Singleton<T>.SetPermanent();
        }
    }

#if UNITY_EDITOR
    [InitializeOnLoad]
    public class TouchPreloadedAssets
    {
        static TouchPreloadedAssets()
        {
            // Force unity to load preloaded asset in editor
            GetPreloadedAssets();
        }


        private static Object[] GetPreloadedAssets()
        {
            return PlayerSettings.GetPreloadedAssets();
        }
    }
#endif
}
