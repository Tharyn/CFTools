using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)
using System;
using System.IO;


public class PasteMaxTransform : ScriptableWizard {

    // Active Object
    static public GameObject ObjectToAlign = null;
    public GameObject ObjedctToAlign = null;

    public bool Position = true;
    public bool Rotation = true;
    public bool Scale = true;

    public Vector3 BasePosition;
    //[HideInInspector]
    public Quaternion BaseRotation;
    //[HideInInspector]
    public Vector3 BaseScale = new Vector3(1, 2, 1);


    public Vector3 CachePosition;
    //[HideInInspector]
    public Quaternion CacheRotation;
    //[HideInInspector]
    public Vector3 CacheScale = new Vector3(1, 2, 1);




    
    //MenuItem
    [MenuItem("CF/Cross/PASTE MAX Transform")]
    static void CreateWindow()
    {
        // Creates the wizard for display
        if (Selection.gameObjects.Length > 0)
        {
            ScriptableWizard.DisplayWizard("This will apply the cached Max Transform to your selection.",
                typeof(PasteMaxTransform), "Restore", "Apply");
        }
        else
        {
            Debug.Log("PLEASE SELECT AN OBJECT FIRST.");
        }
    }
    /*
    // Read CFTransformFile
    static List<string> CSVreader()
    {
        string cacheLocation = (Application.dataPath + "/(Cache)/FromMax.cache");
        var reader = new StreamReader(File.OpenRead(@cacheLocation));
        List<string> listA = new List<string>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');

            for (int i = 0; i < values.Length; i++)
            {
                listA.Add(values[i]);
                Debug.Log(values[i]);
            }
        }
        return listA;
    }

    
    /*
    static void PasteMaxTransform()
    {
        if (Selection.objects.Length == 1)
        {
            GameObject Go = Selection.objects[0] as GameObject;
            List<string> transformList = CSVreader();

            Quaternion rotation = Quaternion.Euler(Convert.ToSingle(transformList[3]), Convert.ToSingle(transformList[4]), Convert.ToSingle(transformList[5]));
            Quaternion rotationInv = new Quaternion(rotation.x * -1, rotation.y * -1, rotation.z * -1, rotation.w * -1);

            Go.transform.position = new Vector3(Convert.ToSingle(transformList[0]), Convert.ToSingle(transformList[1]), Convert.ToSingle(transformList[2]));
            Go.transform.rotation = rotationInv;
            Go.transform.localScale = new Vector3(Convert.ToSingle(transformList[6]), Convert.ToSingle(transformList[7]), Convert.ToSingle(transformList[8]));
        }
        else
        {
            Debug.Log("You must select a single object.");
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
            BaseScale = ObjectToAlign.transform.localScale;

            List<string> transformList = CSVreader();


            Quaternion rotation = Quaternion.Euler(Convert.ToSingle(transformList[3]), Convert.ToSingle(transformList[4]), Convert.ToSingle(transformList[5]));
            Quaternion rotationInv = new Quaternion(rotation.x * -1, rotation.y * -1, rotation.z * -1, rotation.w * -1);

            CachePosition = new Vector3(Convert.ToSingle(transformList[0]), Convert.ToSingle(transformList[1]), Convert.ToSingle(transformList[2]));
            CacheRotation = rotationInv;
            CacheScale = new Vector3(Convert.ToSingle(transformList[6]), Convert.ToSingle(transformList[7]), Convert.ToSingle(transformList[8]));

        }
    }

    void OnWizardOtherButton()
    {
        GameObject TargetObject = Selection.gameObjects[0];
        if (Selection.gameObjects[0] != null && Selection.gameObjects[0] != ObjectToAlign)
        {
            if (Position)
            {
                ObjectToAlign.transform.position = TargetObject.transform.position;
            }
            if (Rotation)
            {
                ObjectToAlign.transform.rotation = TargetObject.transform.rotation;
            }
            if (Scale)
            {
                ObjectToAlign.transform.localScale = TargetObject.transform.localScale;
            }
        }
        else
        {
            Debug.Log("You need to select another object to align to.");
        }

    }

    void OnWizardCreate()
    {

        ObjectToAlign.transform.position = new Vector3(Convert.ToSingle(transformList[0]), Convert.ToSingle(transformList[1]), Convert.ToSingle(transformList[2]));
        ObjectToAlign.transform.rotation = rotationInv;
        ObjectToAlign.transform.localScale = new Vector3(Convert.ToSingle(transformList[6]), Convert.ToSingle(transformList[7]), Convert.ToSingle(transformList[8]));

    }
    */
}
