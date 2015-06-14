using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using System;

public class Reflection
{

    // Get the sorting layer names
    public string[] GetSortingLayerNames()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        
        return (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }

    // Get the unique sorting layer IDs -- tossed this in for good measure
    public int[] GetSortingLayerUniqueIDs()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);
        return (int[])sortingLayerUniqueIDsProperty.GetValue(null, new object[0]);
    }
}