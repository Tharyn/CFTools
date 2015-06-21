using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[InitializeOnLoad]
class MyHierarchyIcon
{
    static Texture2D texture;
    static List<Texture2D> icons = new List<Texture2D>();

    static List<int> markedObjects;
    static List<bool> hasChild;
    static List<int> iconToUse;
    static List<int> depth;
    



    // This method is run on initilization. It shares the same name as the class
    static MyHierarchyIcon()
    {


        //icons.Add("SpotLight.ico.png");
        // Init
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/SpotLight.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/Mesh.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/Sky.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/Camera.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/Null.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/Audio.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/Collider.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/OmniLight.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/SkySystem.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        texture = AssetDatabase.LoadAssetAtPath("Assets/CFTools/GUIicons/Target.ico.png", typeof(Texture2D)) as Texture2D;
        icons.Add(texture);
        EditorApplication.update += UpdateCB;
        //EditorApplication.hierarchyWindowChanged += UpdateCB;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        
    }

    static int FindDepth(GameObject Go, int depth)
    {
        if (Go.transform.parent == null)
            return (depth);
        else {
            return (FindDepth(Go.transform.parent.gameObject, (depth + 1)));
        }
    }




    // This method is attached to the EditorApplication's 'update'
    static void UpdateCB()
    {
        
        // Check here
        GameObject[] go = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        markedObjects = new List<int>();
        iconToUse = new List<int>();
        depth = new List<int>();
        hasChild = new List<bool>();
        foreach (GameObject g in go)
        {
            
            //Debug.Log(FindDepth(g, 0));
            //if (g.name == "First Person Controller")
               // Debug.Log(FindDepth(g, 0));

            // Example: mark all lights
            if (g.GetComponent<Light>() != null)
            {
                markedObjects.Add(g.GetInstanceID());
                if (g.GetComponent<Light>().type == LightType.Spot) 
                    iconToUse.Add(0);
                else
                    iconToUse.Add(7);


                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);
            }
            if (g.GetComponent<AudioSource>() != null)
            {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(5);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);

            }  else if (g.GetComponent<Collider>() != null) {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(6);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);
            } else if (g.GetComponent<MeshRenderer>() != null) {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(1);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);
            }


            // Example: mark all lights
            if (g.GetComponent("Sky") != null)
            {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(2);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);
            }
            // Example: mark all lights
            if (g.GetComponent("SkyManager") != null)
            {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(8);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);
            }
            // Example: mark all lights
            if (g.GetComponent<Camera>() != null)
            {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(3);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);
            }
            // Example: mark all lights
            if (g.name.IndexOf(".Target") > -1)
            {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(9);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
                else
                    hasChild.Add(true);
            } else if (g.GetComponent<CF_Properties>() != null) {
                markedObjects.Add(g.GetInstanceID());
                iconToUse.Add(4);
                depth.Add(FindDepth(g, 0));
                if (g.transform.childCount == 0)
                    hasChild.Add(false);
            }
            // Example: mark all lights
              
        }

    }

    // This method is attached to the EditorApplication's 'hierarchyWindowItemOnGUI'
    static void HierarchyItemCB(int instanceID, Rect selectionRect)
    {

        bool cont = markedObjects.Contains(instanceID);
        int index = markedObjects.IndexOf(instanceID);

        if (cont)
        {
            Rect r = new Rect(selectionRect);
            if (depth[index] != 0) // If not in the root node
            {

                // place the icon to the right of the list:
                

                r.x = depth[index] * 14;//r.width - 20;

                if (hasChild[index])
                    r.x -= 14;

                r.width = 20;

                // Draw the texture if it's a light (e.g.)
                Texture2D tempTex = icons[iconToUse[index]];
                GUI.Label(r, tempTex);
            }
            else if (hasChild[index] != true) // If Root node but no children
            {
                r.x = 0;
                r.width = 20;
                Texture2D tempTex = icons[iconToUse[index]];
                GUI.Label(r, tempTex);
            }
        }
    }

}