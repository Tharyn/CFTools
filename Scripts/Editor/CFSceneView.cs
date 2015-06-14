using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CF_Properties))]
public class DemoInspector : Editor
{
    void OnSceneGUI()
    {

        /*
        Handles.BeginGUI();

        GUILayout.Window(2, new Rect(Screen.width - 110, Screen.height - 130, 100, 100), (id) =>
        {
            // Content of window here
            GUILayout.Button("A Button");
        }, "Title");

        Handles.EndGUI();
        */
    }
}
