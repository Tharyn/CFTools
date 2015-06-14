using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)
using System.IO;


/* This is a collection of static variables and methods for use in CFTools */
public class CFTools : MonoBehaviour
{

    /* USEFUL FINCTIONS 
        UnityEditorInternal.ComponentUtility
    */
    [System.Serializable]
    public class ListWrapperInt
    {
        public List<int> myList;
    }

    [System.Serializable]
    public class ListWrapperGo
    {
        public List<GameObject> myList;
    }

    public static bool CFGUI;
    public static List<GameObject> SelectionTBL;
    public static List<GameObject> HiddenTBL;
    public static List<int> LayerTBL;
    public static bool IsIsolated;

    // THIS METHOD USES REFLECTION TO CLEAR THE CONCOLE
    //  Debug.ClearDeveloperConsole()  <---- Does not work 
    public static void ClearConsole()
    {
        // This simply does "LogEntries.Clear()" the long way:
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    // Select an Object. This function simply converts a simgle selection to an array.
    public static void SelectObject(GameObject go)
    {
        GameObject[] objs = new GameObject[1];
        objs[0] = go;
        Selection.objects = objs;
    }

    // RECURSIVLY GET ALL CHILDREN
    public static void AddChildren(Transform activeObj)
    {
        if (activeObj.childCount > 0)
            for (int i = 0; i < activeObj.childCount; i++)           // FOR THE NUMBER OF CHILDREN(Transforms)
            {
                SelectionTBL.Add(activeObj.GetChild(i).gameObject);  // ADD THE GAME OBJECT THE TRANSFORM IS ATTACHED TO
                if (activeObj.GetChild(i).childCount != 0)           // IF THE CHILD HAS CHILDREN RECURSE     
                    AddChildren(activeObj.GetChild(i));
            }
    }

    // GET SELECTION AS A LIST
    public static void GetSelected(bool withChildren)
    {
        SelectionTBL = new List<GameObject>(); // NEW LIST
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            SelectionTBL.Add(Selection.gameObjects[i]);
            if (withChildren)
                AddChildren(Selection.gameObjects[i].transform);
        }
    }

    // SELECT CHILDREN 
    public static void SelectChildren()
    {
        if (Selection.gameObjects.Length > 0)
        {
            GetSelected(true);
            Selection.objects = SelectionTBL.ToArray();
        }
        else
            Debug.Log("Nothing Selected.");
    }


    // INVERT SELECTION 
    public static void InvertSelection()
    {
        GetSelected(true);

        // GET ALL OBJECTS
        GameObject[] activeObjs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> invSelectionTBL = new List<GameObject>();

        for (int i = 0; i < activeObjs.Length; i++)
            if ((activeObjs[i]).activeInHierarchy)
                if (SelectionTBL.Contains(activeObjs[i]) == false)
                {
                    invSelectionTBL.Add(activeObjs[i]);
                }
        Selection.objects = invSelectionTBL.ToArray();
    }

    // HIDE SELECTION
    public static void Hide()
    {
        if (HiddenTBL == null)
        {
            HiddenTBL = new List<GameObject>();
            LayerTBL = new List<int>();
        }
        GetSelected(false);
        for (int i = 0; i < SelectionTBL.Count; i++)
            if (HiddenTBL.Contains(SelectionTBL[i]) == false)
            {
                HiddenTBL.Add(SelectionTBL[i]);
                LayerTBL.Add(SelectionTBL[i].layer);
                SelectionTBL[i].layer = LayerMask.NameToLayer("Hidden");
                SelectionTBL[i].hideFlags = HideFlags.HideInHierarchy;
            }
    }

    // ENTER ISOLATION MODE
    public static void Isolate()
    {
        if (Selection.gameObjects.Length > 0)
        {
            IsIsolated = true; //FLAG
            GameObject root = Selection.gameObjects[0];
            InvertSelection();
            Hide();
            SelectionTBL = new List<GameObject>(); // NEW LIST
            SelectionTBL.Add(root);
            Selection.objects = SelectionTBL.ToArray();
            SceneView.lastActiveSceneView.pivot = root.transform.position;
            
        }
        else
            Debug.Log("Nothing Selected.");
    }

    // EXIT ISOLATION MODE
    public static void ExitIsolate()
    {
        IsIsolated = false;

        for (int i = 0; i < HiddenTBL.Count; i++)
        {
            HiddenTBL[i].layer = LayerTBL[i];
            HiddenTBL[i].hideFlags = HideFlags.None;
        }
        HiddenTBL = new List<GameObject>();
        LayerTBL = new List<int>();
    }


    void DoWindow()
    {
        GUILayout.Button("Hi");
        GUI.DragWindow();
    }

