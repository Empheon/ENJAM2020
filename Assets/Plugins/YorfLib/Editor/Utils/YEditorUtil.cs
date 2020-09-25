using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YorfLib
{
	public class YEditorUtil : ScriptableObject
	{
		public static Color GetHighLightColor()
		{
			return EditorGUIUtility.isProSkin ? new Color(0.3F, 0.3F, 0.3F) : new Color(0.9F, 0.9F, 0.9F);
		}

		public static Color GetTableBackgroundColor(bool isOdd)
		{
			if (EditorGUIUtility.isProSkin)
			{
				return isOdd ? new Color(0.1875F, 0.1875F, 0.1875F) : new Color(0.1718F, 0.1718F, 0.1718F);
			}
			else
			{
				return isOdd ? new Color(0.867F, 0.867F, 0.867F) : new Color(0.8437F, 0.8437F, 0.8437F);
			}
		}

		public static void DrawBackgroundRect(Color color, int height)
		{
			Rect rect = EditorGUILayout.GetControlRect(false, 0);
			rect.height = height;
			EditorGUI.DrawRect(rect, color);
		}

		public static string SearchField(string searchString)
		{
			BeginToolbar();
			searchString = GUILayout.TextField(searchString, GUI.skin.FindStyle("ToolbarSeachTextField")).ToLower();
			if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
			{
				searchString = "";
				GUI.FocusControl(null);
			}
			EndToolBar();

			return searchString;
		}

		public static void BeginToolbar()
		{
			GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
		}

		public static void EndToolBar()
		{
			GUILayout.EndHorizontal();
		}


		private readonly static Dictionary<System.Type, System.Func<string, object, object>> s_editorFieldsForType = new Dictionary<System.Type, System.Func<string, object, object>>()
		{
			{ typeof(int), (name, value) => { return EditorGUILayout.IntField(name, (int) value); } },
			{ typeof(long), (name, value) => { return EditorGUILayout.LongField(name, (long) value); } },
			{ typeof(double), (name, value) => { return EditorGUILayout.DoubleField(name, (double) value); } },
			{ typeof(float), (name, value) => { return EditorGUILayout.FloatField(name, (float) value); } },
			{ typeof(string), (name, value) => { return EditorGUILayout.TextField(name, (string) value); } },
			{ typeof(bool), (name, value) => { return EditorGUILayout.Toggle(name, (bool) value); } },
			{ typeof(Color), (name, value) => { return EditorGUILayout.ColorField(name, (Color) value); } },
			{ typeof(Gradient), (name, value) => { return EditorGUILayout.GradientField(name, (Gradient) value); } },
			{ typeof(Vector2), (name, value) => { return EditorGUILayout.Vector2Field(name, (Vector2) value); } },
			{ typeof(Vector3), (name, value) => { return EditorGUILayout.Vector3Field(name, (Vector3) value); } },
			{ typeof(Vector4), (name, value) => { return EditorGUILayout.Vector4Field(name, (Vector4) value); } },
			{ typeof(Vector2Int), (name, value) => { return EditorGUILayout.Vector2IntField(name, (Vector2Int) value); } },
			{ typeof(Vector3Int), (name, value) => { return EditorGUILayout.Vector3IntField(name, (Vector3Int) value); } },
			{ typeof(Rect), (name, value) => { return EditorGUILayout.RectField(name, (Rect) value); } },
			{ typeof(RectInt), (name, value) => { return EditorGUILayout.RectIntField(name, (RectInt) value); } },
			{ typeof(Bounds), (name, value) => { return EditorGUILayout.BoundsField(name, (Bounds) value); } },
			{ typeof(BoundsInt), (name, value) => { return EditorGUILayout.BoundsIntField(name, (BoundsInt) value); } },
			{ typeof(AnimationCurve), (name, value) => { return EditorGUILayout.CurveField(name, (AnimationCurve) value); } },
		};

		public static object DisplayFieldsForObject(object obj, int depth = 0)
		{
			if(obj == null)
			{
				EditorGUILayout.LabelField("Object is null!");
				return obj;
			}

			System.Type type = obj.GetType();
			List<FieldInfo> fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();

			foreach (FieldInfo field in fieldInfos)
			{
				field.SetValue(obj, HandleFieldForObject(field.GetValue(obj), field.FieldType, field.Name, depth));
			}

			return obj;
		}

		private static object HandleFieldForObject(object obj, System.Type type, string name, int depth)
		{
			if (obj == null)
			{
				EditorGUILayout.LabelField($"{name}: null");
				return obj;
			}

			if (type.IsEnum)
			{
				return EditorGUILayout.EnumPopup(name, (System.Enum) obj);
			}
			else if (s_editorFieldsForType.TryGetValue(type, out var editorFunction))
			{
				return editorFunction.Invoke(name, obj);
			}
			else if (type.IsArray)
			{
				EditorGUILayout.Foldout(true, name);
				EditorGUI.indentLevel++;
				System.Array array = (System.Array) obj;

				// Todo handle resize
				System.Type arrayType = type.GetElementType();
				int newSize = EditorGUILayout.IntField("Size", array.Length);
				if (newSize != array.Length)
				{
					System.Array newArray = System.Array.CreateInstance(arrayType, newSize);
					System.Array.Copy(array, 0, newArray, 0, Mathf.Min(array.Length, newSize));
					array = newArray;
				}

				for(int i = 0; i < array.Length; i++)
				{
					array.SetValue(HandleFieldForObject(array.GetValue(i), arrayType, $"Element {i}", depth), i);
				}

				EditorGUI.indentLevel--;
				return array;
			}
			else if (!type.IsPrimitive && depth < 5)
			{
                if (obj as Object)
                {
                    return EditorGUILayout.ObjectField((Object) obj, type, false);
                }

                EditorGUILayout.Foldout(true, name);
                EditorGUI.indentLevel++;
                object outObj = DisplayFieldsForObject(obj, depth + 1);
                EditorGUI.indentLevel--;
                return outObj;
			}

			Debug.LogError($"Type {type.Name} not handled!");
			return null;
		}
	}
}
