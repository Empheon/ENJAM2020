using UnityEngine;

namespace YorfLib
{
	public class MinMaxAttribute : PropertyAttribute
	{
		public float m_minLimit = 0.0f;
		public float m_maxLimit = 1.0f;
		public int m_decimalPlace = 2;
		public float m_step = 0.01f;

		public MinMaxAttribute(float min, float max)
		{
			m_minLimit = min;
			m_maxLimit = max;
		}
	}
}
