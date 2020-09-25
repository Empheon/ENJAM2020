using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;

namespace YorfLib
{
    public struct PoolObjectQueue
    {
        public bool isCircularPool;
        public bool enabledAsDefault;
        public Queue<Object> queue;
    }

    [System.Serializable]
	public static class MasterPool
	{
        private static Dictionary<Object, PoolObjectQueue> m_objectQueues = new Dictionary<Object, PoolObjectQueue>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Reset()
        {
            m_objectQueues.Clear();
        }

        public static void RegisterObject(Object obj, bool isCircular, int quantity, bool enabledAsDefault = false)
		{
            if(m_objectQueues.ContainsKey(obj))
            {
                return;
            }

            Queue<Object> objQueue = new Queue<Object>(quantity);
            PoolObjectQueue poq = new PoolObjectQueue
            {
                isCircularPool = isCircular,
                enabledAsDefault = enabledAsDefault,
                queue = objQueue
            };

            for (int i = 0; i < quantity; i++)
			{
                AddNewObject(obj, objQueue, enabledAsDefault);
			}

            m_objectQueues.Add(obj, poq);
		}

        private static void AddNewObject(Object obj, Queue<Object> objQueue, bool enabledAsDefault)
        {
            Object newObject = Object.Instantiate(obj);

            if (newObject is GameObject)
            {
                ((GameObject) newObject).SetActive(enabledAsDefault);
            }
            else if(newObject is Component)
            {
                ((Component) newObject).gameObject.SetActive(enabledAsDefault);
            }

            objQueue.Enqueue(newObject);
        }

        public static T RetrieveObject<T>(T obj) where T : Object
		{
            PoolObjectQueue poq = m_objectQueues[obj];
            Queue<Object> objQueue = poq.queue;

            if (!poq.isCircularPool && objQueue.Count == 0)
            {
                AddNewObject(obj, objQueue, poq.enabledAsDefault);
            }

            Object newObj = objQueue.Dequeue();

            if (poq.isCircularPool)
            {
                objQueue.Enqueue(newObj);
            }

			return (T) newObj;
		}

        public static GameObject SpawnObject(GameObject obj, Vector3 position, Quaternion rotation)
		{
            GameObject newObj = RetrieveObject(obj);
            newObj.transform.SetPositionAndRotation(position, rotation);
            newObj.gameObject.SetActive(true);

			return newObj;
		}

        public static T SpawnObject<T>(T obj, Vector3 position, Quaternion rotation) where T : Component
        {
            Component newObj = RetrieveObject(obj);
            newObj.transform.SetPositionAndRotation(position, rotation);
            newObj.gameObject.SetActive(true);

            return (T) newObj;
        }

        public static void DestroyPool()
		{
            foreach (var objQueue in m_objectQueues)
            {
                foreach (Object objectToDestroy in objQueue.Value.queue)
                {
                    Object.Destroy(objectToDestroy);
                }
            }

            m_objectQueues.Clear();
            m_objectQueues = null;
		}
	}
}

