using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;
using mset;
public class CFExtractCubeMap : ScriptableWizard {

    public Cubemap cubemap;
    Texture2D drawTexture;
    byte[] bytes;


    void OnWizardUpdate () {
        //TV string helpString = "Select cubemap to save to individual png";
        //TV bool isValid = (cubemap != null);
    }


    void FlipTextureVertically() {


    }

    void FlipTextureHorozontally() {


    }

    void OnWizardCreate ()
    {
        //GameObject go = Selection.activeGameObject;
        //if (go != null)
        {
            //string skyTexName = go.GetComponent<Sky>().SpecularCube.name ;
            //cubemap = (Cubemap)go.GetComponent<Sky>().SpecularCube;
            //Debug.Log( AssetDatabase.GetAssetPath(go.GetComponent<Sky>().SpecularCube ));

            
           // string[] tempArray = AssetDatabase.FindAssets(mat.name + " t:texture2D");

            //string assetPathStr = AssetDatabase.GUIDToAssetPath(tempArray[i]);

            //Cubemap dTexture = (Cubemap)cubemap;
            //drawTexture = (Texture2D)cubemap;
            //Debug.Log(skyTexName);
            Debug.Log(cubemap);
        

            int width = cubemap.width;
            int height = cubemap.height;
 
            //Debug.Log(Application.dataPath + "/" +cubemap.name +"_PositiveX.png");
            Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
     
            tex.SetPixels(cubemap.GetPixels(CubemapFace.PositiveX));   
            bytes = tex.EncodeToPNG();      
            File.WriteAllBytes(Application.dataPath + "/"  + cubemap.name +"_PositiveX.png", bytes);       
 

            tex.SetPixels(cubemap.GetPixels(CubemapFace.NegativeX));
            bytes = tex.EncodeToPNG();     
            File.WriteAllBytes(Application.dataPath + "/"  + cubemap.name +"_NegativeX.png", bytes);       
 
            tex.SetPixels(cubemap.GetPixels(CubemapFace.PositiveY));
            bytes = tex.EncodeToPNG();     
            File.WriteAllBytes(Application.dataPath + "/"  + cubemap.name +"_PositiveY.png", bytes);       
 
            tex.SetPixels(cubemap.GetPixels(CubemapFace.NegativeY));
            bytes = tex.EncodeToPNG();     
            File.WriteAllBytes(Application.dataPath + "/"  + cubemap.name +"_NegativeY.png", bytes);       
 
            tex.SetPixels(cubemap.GetPixels(CubemapFace.PositiveZ));
            bytes = tex.EncodeToPNG();     
            File.WriteAllBytes(Application.dataPath + "/"  + cubemap.name +"_PositiveZ.png", bytes);       
 
            tex.SetPixels(cubemap.GetPixels(CubemapFace.NegativeZ));
            bytes = tex.EncodeToPNG();     
            File.WriteAllBytes(Application.dataPath + "/"  + cubemap.name +"_NegativeZ.png", bytes);       
 

            UnityEngine.Object.DestroyImmediate(tex);   
        }

    }


    [MenuItem("CF/Tools/Save CubeMap To Png ")]
    static void CreateWizard()
    {
     
        ScriptableWizard.DisplayWizard<CFExtractCubeMap>("Save CubeMap To Png", "CFExtractCubeMap");
    }

}
