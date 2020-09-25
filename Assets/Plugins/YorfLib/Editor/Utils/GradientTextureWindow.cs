using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YorfLib;

public class GradientTextureWindow : EditorWindow
{
    private Gradient m_gradient = new Gradient();
    private int m_textureWidth = 128;

    [MenuItem("YorfLib/GradientToTexture")]
    public static void ShowWindow()
    {
       GetWindow(typeof(GradientTextureWindow));
    }

    void OnGUI()
    {
        m_gradient = EditorGUILayout.GradientField(m_gradient);
        m_textureWidth = EditorGUILayout.IntField(m_textureWidth);

        if(GUILayout.Button("Generate"))
        {
            string path = EditorUtility.SaveFilePanel("Save", "", "Gradient", "png");
            var pngData = m_gradient.CreateTexture(m_textureWidth).EncodeToPNG();
            File.WriteAllBytes(path, pngData);
        }
    }
}
