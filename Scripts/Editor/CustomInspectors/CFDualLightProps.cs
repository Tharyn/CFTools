using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor (typeof(Light))]
public class CFDualLightProps : Editor {

    public bool rtFoldout = true;
    public bool rtOn = true;
    public Color rtColor = new Color(1, 1, 1);
    public int rtIntensity = 1;
    public int rtRange = 1;
    public bool rtShadows = false;

    public bool bkFoldout = true;
    public bool bkOn = true;
    public Color bkColor = new Color(1, 1, 1);
    public int bkIntensity = 1;
    public int bkRange = 1;
    public bool bkShadows = false; 

    public override void OnInspectorGUI()
    {
       base.OnInspectorGUI(); // Keeps Existing GUI
       //Light test = (Light)target;

       rtFoldout = EditorGUILayout.Foldout(rtFoldout, "REALTIME:");
       //EditorGUILayout.LabelField("Real Time:");
       rtOn = EditorGUILayout.Toggle("ON", rtOn);
       rtColor = EditorGUILayout.ColorField("Color", rtColor);
       rtIntensity =  Mathf.Clamp  ( EditorGUILayout.IntField("Intensity", rtIntensity), 0, 20)   ;
       rtRange = Mathf.Clamp  ( EditorGUILayout.IntField("Range", rtRange), 1, 1000);
       rtShadows = EditorGUILayout.Toggle("Shadows", rtShadows);

       bkFoldout = EditorGUILayout.Foldout(bkFoldout, "BAKEING:");

       bkOn = EditorGUILayout.Toggle("ON", bkOn);
       bkColor = EditorGUILayout.ColorField("Color", bkColor);
       bkIntensity = Mathf.Clamp(EditorGUILayout.IntField("Intensity", bkIntensity), 0, 20);
       bkRange = Mathf.Clamp(EditorGUILayout.IntField("Range", bkRange), 1, 1000);
       bkShadows = EditorGUILayout.Toggle("Shadows", bkShadows);




    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
