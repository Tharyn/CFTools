﻿using UnityEngine;
using UnityEditor; 
using System.Collections;
using mset;

public class CF_Bridge : EditorWindow {

    int timer = 0;
    private bool Active = false;
    string text = "Disabled";

    public static bool run = false;
    public static EditorWindow window;

    [MenuItem("CF/THE BRIDGE")]
    public static void Launch() {
         window = GetWindow(typeof(CF_Bridge));

    }


    // Copys from the light to the Realtime Property storage
    void UpdateRtProps() {
        Debug.Log("Updating RT lights");
        CF_DualLightProps[] DualLightProps = FindObjectsOfType(typeof(CF_DualLightProps)) as CF_DualLightProps[];

        for (int i = 0; i < DualLightProps.Length; i++) {
            CF_DualLightProps DLP = DualLightProps[i];
            Light aLight = DualLightProps[i].gameObject.GetComponent<Light>();
            DLP.rtOn = aLight.enabled;
            DLP.rtRange = aLight.range;
            DLP.rtColor = aLight.color;
            DLP.rtIntensity = aLight.intensity;
            DLP.rtShadows = aLight.shadows;
            EditorUtility.SetDirty(DLP);
            EditorUtility.SetDirty(DLP.gameObject.light);
        }

           
    }


    static public void SetLights(int type, bool renderProbe, Sky[] skys) {

        CF_DualLightProps[] DualLightProps = FindObjectsOfType(typeof(CF_DualLightProps)) as CF_DualLightProps[];

        for (int i = 0; i < DualLightProps.Length; i++) {

            CF_DualLightProps DLP = DualLightProps[i];
            Light aLight = DualLightProps[i].gameObject.GetComponent<Light>();
            SerializedObject serialObj = new SerializedObject(aLight);
            SerializedProperty lightmapProp = serialObj.FindProperty("m_Lightmapping");

            switch (type) {
                case 0: { // REAL TIME
                    Debug.Log("Setting Realtime Lights");
                    lightmapProp.intValue = 0;
                    serialObj.ApplyModifiedProperties();
                    aLight.enabled =  DLP.rtOn;
                    aLight.range = DLP.rtRange;
                    aLight.color = DLP.rtColor;
                    aLight.intensity = DLP.rtIntensity;
                    aLight.shadows = DLP.rtShadows;

                    break;
                }
                case 1: { // LIGHTMAPPING
                    lightmapProp.intValue = 2; // Set Lightmapping dropdown
                    serialObj.ApplyModifiedProperties(); // Set Lightmapping dropdown
                    aLight.enabled = DLP.bkOn;
                    aLight.range = DLP.bkRange;
                    aLight.color = DLP.bkColor;
                    aLight.intensity = DLP.bkIntensity;
                    aLight.shadows = DLP.bkShadows;

                    break;
                }
                case 2: { // REFLECTIOPN PROBE
                    Debug.Log("Setting Reflection Lights");
                    lightmapProp.intValue = 0;
                    serialObj.ApplyModifiedProperties();
                    aLight.enabled = DLP.reOn;
                    aLight.range = DLP.reRange;
                    aLight.color = DLP.reColor;
                    aLight.intensity = DLP.reIntensity;
                    aLight.shadows = DLP.reShadows;

                    break;
                }
            }


        }

    }
	

    void BakeLightmaps() {

        // Store Current Realtime Lighting
        UpdateRtProps();

        SetLights(1, false, new Sky[1]);

        Lightmapping.BakeAsync();

        SetLights(0, false, new Sky[1]);

   
    }

    /*
    public static IEnumerator WaitForRender(CF_Bridge bridge, Sky[] skys) {
        // A->B     Slerp
        // A<->B    PingPong

        while (Probeshop.probing == true) {
            yield return null;
        }
        
    }
    */


    void RenderSky() {
        
        if (Selection.gameObjects.Length > 0) {
            GameObject go = Selection.gameObjects[0];
            Sky sky = go.GetComponent<Sky>();

            if (sky != null) {
                Sky[] skys = new Sky[1];
                skys[0] = sky;
                UpdateRtProps();
                
                
                Probeshop.ProbeSkies(null, skys, false, false, null);
                
                
            } else
                Debug.Log("Please select a Sky");
        } else 
            Debug.Log("Please select a Sky");
        
    }

    public void OnGUI() {
        if (Active = GUI.Toggle(new Rect(10, 10, 100, 30), Active, text))
            text = "ACTIVE";
        else
            text = "Disabled";

        if (GUI.Button(new Rect(10, 30, 160, 20), "Bake Lightmaps")) {
            if (EditorUtility.DisplayDialog("Bake Lightmaps", "Are you sure?", "Yes", "Cancle")) 
                BakeLightmaps();
            
        }

        if (GUI.Button(new Rect(10, 55, 160, 20), "Render Sky"))
            if (EditorUtility.DisplayDialog("Render Sky", "Are you sure?", "Yes", "Cancle")) 
                RenderSky();
        

        if (GUI.Button(new Rect(10, 80, 160, 20), "Update RT Lights"))
            UpdateRtProps();
    }


    // Update is called once per frame
    void Update() {
        if (Active) {
            if (!Application.isPlaying) {
                timer += 1;
                if (timer > 50) {
                    timer = 0;
                    AssetDatabase.Refresh();
                }

            }
        }
    }
}
