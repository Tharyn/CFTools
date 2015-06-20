using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)
using System;
using System.IO;

public class FBXscaleImport2 : AssetPostprocessor
{

    public static List<string> texIDs;
    public static List<string> texNames;
    public char[] delimiterChars = { '(', ')', ','};

    void OnPreprocessModel()
    {
        if (assetPath.Contains(".max."))
        {
            ModelImporter importer = assetImporter as ModelImporter;
            importer.globalScale = 0.0833333f;
        }
        if (assetPath.Contains(".ma."))
        {
            ModelImporter importer = assetImporter as ModelImporter;
            importer.globalScale = 0.0833333f;
        }
        if (assetPath.Contains("(Sets)"))
        {
            ModelImporter importer = assetImporter as ModelImporter;
            importer.generateSecondaryUV = true;
            importer.secondaryUVPackMargin = 8;
        }

    }


    // RECURSIVLY GET ALL CHILDREN uses ref and palaces children in supplied list
    public static void CollectChildren(Transform GoTran, ref List<GameObject> GoChildren)
    {
        if (GoTran.childCount > 0)
            for (int i = 0; i < GoTran.childCount; i++)           // FOR THE NUMBER OF CHILDREN(Transforms)
            {
                GoChildren.Add(GoTran.GetChild(i).gameObject);  // ADD THE GAME OBJECT THE TRANSFORM IS ATTACHED TO
                if (GoTran.GetChild(i).childCount != 0)           // IF THE CHILD HAS CHILDREN RECURSE     
                    CollectChildren(GoTran.GetChild(i), ref GoChildren);
            }
    }

    // Get the PATH part of the file
    public static string GetFilePath(string aPath)
    {
        string path = "";

        char[] slash = { '/'};
        string[] args = aPath.Split(slash);

        for (int i = 0; i < args.Length-1; i++)
            path += args[i] + "/";

        return path;
    }

    // Get the file NAME part of the file, with or without the extension
    public static string GetFileName(string aPath, bool withExt)
    {
        string file = "";

        char[] slash = { '/' };
        string[] args = aPath.Split(slash);

        if (withExt)
            file = args[args.Length - 1];
        else {
            char[] dots = { '.' };
            string[] parts = args[args.Length - 1].Split(dots);
            file += parts[0] + "." + parts[1];
        }

        return file;
    }


    
    // Does file exist
    bool DoesAssetExits(string[] Paths, string fileName){

        string[] tempArray = AssetDatabase.FindAssets(fileName, Paths);

        if (tempArray.Length > 0)
            return true;
        else
            return false;
    }


