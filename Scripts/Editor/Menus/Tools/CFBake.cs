using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngineInternal;




public class CFBake : MonoBehaviour {




    [MenuItem("CF/Bake/Bake Lightmaps")]
    static void BakeLightmaps() {
        // Get all the dual lights
        CF_DualLightProps[] DualLightProps = FindObjectsOfType(typeof(CF_DualLightProps)) as CF_DualLightProps[];
        Debug.Log(DualLightProps.Length);

        // Save Scene
        string path = EditorApplication.currentScene;
        EditorApplication.SaveScene();
        Debug.Log(path);


        // Make change to baked lights
        for (int i = 0; i < DualLightProps.Length; i++) {

            CF_DualLightProps DLP = DualLightProps[i];
            Light aLight = DualLightProps[i].gameObject.GetComponent<Light>();
            if (aLight != null) {



                if (DualLightProps[i].bkOn == true) {
                    SerializedObject serialObj = new SerializedObject(aLight);
                    SerializedProperty lightmapProp = serialObj.FindProperty("m_Lightmapping");

                    lightmapProp.intValue = 2;
                    serialObj.ApplyModifiedProperties(); 

                    aLight.range = DLP.bkRange;
                    aLight.color = DLP.bkColor;
                    aLight.intensity = DLP.bkIntensity;
                    aLight.shadows = DLP.bkShadows;
                } else {
                    aLight.enabled = false;
                }
            } else Debug.Log(DualLightProps[i].gameObject + " is missing a Light componant.");
               
        }


        // Kick off the bake
        Lightmapping.BakeAsync();

        // Restore Scene
        for (int i = 0; i < DualLightProps.Length; i++) {

            CF_DualLightProps DLP = DualLightProps[i];
            Light aLight = DualLightProps[i].gameObject.GetComponent<Light>();
            if (aLight != null) {


                if (DualLightProps[i].rtOn == true) {
                    SerializedObject serialObj = new SerializedObject(aLight);
                    SerializedProperty lightmapProp = serialObj.FindProperty("m_Lightmapping");

                    lightmapProp.intValue = 0;
                    serialObj.ApplyModifiedProperties(); 

                    aLight.range = DLP.rtRange;
                    aLight.color = DLP.rtColor;
                    aLight.intensity = DLP.rtIntensity;
                    aLight.shadows = DLP.rtShadows;
                } else {
                    aLight.enabled = false;
                }
            } else Debug.Log(DualLightProps[i].gameObject + " is missing a Light componant.");

        }

    }




	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
