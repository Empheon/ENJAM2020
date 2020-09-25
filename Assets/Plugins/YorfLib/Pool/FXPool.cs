using System.Collections.Generic;
using UnityEngine;

namespace YorfLib
{
	[System.Serializable]
	public class FXPool
	{
		public int m_size;

		private Dictionary<ParticleSystem, Queue<ParticleSystem>> m_objectQueues = new Dictionary<ParticleSystem, Queue<ParticleSystem>>();

		public void RegisterParticleSystem(ParticleSystem ps, int quantity)
		{
            if(m_objectQueues.ContainsKey(ps))
            {
                return;
            }

            Queue<ParticleSystem> psQueue = new Queue<ParticleSystem>(quantity);

            for (int i = 0; i < quantity; i++)
			{
                ParticleSystem newPs = Object.Instantiate(ps);
				newPs.gameObject.SetActive(false);
                psQueue.Enqueue(newPs);
			}

            m_objectQueues.Add(ps, psQueue);
		}

		public ParticleSystem RetrieveObject(ParticleSystem ps)
		{
            Queue<ParticleSystem> psQueue = m_objectQueues[ps];

            ParticleSystem newPS = psQueue.Dequeue();
            psQueue.Enqueue(newPS);

			return newPS;
		}

		public ParticleSystem SpawnParticle(ParticleSystem ps, Vector3 position, Quaternion rotation)
		{
			ParticleSystem newPS = RetrieveObject(ps);
            newPS.transform.SetPositionAndRotation(position, rotation);
            newPS.gameObject.SetActive(true);

            newPS.Play();

			return newPS;
		}

		public void DestroyPool()
		{
            foreach (var psQueue in m_objectQueues)
            {
                foreach (ParticleSystem objectToDestroy in psQueue.Value)
                {
                    Object.Destroy(objectToDestroy.gameObject);
                }
            }

            m_objectQueues.Clear();
            m_objectQueues = null;
		}
	}
}

