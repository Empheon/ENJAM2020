using System.Collections.Generic;
using UnityEngine;

namespace YorfLib
{
	[System.Serializable]
	public class CircularPool
	{
		public GameObject m_gameObject;
		public int m_size;

		private Queue<GameObject> m_objectQueue;

		public CircularPool()
		{
		}

		public CircularPool(GameObject gameObject, int size)
		{
			m_gameObject = gameObject;
			m_size = size;
		}

		public void InitPool(bool objectActive = false)
		{
			m_objectQueue = new Queue<GameObject>(m_size);

			for (int i = 0; i < m_size; i++)
			{
				GameObject newObject = Object.Instantiate(m_gameObject);
				newObject.SetActive(objectActive);
				m_objectQueue.Enqueue(newObject);
			}
		}

		public GameObject RetrieveObject()
		{
			GameObject newObject = m_objectQueue.Dequeue();
			m_objectQueue.Enqueue(newObject);

			return newObject;
		}

		public GameObject SpawnObject(Vector3 position, Quaternion rotation)
		{
			GameObject newObject = RetrieveObject();
			newObject.transform.SetPositionAndRotation(position, rotation);
			newObject.SetActive(true);
			return newObject;
		}

		public void DestroyPool()
		{
			foreach (GameObject objectToDestroy in m_objectQueue)
			{
				Object.Destroy(objectToDestroy);
			}

			m_objectQueue.Clear();
			m_objectQueue = null;
		}
	}
}
