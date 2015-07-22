using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic; // NEEDED FOR (Lists)
using System;
using System.IO;

public class CFMenus : MonoBehaviour
{


    static int FindDepth(GameObject Go, int depth)
    {
        if (Go.transform.parent == null)
            return (depth);
        else
        {
            return (FindDepth(Go.transform.parent.gameObject, (depth + 1)));
        }
    }


    [MenuItem("CF/TEST CODE")]
    static void TestCode()
    {
        GameObject  A = Selection.objects[0] as GameObject;
        /*
        Debug.Log(A.light.alreadyLightmapped);
        Debug.Log(A.light.cullingMask);
        Debug.Log(A.light.renderMode);
         * */
        //Debug.Log(FindDepth(A, 0));

        //EditorGUIUtility.SetVisibleLayers(1);
        Shader temp = EditorGUIUtility.LoadRequired("SceneView/SceneViewAura.shader") as Shader;
        A.renderer.material.shader = temp;
        // How to change the visible layers(int layertMask)
        //Tools.visibleLayers = 10;
        //Tools.lockedLayers = 10;


        /* How to change the Light mapping property of a LIGHT
        Light go = A.light;

        SerializedObject serialObj = new SerializedObject(go);
        SerializedProperty lightmapProp = serialObj.FindProperty("m_Lightmapping");

        lightmapProp.intValue = 1;
        serialObj.ApplyModifiedProperties(); 
         * */


    }

    [MenuItem("CF/Tools/Log Transform")]
    static void DumpTransform()
    {
        GameObject A = Selection.objects[0] as GameObject;
        Debug.Log(A.transform.position);
        Debug.Log(A.transform.rotation);
        Debug.Log(A.transform.localScale);
        //Debug.Log(new Quaternion.Euler(new Vector3 (-2.575928, -165.2178, 99.6658)));
        //Vector3 temp = new Vector3(-2.575928f, -165.2178f, 99.6658f);

        Quaternion rotation = Quaternion.Euler(-2.575928f, -165.2178f, 99.6658f);
        Debug.Log(rotation);
        Quaternion rotationB = new Quaternion(rotation.x * -1, rotation.y * -1, rotation.z * -1, rotation.w * -1);
        Debug.Log(rotationB);

        Debug.Log(rotationB.eulerAngles);
       
    }

    

    [MenuItem("CF/Cross/COPY Unity Transform")]
    static void CopyUnityTransform()
    {
        if (Selection.objects.Length == 1)
        {
            string cacheLocation = (Application.dataPath + "/(Cache)/FromUnity.cache");

            GameObject Go = Selection.objects[0] as GameObject;
            string transformList = "";
            transformList += "";
            transformList += Go.transform.position.x.ToString() + ",";
            transformList += Go.transform.position.y.ToString() + ",";
            transformList += Go.transform.position.z.ToString() + ",";
            Vector3 tempRot = Go.transform.rotation.eulerAngles;

            transformList += tempRot.x.ToString() + ",";
            transformList += tempRot.y.ToString() + ",";
            transformList += tempRot.z.ToString() + ",";

            transformList += Go.transform.localScale.x.ToString() + ",";
            transformList += Go.transform.localScale.y.ToString() + ",";
            transformList += Go.transform.localScale.z.ToString();
            File.WriteAllText(cacheLocation, transformList);
            System.IO.File.WriteAllText(@cacheLocation, transformList);

            Debug.Log(transformList);
        }
        else
        {
            Debug.Log("You must select a single object.");
        }
    }



    // VIEW
    [MenuItem("CF/Isolate &q")]
    static void Isolate()
    {
        if (CFTools.IsIsolated)
            CFTools.ExitIsolate();
        else
            CFTools.Isolate();
    }

    // SCENE VIEW GUI
    [MenuItem("CF/GUI")]
    public static void Enable()
    {
        if (CFTools.CFGUI)
        {
            CFTools.CFGUI = false;
            SceneView.RepaintAll();
        }
        else
        {
            CFTools.CFGUI = true;
            SceneView.RepaintAll();
        }
    }

