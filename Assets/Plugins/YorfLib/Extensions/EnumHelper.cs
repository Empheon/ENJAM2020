using System;
using System.Reflection;

namespace YorfLib
{
	public static class EnumHelper
	{
		public static T GetAttributeOfType<T>(this Enum value) where T : Attribute
		{
			Type type = value.GetType();
			MemberInfo[] members = type.GetMember(value.ToString());
			object[] attributes = members[0].GetCustomAttributes(typeof(T), false);
			return attributes.Length > 0 ? (T) attributes[0] : null;
		}
	}
}
