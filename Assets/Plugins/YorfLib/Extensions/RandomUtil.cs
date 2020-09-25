using UnityEngine;

namespace YorfLib
{
	public static class RandomUtil
	{
		public static int Poisson(this Random random, float lambda)
		{
			lambda = Mathf.Exp(-lambda);
			int k = 0;
			float p = 1.0F;

			do
			{
				k++;
				p *= Random.value;
			} while (p > lambda);

			return k - 1;
		}
	}
}
