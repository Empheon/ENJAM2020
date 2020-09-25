using UnityEngine;
using UnityEngine.SceneManagement;
using static YorfLib.SingletonHelper;

namespace YorfLib
{
    public class Singleton<T> where T : Object
    {
        private static T s_instance;

#if UNITY_EDITOR
        private static bool s_permanent;
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Reset()
        {
#if UNITY_EDITOR
            if (!s_permanent)
#endif
            {
                s_instance = null;
            }
        }

        public static void Initialise(T instance)
        {
#if UNITY_EDITOR
            if (s_instance != null)
            {
                if (Application.isPlaying)
                {
                    Debug.LogWarning("Singleton: " + typeof(T).Name + " already initialized!");
                }
            }
#endif
            s_instance = instance;
        }

        public static void InitialiseGeneric(Object instance)
        {
            Initialise((T) instance);
        }

        public static void SetPermanent()
        {
#if UNITY_EDITOR
            s_permanent = true;
#endif
        }

        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (s_instance == null)
                {
                    if (Application.isPlaying)
                    {
                        Debug.LogWarning("Singleton instance " + typeof(T).Name + " is null, searching for it. Fix this to prevent build error");
                    }
                    s_instance = FindComponent();
                }
#endif

                return s_instance;
            }
        }

        public static bool Exists
        {
            get
            {
                return s_instance != null;
            }
        }

#if UNITY_EDITOR

        private static T FindComponent()
        {
#if UNITY_EDITOR
            if (!typeof(T).IsSubclassOf(typeof(Component)))
            {
                T[] resources = Resources.FindObjectsOfTypeAll<T>();
                if(resources.Length > 0)
                {
                    return resources[0];
                }
                else
                {
                    return null;
                }
            }
#endif

            for (int i = 0; i <= SceneManager.sceneCount; i++)
            {
                Scene scene;

                // Retrieve normal scene
                if (i < SceneManager.sceneCount)
                {
                    scene = SceneManager.GetSceneAt(i);
                }
                else // Retrieve dont destroy on load
                {
                    // Don't destroy on load hacky way to get scene
                    GameObject temp = new GameObject();
                    Object.DontDestroyOnLoad(temp);
                    scene = temp.scene;
                    Object.DestroyImmediate(temp);
                    temp = null;
                }

                // Find singleton in scene
                GameObject[] objects = scene.GetRootGameObjects();
                for (int j = 0; j < objects.Length; j++)
                {
                    T[] outValue = objects[j].transform.GetComponentsInChildren<T>(true);
                    if (outValue.Length > 0)
                    {
                        if (outValue.Length > 1)
                        {
                            Debug.LogWarning("Singleton " + typeof(T).Name + " appears multiple times");
                        }
                        return outValue[0];
                    }
                }
            }

            return default(T);
        }

#endif
    }

    public static class SingletonHelper
    {
        public static T Get<T>() where T : Object
        {
            return Singleton<T>.Instance;
        }

        public static void InitSingleton<T>(T instance) where T : Object
        {
            Singleton<T>.Initialise(instance);
        }
		
		public static bool IsSingletonSet<T>() where T : Object
        {
            return Singleton<T>.Exists;
        }
    }
}