    // Read node data file
    static void MaxDataParser(ref List<string> Keys, ref List<string> Values, string dataFile)
    {
        //Debug.Log("In MaxDataParser");
        //string cacheLocation = (Application.dataPath + "/(Cache)/FromMax.cache");
        StreamReader reader = new StreamReader(File.OpenRead(@dataFile));

        //Debug.Log(reader);
        //Debug.Log(reader.EndOfStream);

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            

            string[] values = line.Split(':');

            //Debug.Log(values[0]);
            Keys.Add(values[0]);

            //Debug.Log(values[1]);
            Values.Add(values[1]);
        }
    }


    // FBXscaleImport2 importer 2.0
    void OnPostprocessModel(GameObject go) {
        //Debug.Log(assetPath);
        //Debug.Log(GetFilePath(assetPath));
        //Debug.Log(GetFileName(assetPath, false));

        if ( assetPath.Contains("(Sets)")) {
            // Get all children
            List<GameObject> GoChildren = new List<GameObject>();
            GoChildren.Add(go); // ADD SELF
            CollectChildren(go.transform, ref GoChildren);
            
            string[] Paths = new string[1];
            Paths[0] = (GetFilePath(assetPath) + "Data");
            //Debug.Log(Paths[0] );
            //Debug.Log( DoesAssetExits(Paths, GoChildren[3].name));

            // For i in Children read data files
            // FOR EACH GAME OBJECT
            for (int i = 0; i < GoChildren.Count; i++)
            {
                // Has data file ?
                if (DoesAssetExits(Paths, GoChildren[i].name))
                {
                    List<string> Keys = new List<string>();
                    List<string> Values = new List<string>();
                    string filePath = Paths[0] + "/" + GoChildren[i].name + ".txt";
                    MaxDataParser(ref Keys, ref Values, filePath);

                    /* Get CLASS index
                    int index = Keys.FindIndex(s => s.Contains("Class"));
                    if (index > -1)
                        Debug.Log(GoChildren[i].name);
                    */
                    //Debug.Log(Values[0]);
                    int index = Values.FindIndex(s => s.Contains("Light"));
                    if (index > -1)
                        Debug.Log(GoChildren[i].name);
                }
                //if (Keys[0] == "Light")
            }


            /* If a data file exists for a node
            if (DoesAssetExits(Paths, GoChildren[3].name))
            {
                List<string> Keys = new List<string>();
                List<string> Values = new List<string>();
                string filePath = Paths[0] + "/" + GoChildren[3].name + ".txt";
                MaxDataParser(ref Keys, ref Values, filePath);
            }
            */
            
            
        }
    }












    /*
    GameObject FindTarget(List<GameObject> GoChildren, String name)
    {
        GameObject to = null;
        for (int j = 0; j < GoChildren.Count; j++)
        {
            if (GoChildren[j].name.IndexOf(name) > -1 && GoChildren[j].name.IndexOf("Target") > -1)
                to = GoChildren[j];
        }
        Debug.Log(to);
        return to;
    }

    
    void OnPostprocessModel( GameObject go) {
        if (assetPath.Contains(".max.") && assetPath.Contains("Props") || assetPath.Contains("SetDress") || assetPath.Contains("Environment") || assetPath.Contains("(Sets)"))
        {

            // Get all children to check for mateials that may have been imported
            List<GameObject> GoChildren = new List<GameObject>();
            GoChildren.Add (go); // ADD SELF
            CollectChildren(go.transform, ref GoChildren);

            // FOR EACH GAME OBJECT
            for (int j = 0; j < GoChildren.Count; j++) {

                // IF NOT GLASS MARK AS STATIC
                if (GoChildren[j].name.IndexOf("TranFX") > -1) {
                    GoChildren[j].layer = LayerMask.NameToLayer("TransparentFX");
                    GoChildren[j].isStatic = false;
                }
                else if (GoChildren[j].name.IndexOf("PropGo") > -1)
                {
                    GoChildren[j].layer = LayerMask.NameToLayer("PropGo");
                    GoChildren[j].isStatic = false;
                   if (GoChildren[j].name.IndexOf("Ridged") > -1)
                       GoChildren[j].AddComponent<Rigidbody>();
                }
                else if (GoChildren[j].name.IndexOf("PropRayCast") > -1)
                {
                    GoChildren[j].layer = LayerMask.NameToLayer("PropRayCast");
                    GoChildren[j].isStatic = false;
                } 
                else
                {
                    GoChildren[j].layer = LayerMask.NameToLayer("Ignore Raycast");
                    GoChildren[j].isStatic = true;
                }


                

                // ARGUMENTS
                string[] args = GoChildren[j].name.Split(delimiterChars);

                // SPOT
                if (GoChildren[j].name.IndexOf("SpotAUTO") > -1 && GoChildren[j].name.IndexOf(".Target") < 0)
                {
                    
                    if (GoChildren[j].GetComponent<Light>() == null)
                    {
                        Light lightComp = GoChildren[j].AddComponent<Light>();
                        
                        lightComp.type = LightType.Spot;
                        if (args[1] == "true")
                            lightComp.shadows = LightShadows.Soft;

                        lightComp.intensity = Convert.ToSingle(args[2]);
                        lightComp.color = new Color(Convert.ToSingle(args[3]) / 255, Convert.ToSingle(args[4]) / 255, Convert.ToSingle(args[5]) / 255);
                        lightComp.range = Convert.ToSingle(args[6]);
                        lightComp.cullingMask = Convert.ToInt32(args[7]);
                        lightComp.spotAngle = Convert.ToSingle(args[8]);
                    }
                    if (GoChildren[j].GetComponent<CF_Properties>() == null)
                    {
                        CF_Properties tempCompA = GoChildren[j].AddComponent<CF_Properties>();
                        GameObject to = FindTarget(GoChildren, args[0]);
                        if (to != null)
                            tempCompA.lookAtTarget = to.transform;
                    }
                }

                // SPOT
                if (GoChildren[j].name.IndexOf("SpotRT") > -1 && GoChildren[j].name.IndexOf(".Target") < 0)
                {
                    if (GoChildren[j].GetComponent<Light>() == null)
                    {
                        GoChildren[j].transform.rotation = Quaternion.Euler(180,0,0);
                        Light lightComp = GoChildren[j].AddComponent<Light>();

                        lightComp.type = LightType.Spot;
                        if (args[1] == "true")
                            lightComp.shadows = LightShadows.Soft;

                        lightComp.intensity = Convert.ToSingle(args[2]);
                        lightComp.color = new Color(Convert.ToSingle(args[3]) / 255, Convert.ToSingle(args[4]) / 255, Convert.ToSingle(args[5]) / 255);
                        lightComp.range = Convert.ToSingle(args[6]);
                        lightComp.cullingMask = Convert.ToInt32(args[7]);
                        lightComp.spotAngle = Convert.ToSingle(args[8]);
                    }
                    if (GoChildren[j].GetComponent<CF_Properties>() == null)
                    {
                        CF_Properties tempCompA = GoChildren[j].AddComponent<CF_Properties>();
                        GameObject to = FindTarget(GoChildren, args[0]);
                        if (to != null)
                            tempCompA.lookAtTarget = to.transform;
                    }
                }
                // FILL
                if (GoChildren[j].name.IndexOf("Fill") > -1 )
                {
                    if (GoChildren[j].GetComponent<Light>() == null)
                    {
                        Light lightComp = GoChildren[j].AddComponent<Light>();



                        lightComp.type = LightType.Point;
                        if (args[1] == "true")
                            lightComp.shadows = LightShadows.Soft;

                        lightComp.intensity = Convert.ToSingle(args[2]);
                        lightComp.color = new Color(Convert.ToSingle(args[3]), Convert.ToSingle(args[4]), Convert.ToSingle(args[5]));
                        lightComp.range = Convert.ToSingle(args[6]);
                        lightComp.cullingMask = Convert.ToInt32(args[7]);
                    }

                }

                // MeshCollider
                if (GoChildren[j].name.IndexOf("ColliderMesh") > -1) {
                    if (GoChildren[j].GetComponent<MeshCollider>() == null ) {
                        GoChildren[j].AddComponent<MeshCollider>();
                    }
                    GoChildren[j].renderer.enabled = false;
                    
                }

                // ColliderCapsule
                if (GoChildren[j].name.IndexOf("ColliderCapsule") > -1)
                {
                    CapsuleCollider tempCompA = GoChildren[j].GetComponent<CapsuleCollider>();
                    if (tempCompA == null)
                    {
                        tempCompA = GoChildren[j].AddComponent<CapsuleCollider>();
                    }

                    // AXIS
                    if (args[2] == "x")
                        tempCompA.direction = 0;
                    else if (args[2] == "y")
                        tempCompA.direction = 1;
                    else tempCompA.direction = 2;

                    tempCompA.radius = Convert.ToSingle(args[3]);
                    tempCompA.height = Convert.ToSingle(args[4]);
                    if (Convert.ToBoolean(args[5]))
                    {
                        GoChildren[j].renderer.enabled = false;
                    }
                }

                // ColliderBox
                if (GoChildren[j].name.IndexOf("ColliderBox") > -1)
                {
                    BoxCollider tempCompA = GoChildren[j].GetComponent<BoxCollider>();
                    if (tempCompA == null)
                    {
                        tempCompA = GoChildren[j].AddComponent<BoxCollider>();
                    }

                     GoChildren[j].renderer.enabled = false;
                    
                }

                // ColliderBox
                if (GoChildren[j].name.IndexOf("ColliderSphere") > -1)
                {
                    SphereCollider tempCompA = GoChildren[j].GetComponent<SphereCollider>();
                    if (tempCompA == null)
                    {
                        tempCompA = GoChildren[j].AddComponent<SphereCollider>();
                    }

                    GoChildren[j].renderer.enabled = false;

                }

                // CONTROL LIGHT FOR EMMIS MATERIAL
                if (GoChildren[j].name.IndexOf("ControlLight") > -1)
                {
                    if (GoChildren[j].GetComponent<CF_Properties>() == null)
                    {
                        CF_Properties tempCompA = GoChildren[j].AddComponent<CF_Properties>();
                        tempCompA.size = .1f;
                        if (GoChildren[j].GetComponent<CF_EmmissionController>() == null)
                        {
                            GoChildren[j].AddComponent<CF_EmmissionController>();
                        }
                    }
                }

                // MATERIAL RELATED 
                if (GoChildren[j].renderer != null)
                {
                    Material[] mats = GoChildren[j].renderer.sharedMaterials;

                    for (int k = 0; k < mats.Length; k ++)
                    {
                        Material mat = mats[k];
                        string[] strSpl = new string[] { "(" };
                        string matName = mat.name.Split(strSpl, StringSplitOptions.RemoveEmptyEntries)[0];
                        
                        
                        // SHADER ASSIGNMENT
                        if (mat.name.IndexOf("EmisStatic") > -1)  // EmisStatic
                        {
                            mat.shader = Shader.Find("DM2/DM_SS_Adv_Emis_LM");
                        }
                        else if (mat.name.IndexOf("EmisWmap") > -1) // EmisWmap
                        {
                            mat.shader = Shader.Find("DM2/DM_SS_Adv_EmisWmap");
                        }
                        else if (mat.name.IndexOf("LightCone") > -1) // LightCone
                        {
                            mat.shader = Shader.Find("DM2/DM_LightCone_A"); 
                        }
                        else if (mat.name.IndexOf("GeoRefract") > -1) // GeoRefract
                        {
                            mat.shader = Shader.Find("DM2/DM_Geo_Refract");
                        }
                        else if (mat.name.IndexOf("SimpReflect") > -1) // SimpReflect
                        {
                            mat.shader = Shader.Find("DM2/DM_Simple_Reflect");
                        }
                        else if (mat.name.IndexOf("MoonBeams") > -1) // MoonBeams
                        {
                            mat.shader = Shader.Find("DM2/DM_MoonBeams");
                        }
                        else if (mat.name.IndexOf("Refract") > -1) // Refract
                        {
                            mat.shader = Shader.Find("DM2/DM_SS_Refract");
                        }
                        else if (mat.name.IndexOf("Mirror") > -1) // Mirror
                        {
                            mat.shader = Shader.Find("DM2/DM_Adv_SS");
                        }
                        else if (mat.name.IndexOf("Cloth") > -1 || mat.name.IndexOf("Velvet") > -1) // Cloth
                        {
                            mat.shader = Shader.Find("DM2/DM_Cloth_LM");
                        }
                        else if (mat.name.IndexOf("SSProp") > -1) // SSProp
                        {
                            mat.shader = Shader.Find("DM2/DM_Adv_SS");
                        }
                        else if (mat.name.IndexOf("Collider") > -1 || mat.name.IndexOf("RidgedBody") > -1) // Collider
                        {
                            mat.shader = Shader.Find("DM2/Collider");
                        }
                        else if (mat.name.IndexOf("Glass_DB") > -1 )
                        {
                            mat.shader = Shader.Find("DM2/DM_SS_GlassDB");
                        }
                        else if (mat.name.IndexOf("Adv_Metal_LM") > -1) // ADD METAL SHADER WHEN IOR IS FIXED
                        {
                            mat.shader = Shader.Find("DM2/DM_SS_Adv_LM");
                        }
                        else if (mat.name.IndexOf("NoNormal") > -1)
                        {
                            mat.shader = Shader.Find("DM2/DM_SS_Adv_LM_NN");
                        }
                        else
                        {
                            mat.shader = Shader.Find("DM2/DM_SS_Adv_LM");
                        }


                        string[] tempArray = AssetDatabase.FindAssets(matName + " t:texture2D");

                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            string assetPathStr = AssetDatabase.GUIDToAssetPath(tempArray[i]);
                            Texture2D tempTex = Resources.LoadAssetAtPath(assetPathStr, typeof(Texture2D)) as Texture2D;


                            if (assetPathStr.Contains("Albedo"))
                            {
                                mat.SetTexture("_MainTex", tempTex);
                            }
                            if (assetPathStr.Contains("Spec"))
                            {
                                mat.SetTexture("_SpecMap", tempTex);
                            }
                            if (assetPathStr.Contains("AO"))
                            {
                                mat.SetTexture("_AOMap", tempTex);
                            }
                            if (assetPathStr.Contains("Norm"))
                            {
                                mat.SetTexture("_BumpMap", tempTex);
                            }
                            //SetTexture(string propertyName, Texture texture); 
                            //texIDs.Add (tempArray[i]);
                            //texNames.Add (AssetDatabase.GUIDToAssetPath(tempArray[i]));
                        }
                    }
                     
                }
                string[] strSpl2 = new string[] { "(" };
                string GoName = GoChildren[j].name.Split(strSpl2, StringSplitOptions.RemoveEmptyEntries)[0];
                GoChildren[j].name = GoName;
                
            }

        }
    }

    */
}
