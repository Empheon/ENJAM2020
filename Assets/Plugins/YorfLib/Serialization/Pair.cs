using System;

namespace YorfLib
{
	[Serializable]
	public class Pair<T, U>
	{
		public T m_first;
		public U m_second;

		public Pair(){}

		public Pair(T m_first, U m_second)
		{
			this.m_first = m_first;
			this.m_second = m_second;
		}
	}
	
	public class Pair
	{
		public object m_first;
		public object m_second;

		public Pair() { }

		public Pair(object m_first, object m_second)
		{
			this.m_first = m_first;
			this.m_second = m_second;
		}
	}
}