    // FILES
    [MenuItem("CF/Files/New &n")]
    static void New()
    {
        CFTools.ClearConsole();
        if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
            EditorApplication.OpenScene("Assets/CFTools/Resources/Untitled.unity");
        Debug.Log("... Default Scene Loaded ...");
    }
    [MenuItem("CF/Files/Hold &h")]
    static void Hold()
    {
        
        String[] path = EditorApplication.currentScene.Split(char.Parse("/"));
        if (path[path.Length-1] != "Untitled.unity")
        {
            EditorApplication.SaveScene("Assets/CFTools/Resources/Hold.unity", true);
            Debug.Log("... Scene Held ...");
        }
        else
            Debug.Log("... Scene 'Untitled.unity' can't not be Held. Please save your scene file as a new name ...");
        
    }
    [MenuItem("CF/Files/Fetch &f")]
    static void Fetch()
    {
        String[] path = EditorApplication.currentScene.Split(char.Parse("/"));
        if (path[path.Length-1] != "Untitled.unity")
        {
            if (EditorUtility.DisplayDialog("Warning", "FETCH an not be undone.", "Fetch", "Cancle"))
            {
                string aPath = EditorApplication.currentScene;
                EditorApplication.OpenScene("Assets/CFTools/Resources/Hold.unity");
                EditorApplication.SaveScene(aPath);
                Debug.Log("... Scene Retrived ...");
            }
        }
        else
            Debug.Log("... Scene 'Untitled.unity' can't not be overwritten . Please save your scene file as a new name ...");
    }


    // CREATE
    [MenuItem("CF/Create/Null",false, 1)]
    static void Null()
    {
        GameObject go = new GameObject("Null");
        go.transform.position = new Vector3(0, 0, 0);

        go.AddComponent("CF_Properties");
        go.GetComponent<CF_Properties>().gizmo = true;
        CFTools.SelectObject(go);
    }


    [MenuItem("CF/Create/Cube", false, 12)]
    static void CFCube()
    {
        GameObject tempGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject go = new GameObject("Box");
        go.transform.position = new Vector3(0, 0, 0);

        go.AddComponent("CubeProps");
        //TV CubeProps tempCP = go.GetComponent<CubeProps>();

        //go.AddComponent("BoxCollider");

        go.AddComponent("MeshFilter");
        MeshFilter mesh = go.GetComponent<MeshFilter>();
        mesh.sharedMesh = tempGo.GetComponent<MeshFilter>().sharedMesh;

        go.AddComponent("MeshRenderer");
        Material diffuse = tempGo.GetComponent<MeshRenderer>().sharedMaterial;
        go.renderer.sharedMaterial = diffuse;

        DestroyImmediate(tempGo);
        /* THIS CHANGES THE ICON
        Texture2D icon = (Texture2D)Resources.Load("CFLogo");
        Type editorGUIUtilityType = typeof(EditorGUIUtility);
        BindingFlags bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        object[] args = new object[] { tempCP, icon };
        editorGUIUtilityType.InvokeMember("SetIconForObject", bindingFlags, null, null, args);
        */
        CFTools.SelectObject(go);
        SceneView.RepaintAll();
    }

    [MenuItem("CF/Create/Cylinder", false, 13)]
    static void Cylinder()
    {
        GameObject tempGo = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject go = new GameObject("Cylinder");
        go.transform.position = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(1, .5F, 1);

        go.AddComponent("CylinderProps");

        /* 
        go.AddComponent("CapsuleCollider");
        CapsuleCollider goCol = go.GetComponent<CapsuleCollider>();
        (goCol as CapsuleCollider).height = 2; // YOU HAVE TO GET THE Collider this way to access the height. 
        */

        go.AddComponent("MeshFilter");
        MeshFilter mesh = go.GetComponent<MeshFilter>();
        mesh.sharedMesh = tempGo.GetComponent<MeshFilter>().sharedMesh;

        go.AddComponent("MeshRenderer");
        Material diffuse = tempGo.GetComponent<MeshRenderer>().sharedMaterial;
        go.renderer.sharedMaterial = diffuse;

        DestroyImmediate(tempGo);

        CFTools.SelectObject(go);
        SceneView.RepaintAll();
    }

    [MenuItem("CF/Create/Plane", false, 14)]
    static void Plane()
    {
        GameObject tempGo = GameObject.CreatePrimitive(PrimitiveType.Plane);
        GameObject go = new GameObject("Plane");
        go.transform.position = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(.5F, 1, .5F);

        go.AddComponent("PlaneProps");

        // go.AddComponent("MeshCollider");

        go.AddComponent("MeshFilter");
        MeshFilter mesh = go.GetComponent<MeshFilter>();
        mesh.sharedMesh = tempGo.GetComponent<MeshFilter>().sharedMesh;

        go.AddComponent("MeshRenderer");
        Material diffuse = tempGo.GetComponent<MeshRenderer>().sharedMaterial;
        go.renderer.sharedMaterial = diffuse;

        DestroyImmediate(tempGo);

        CFTools.SelectObject(go);
        SceneView.RepaintAll();
    }

