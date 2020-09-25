using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(EnumNamedArrayAttribute))]
public class DrawerEnumNamedArray : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnumNamedArrayAttribute enumNames = attribute as EnumNamedArrayAttribute;
        //propertyPath returns something like component_hp_max.Array.data[4]
        //so get the index from there
        int index = System.Convert.ToInt32(property.propertyPath.Substring(property.propertyPath.IndexOf("[")).Replace("[", "").Replace("]", ""));
        //change the label
        if(index < enumNames.Names.Length)
        {
            label.text = enumNames.Names[index];
        }
        //draw field
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }
}
#endif

public class EnumNamedArrayAttribute : PropertyAttribute
{
#if UNITY_EDITOR
    public string[] Names;
#endif

    public EnumNamedArrayAttribute(Type enumType)
    {
#if UNITY_EDITOR
        Names = Enum.GetNames(enumType);
#endif
    }
}