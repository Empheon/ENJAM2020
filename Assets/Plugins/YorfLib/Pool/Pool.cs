using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YorfLib
{
	[System.Serializable]
	public class Pool
	{
		public GameObject m_gameObject;
		public int m_size;
		public bool m_enabledAsDefault = false;

		private Queue<GameObject> m_objectQueue;
		private List<GameObject> m_allObjectList;

		public Pool()
		{
		}

		public Pool(GameObject gameObject, int size, bool enabledAsDefault = false)
		{
			m_gameObject = gameObject;
			m_size = size;
			m_enabledAsDefault = enabledAsDefault;
		}

		public void InitPool()
		{
			m_objectQueue = new Queue<GameObject>(m_size);

			for (int i = 0; i < m_size; i++)
			{
				AddNewObject();
			}

            m_allObjectList = m_objectQueue.ToList();
        }

		private void AddNewObject()
		{
			GameObject newObject = Object.Instantiate(m_gameObject);
			newObject.SetActive(m_enabledAsDefault);
			m_objectQueue.Enqueue(newObject);
		}

		public GameObject RetrieveObject()
		{
			if (m_objectQueue.Count == 0)
			{
				AddNewObject();
			}

			return m_objectQueue.Dequeue();
		}

		public GameObject SpawnObject(Vector3 position, Quaternion rotation)
		{
			GameObject newObject = RetrieveObject();
			newObject.transform.SetPositionAndRotation(position, rotation);
			newObject.SetActive(true);
			return newObject;
		}

		public void ReturnObject(GameObject gameObject)
		{
			gameObject.SetActive(m_enabledAsDefault);
			m_objectQueue.Enqueue(gameObject);
		}

		public void DestroyPool()
		{
			foreach (GameObject objectToDestroy in m_allObjectList)
			{
				Object.Destroy(objectToDestroy);
			}

            m_allObjectList.Clear();
            m_allObjectList = null;

            m_objectQueue.Clear();
			m_objectQueue = null;
		}
	}
}