    [MenuItem("CF/Create/Sphere", false, 15)]
    static void Sphere()
    {
        GameObject tempGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject go = new GameObject("Sphere");
        go.transform.position = new Vector3(0, 0, 0);

        go.AddComponent("SphereProps");

        //go.AddComponent("SphereCollider");

        go.AddComponent("MeshFilter");
        MeshFilter mesh = go.GetComponent<MeshFilter>();
        mesh.sharedMesh = tempGo.GetComponent<MeshFilter>().sharedMesh;

        go.AddComponent("MeshRenderer");
        Material diffuse = tempGo.GetComponent<MeshRenderer>().sharedMaterial;
        go.renderer.sharedMaterial = diffuse;

        DestroyImmediate(tempGo);

        CFTools.SelectObject(go);
        SceneView.RepaintAll();
    }

    [MenuItem("CF/Create/Light (Direct)", false, 26)]
    static void Direct()
    {
        GameObject go = new GameObject("Light(Direct)");
        go.transform.position = new Vector3(-10, 5, 0);
        go.AddComponent(typeof(Light));
        Light light = go.GetComponent<Light>();
        light.type = LightType.Directional;
        //go.AddComponent("LookAtTarget");


        GameObject ct = new GameObject("Light(Direct)_Target");
        ct.transform.position = new Vector3(0, 0, 0);
        ct.AddComponent("CF_Properties");
        ct.GetComponent<CF_Properties>().gizmo = true;


        go.AddComponent("CF_Properties");
        go.GetComponent<CF_Properties>().lookAtTarget = ct.transform;

        CFTools.SelectObject(go);
        SceneView.RepaintAll();

    }

    [MenuItem("CF/Create/Light (Spot)", false, 27)]
    static void Spot()
    {
        GameObject go = new GameObject("Light(Spot)");
        go.transform.position = new Vector3(-10, 5, 0);
        go.AddComponent(typeof(Light));
        Light light = go.GetComponent<Light>();
        light.type = LightType.Spot;
        light.range = 100;

        GameObject ct = new GameObject("Light(Spot)_Target");
        ct.transform.position = new Vector3(0, 0, 0);
        ct.AddComponent("CF_Properties");
        ct.GetComponent<CF_Properties>().gizmo = true;


        go.AddComponent("CF_Properties");
        go.GetComponent<CF_Properties>().lookAtTarget = ct.transform;

        CFTools.SelectObject(go);
        SceneView.RepaintAll();


        CFTools.SelectObject(go);
        SceneView.RepaintAll();
    }

    [MenuItem("CF/Create/Camera", false, 47)]
    static void CameraPersp()
    {

        GameObject cam = new GameObject("Camera");
        cam.AddComponent(typeof(Camera));
        cam.transform.position = new Vector3(-10, 5, -10);

        GameObject ct = new GameObject("Camera_Target");
        ct.transform.position = new Vector3(0, 0, 0);
        ct.AddComponent("CF_Properties");
        ct.GetComponent<CF_Properties>().gizmo = true;


        cam.AddComponent("CF_Properties");
        cam.GetComponent<CF_Properties>().lookAtTarget = ct.transform;
        cam.GetComponent<CF_Properties>().gizmo = true;

        CFTools.SelectObject(cam);
        SceneView.RepaintAll();

    }


    // SELECT
    [MenuItem("CF/Select/Children")]
    static void SelectChildren()
    {
        CFTools.SelectChildren();
    }
    [MenuItem("CF/Select/Invert")]
    static void InvertSelection()
    {
        CFTools.InvertSelection();
    }

    // TOOLS
    [MenuItem("CF/Tools/DumpData")]
    static void DumpData()
    {
        if (Selection.activeGameObject != null)
        {

            CFTools.ClearConsole();
            Debug.Log("Active Game Object : " + Selection.activeGameObject);
            Debug.Log("Active Instance ID : " + Selection.activeInstanceID);

            Debug.Log("Active Object : " + Selection.activeObject);
            Debug.Log("Active Transform : " + Selection.activeTransform);

            Debug.Log("Asset GUIDs : " + Selection.assetGUIDs);
            Debug.Log("Game Objects : " + Selection.gameObjects);
            Debug.Log("Instance IDs : " + Selection.instanceIDs);
            Debug.Log("Objects : " + Selection.objects);
            Debug.Log("Transforms : " + Selection.transforms);
        }
        else
            Debug.Log("Nothing Selected.");
    }
    [MenuItem("CF/Tools/Clear Console &d")]
    static void ClearConHotKey()
    {
        CFTools.ClearConsole();
    }

    [MenuItem("CF/View/Hide")]
    static void Hide()
    {
        CFTools.Hide();
    }
    [MenuItem("CF/View/Unhide All")]
    static void UnHideAll()
    {
        CFTools.ExitIsolate();
    }

    
}
