using UnityEditor;
using UnityEngine;

namespace YorfLib
{
	[CustomPropertyDrawer(typeof(YorfLib.MinMaxAttribute))]
	public class MinMaxDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			MinMaxAttribute minMax = attribute as MinMaxAttribute;

			if (property.propertyType == SerializedPropertyType.Vector2)
			{
				float minValue = property.vector2Value.x;
				float maxValue = property.vector2Value.y;
				float minLimit = minMax.m_minLimit;
				float maxLimit = minMax.m_maxLimit;

				string labelWithValue = label.text;
				labelWithValue += " [ " + property.vector2Value.x.ToString("n" + minMax.m_decimalPlace) + " ; " + property.vector2Value.y.ToString("n" + minMax.m_decimalPlace) + "]";
				label.text = labelWithValue;

				EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, minLimit, maxLimit);

				Vector2 vec = Vector2.zero;
				float decimalFactor = Mathf.Pow(10.0f, minMax.m_decimalPlace);
				float stepFactor = 1.0f / minMax.m_step;
				vec.x = Mathf.Round((Mathf.Round(minValue * decimalFactor) / decimalFactor) * stepFactor) / stepFactor;
				vec.y = Mathf.Round((Mathf.Round(maxValue * decimalFactor) / decimalFactor) * stepFactor) / stepFactor;
				property.vector2Value = vec;
			}
		}
	}
}
