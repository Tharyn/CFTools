using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

class CFInspector
{

    [MenuItem("CF/Tools/Introspect")]
    static void IntrospectNow()
    {
        
        //TV GameObject[] go = Selection.gameObjects;
        Transform[] trs = Selection.GetTransforms(SelectionMode.Deep | SelectionMode.DeepAssets);

        foreach (Transform tr in trs)
        {
            Component[] components = tr.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                Component c = components[i];
                if (c == null)
                {
                    Debug.Log(tr.name + " has an empty script attached in position: " + i);
         
                }
                else
                {
                    Type t = c.GetType();
                    Debug.Log("Type " + t);
                    Debug.Log("Type information for:" + t.FullName);
                    Debug.Log("\tBase class = " + t.BaseType.FullName);
                    Debug.Log("\tIs Class = " + t.IsClass);
                    Debug.Log("\tIs Enum = " + t.IsEnum);
                    Debug.Log("\tAttributes = " + t.Attributes);


                    System.Reflection.FieldInfo[] fieldInfo = t.GetFields();
                    foreach (System.Reflection.FieldInfo info in fieldInfo)
                        Debug.Log("Field:" + info.Name);

                    System.Reflection.PropertyInfo[] propertyInfo = t.GetProperties();
                    foreach (System.Reflection.PropertyInfo info in propertyInfo)
                        Debug.Log("Prop:" + info.Name);

                    Debug.Log("Found component " + c);
                }
            }
        }
    }
}