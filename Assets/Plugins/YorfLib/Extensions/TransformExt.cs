using UnityEngine;

namespace YorfLib
{
	public static class TransformExt
	{
		public static void SetX(this Transform transform, float x)
		{
			Vector3 pos = transform.position;
			pos.x = x;
			transform.position = pos;
		}

		public static void AddX(this Transform transform, float x)
		{
			Vector3 pos = transform.position;
			pos.x += x;
			transform.position = pos;
		}

		public static void MulX(this Transform transform, float x)
		{
			Vector3 pos = transform.position;
			pos.x *= x;
			transform.position = pos;
		}

		public static void DivX(this Transform transform, float x)
		{
			Vector3 pos = transform.position;
			pos.x /= x;
			transform.position = pos;
		}

		public static void SetY(this Transform transform, float y)
		{
			Vector3 pos = transform.position;
			pos.y = y;
			transform.position = pos;
		}

		public static void AddY(this Transform transform, float y)
		{
			Vector3 pos = transform.position;
			pos.y += y;
			transform.position = pos;
		}

		public static void MulY(this Transform transform, float y)
		{
			Vector3 pos = transform.position;
			pos.y *= y;
			transform.position = pos;
		}

		public static void DivY(this Transform transform, float y)
		{
			Vector3 pos = transform.position;
			pos.y /= y;
			transform.position = pos;
		}

		public static void SetZ(this Transform transform, float z)
		{
			Vector3 pos = transform.position;
			pos.z = z;
			transform.position = pos;
		}

		public static void AddZ(this Transform transform, float z)
		{
			Vector3 pos = transform.position;
			pos.z += z;
			transform.position = pos;
		}

		public static void MulZ(this Transform transform, float z)
		{
			Vector3 pos = transform.position;
			pos.z *= z;
			transform.position = pos;
		}

		public static void DivZ(this Transform transform, float z)
		{
			Vector3 pos = transform.position;
			pos.z /= z;
			transform.position = pos;
		}

		public static void SetGlobalScale(this Transform t, Vector3 globalScale)
		{
			t.localScale = Vector3.one;
			t.localScale = new Vector3(globalScale.x / t.lossyScale.x, globalScale.y / t.lossyScale.y, globalScale.z / t.lossyScale.z);
		}
	}
}
