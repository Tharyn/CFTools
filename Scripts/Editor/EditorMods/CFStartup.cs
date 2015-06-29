using UnityEngine;
using UnityEditor;

using UnityEditorInternal;

using System.Reflection;
using System;


[InitializeOnLoad]
public class Startup
{
    static Startup()
    {
        // Clean up
        //CFTools.ClearConsole();     

        // Add CF GUI
        SceneView.onSceneGUIDelegate += CFTools.OnScene;
        SceneView.RepaintAll();
        CFTools.CFGUI = true;


        Debug.Log("CF Tools Loaded..." + System.DateTime.Now );


        // Get sorting layers
        //TV Type internalEditorUtilityType = typeof(InternalEditorUtility);
        //TV PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        //TV string[] sortingLayers = (string[])sortingLayersProperty.GetValue(null, new object[0]);
        /*
        foreach (string layer in sortingLayers)
        {
            Debug.Log(layer);
        }
        */




        // Layer Masks
        //Debug.Log(UnityEngine.LayerMask.LayerToName(1));
        /*
        foreach (Type layer2 in internalEditorUtilityType.Assembly.GetTypes())
        {
            Debug.Log(layer2);
        }

        
        // UNKNOWN
        Debug.Log((internalEditorUtilityType.Assembly).GetExecutingAssembly() );
        string @namespace = "UnityEditor";
        //.GetExecutingAssembly().GetTypes()
        var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == @namespace
                select t;
        q.ToList().ForEach(t => Debug.Log(t.Name));	
        */
    }
}