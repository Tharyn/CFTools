using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)

public class AlignToGameObject : ScriptableWizard {

    public GameObject ObjectToAlign = null;


    public bool Position = true;
    public bool Rotation = true;
    public bool Scale = false;

    public Vector3 BasePosition;
    //[HideInInspector]
    public Quaternion BaseRotation;
    //[HideInInspector]
    public Vector3 BaseScale = new Vector3(1, 1, 1);

    [MenuItem("CF/Align/Align to Object")]
    static void CreateWindow()
    {
        // Creates the wizard for display
        if (Selection.gameObjects.Length > 0)
        {
            ScriptableWizard.DisplayWizard("Copy an object.",
                typeof(AlignToGameObject), "Restore", "Align");
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
            BasePosition = ObjectToAlign.transform.position;
            BaseRotation = ObjectToAlign.transform.rotation;
            BaseScale   =  ObjectToAlign.transform.localScale;
        }
    }

    void OnWizardOtherButton () {
        GameObject TargetObject = Selection.gameObjects[0];
        if (Selection.gameObjects[0] != null && Selection.gameObjects[0] != ObjectToAlign)
        {
            if (Position)
            {
                ObjectToAlign.transform.position = TargetObject.transform.position;
            }
            if (Rotation)
            {
                ObjectToAlign.transform.rotation = TargetObject.transform.rotation ;
            }
            if (Scale)
            {
                ObjectToAlign.transform.localScale = TargetObject.transform.localScale ;
            }
        }
        else
        {
            Debug.Log("You need to select another object to align to.");
        }

    }

    void OnWizardCreate()
    {
        ObjectToAlign.transform.position = BasePosition;
        ObjectToAlign.transform.rotation = BaseRotation;
        ObjectToAlign.transform.localScale = BaseScale; 
    }


}
