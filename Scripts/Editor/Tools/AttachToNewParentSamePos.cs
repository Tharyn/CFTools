using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)

public class AttachToNewParentSamePos : ScriptableWizard
{
    public GameObject ObjectToAlign = null;
    public Transform ObjectParent = null;

    public bool Position = true;
    public bool Rotation = true;
    public bool Scale = false;

    public Vector3 BasePosition;
    //[HideInInspector]
    public Quaternion BaseRotation;
    //[HideInInspector]
    public Vector3 BaseScale = new Vector3(1, 1, 1);

    [MenuItem("CF/Align/NewParentInPosition")]
    static void CreateWindow()
    {
        // Creates the wizard for display
        if (Selection.gameObjects.Length > 0)
        {
            ScriptableWizard.DisplayWizard("Copy an object.",
                typeof(AttachToNewParentSamePos), "Restore", "Align");
        }
        else
        {
            Debug.Log("PLEASE SELECT AN OBJECT FIRST.");
        }
    }


    // Use this for initialization
    void OnWizardUpdate()
    {
        if (ObjectToAlign == null)
        {
            ObjectToAlign = Selection.gameObjects[0];
            ObjectParent = Selection.gameObjects[0].transform.parent;
            BasePosition = ObjectToAlign.transform.localPosition;
            BaseRotation = ObjectToAlign.transform.localRotation;
            BaseScale   =  ObjectToAlign.transform.localScale;
        }
    }

    void OnWizardOtherButton () {
        //GameObject TargetObject = Selection.gameObjects[0];
        if (Selection.gameObjects[0] != null && Selection.gameObjects[0] != ObjectToAlign)
        {
            ObjectToAlign.transform.parent = Selection.gameObjects[0].transform;
            ObjectToAlign.transform.localPosition = BasePosition;
            ObjectToAlign.transform.localRotation = BaseRotation;
            ObjectToAlign.transform.localScale = BaseScale; 
        }
        else
        {
            Debug.Log("You need to select another object to align to.");
        }

    }

    void OnWizardCreate()
    {
        ObjectToAlign.transform.parent = ObjectParent;
        ObjectToAlign.transform.localPosition = BasePosition;
        ObjectToAlign.transform.localRotation = BaseRotation;
        ObjectToAlign.transform.localScale = BaseScale; 
    }


}