    // CALCULATE BOUNDS 
    Bounds CalculateBounds(GameObject go)
    {
        Bounds b = new Bounds(go.transform.position, Vector3.zero);
        Object[] rList = go.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer r in rList)
        {
            b.Encapsulate(r.bounds);
        }
        return b;
    }

    // FOCUS CAMERA ON OBJECT
    public void FocusCameraOnGameObject(Camera c, GameObject go)
    {
        Bounds b = CalculateBounds(go);
        Vector3 max = b.size;
        // Get the radius of a sphere circumscribing the bounds
        float radius = max.magnitude / 2f;
        // Get the horizontal FOV, since it may be the limiting of the two FOVs to properly encapsulate the objects
        float horizontalFOV = 2f * Mathf.Atan(Mathf.Tan(c.fieldOfView * Mathf.Deg2Rad / 2f) * c.aspect) * Mathf.Rad2Deg;
        // Use the smaller FOV as it limits what would get cut off by the frustum        
        float fov = Mathf.Min(c.fieldOfView, horizontalFOV);
        float dist = radius / (Mathf.Sin(fov * Mathf.Deg2Rad / 2f));
        Debug.Log("Radius = " + radius + " dist = " + dist);

        //c.transform.SetLocalPositionZ(dist);
        Vector3 tempV3 = c.transform.localPosition;
        tempV3.z = dist;
        c.transform.localPosition = tempV3;

        if (c.orthographic)
            c.orthographicSize = radius;

        // Frame the object hierarchy
        c.transform.LookAt(b.center);
    }

    // CF GUI
    public static void OnScene(SceneView sceneview)
    {
        /*
         * Right
          quaternionArray[index1] = Quaternion.LookRotation(new Vector3(-1f, 0.0f, 0.0f));
        Top
          quaternionArray[index2] = Quaternion.LookRotation(new Vector3(0.0f, -1f, 0.0f));
        front
          quaternionArray[index3] = Quaternion.LookRotation(new Vector3(0.0f, 0.0f, -1f));
        left
          quaternionArray[index4] = Quaternion.LookRotation(new Vector3(1f, 0.0f, 0.0f));
        bottom
          quaternionArray[index5] = Quaternion.LookRotation(new Vector3(0.0f, 1f, 0.0f));
        back
          quaternionArray[index6] = Quaternion.LookRotation(new Vector3(0.0f, 0.0f, 1f));
         

        if ( Event.current.type == EventType.keyDown ) {
            if ( Event.current.isKey && Event.current.keyCode == KeyCode.T ) {
                
                Debug.Log( "Number 2 was pressed, default unity hotkey is overridden." );
                //Event.current.Use();    // if you don't use the event, the default action will still take place.

                Quaternion temp = Quaternion.LookRotation(new Vector3(0.0f, -1f, 0.0f));
                SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, temp, SceneView.lastActiveSceneView.size, false);
            }
        }
        */

        /* HOT KEYS */
        if (Event.current.type == EventType.keyDown)
        {
            bool orthographic = SceneView.lastActiveSceneView.orthographic;
            if (Event.current.isKey) {
                switch (Event.current.keyCode)
                {
                    case KeyCode.T :
                        Quaternion tempT = Quaternion.LookRotation(new Vector3(0.0f, -1f, 0.0f));
                        SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, tempT, SceneView.lastActiveSceneView.size, orthographic);
                        SceneView.lastActiveSceneView.FrameSelected();
                        Event.current.Use();
                    break;
                    case KeyCode.B :
                        Quaternion tempF = Quaternion.LookRotation(new Vector3(0.0f, 0.0f, -1f));
                        SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, tempF, SceneView.lastActiveSceneView.size, orthographic);
                        SceneView.lastActiveSceneView.FrameSelected();
                        Event.current.Use();
                    break;
                    case KeyCode.F:
                        Quaternion tempB = Quaternion.LookRotation(new Vector3(0.0f, 0.0f, 1f));
                        SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, tempB, SceneView.lastActiveSceneView.size, orthographic);
                        SceneView.lastActiveSceneView.FrameSelected();
                        Event.current.Use();
                    break;
                    case KeyCode.R:
                        Quaternion tempR = Quaternion.LookRotation(new Vector3(-1f, 0.0f, 0f));
                        SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, tempR, SceneView.lastActiveSceneView.size, orthographic);
                        SceneView.lastActiveSceneView.FrameSelected();
                        Event.current.Use();
                    break;
                    case KeyCode.L:
                        Quaternion tempL = Quaternion.LookRotation(new Vector3(1f, 0.0f, 0f));
                        SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, tempL, SceneView.lastActiveSceneView.size, orthographic);
                        SceneView.lastActiveSceneView.FrameSelected();
                        Event.current.Use();
                    break;
                    case KeyCode.Z:
                        SceneView.lastActiveSceneView.FrameSelected();
                        Event.current.Use();
                    break;


                        
                }

            }

        }






        // Get all the CF Object in the scene
        CF_Properties[] cfObjects = FindObjectsOfType(typeof(CF_Properties)) as CF_Properties[];
        for (int i = 0; i < cfObjects.Length; i++)
        {
            // Look At Manager
            if (cfObjects[i].lookAtTarget != null)
            {
                if (cfObjects[i].selectTarget)
                    CFTools.SelectObject(cfObjects[i].lookAtTarget.gameObject);
            }
            cfObjects[i].selectTarget = false;

            // Gizmo Manager
            if (Selection.Contains(cfObjects[i].gameObject))
                cfObjects[i].selected = true;
            else
                cfObjects[i].selected = false;
            
        }
        Handles.BeginGUI();
     /* WORKING GUI BUTTONS
        
        // FIND OUT WHAT IS GOING ON THE THESCENE VIEW
        //Debug.Log( Event.current.type);

        // Persistant GUI
        if (CFTools.CFGUI)
        {
            GUILayout.BeginArea(new Rect(1, 1, 300, 300));
            GUILayout.BeginHorizontal();
            bool Debug0 = GUILayout.Button("Debug 0 ", GUILayout.Width(100));
            bool Debug1 = GUILayout.Button("Debug 1", GUILayout.Width(100));
            GUILayout.EndArea();

            // FOR FIXED LAYOUT
            if (Debug0)
            {
                string[] guids = AssetDatabase.FindAssets("RenderBox_05.max");
                //foreach (string guid in guids)
                    //Debug.Log(guid);

                GameObject test = AssetDatabase.LoadAssetAtPath("Assets/FBX/RenderBox_05.max.fbx", typeof(GameObject)) as GameObject;

                //Debug.Log(test.childCount);
            }
            if (Debug1)
                Debug.Log(Application.dataPath);
        }

        /* EXAMPLE OF WORLD TO SCREEN COORDS
        Camera[] cameraTBL = FindObjectsOfType(typeof(Camera)) as Camera[];
        for (int i = 0; i < cameraTBL.Length; i++)
        {
            LookAtTargetTarget lookAtControl = cameraTBL[i].GetComponent<LookAtTargetTarget>();
            //if (lookAtControl != null )
           // Debug.Log(Camera.current);
           // Debug.Log(cameraTBL[i].transform.position);
           // Debug.Log(Camera.current.WorldToScreenPoint(cameraTBL[i].transform.position));
            Vector3 fromPos = Camera.current.WorldToScreenPoint(cameraTBL[i].transform.position);
            fromPos.y = fromPos.y * -1 + Screen.height-35;
            fromPos.z = 0;
            Vector3 toPos = Camera.current.WorldToScreenPoint(lookAtControl.CamTarget.transform.position);
            toPos.y = toPos.y * -1 + Screen.height - 35;
            toPos.z = 0;

            //Handles.DrawLine(Camera.current.WorldToScreenPoint(cameraTBL[i].transform.position), Camera.current.WorldToScreenPoint(lookAtControl.CamTarget.transform.position));
            //Handles.DrawLine(new Vector3(0,0,0), new Vector3(20,20,0));
            //Handles.color = Color.blue;
            //Handles.DrawDottedLine(fromPos, toPos, 20);
        }
        */

        // Isolate selection GUI
        if (CFTools.IsIsolated)
            if (GUI.Button(new Rect(0, Screen.height - 80, Screen.width - 4, 20), "Exit Isolation Mode"))
                if (CFTools.IsIsolated)
                    CFTools.ExitIsolate();
                else
                    CFTools.Isolate();

        Handles.EndGUI();
      
    }

    ///////////////////////////////////////////////////////////////////////////
    // PERSISTANT WATCH METHODS
    public class NotifyForLockedFiles : UnityEditor.AssetModificationProcessor
    {
        public static string[] OnWillSaveAssets(string[] paths)
        {
            List<string> pathsToSave = new List<string>();

            for (int i = 0; i < paths.Length; ++i)
            {
                FileInfo info = new FileInfo(paths[i]);
                if (info.IsReadOnly)
                    UnityEditor.EditorUtility.DisplayDialog("Could not save!",
                    "Sorry, but " + paths[i] + " is Read-Only. Please save as another name.",
                    "Ok");
                else
                    pathsToSave.Add(paths[i]);
            }
            return pathsToSave.ToArray();
        }
    }
}
