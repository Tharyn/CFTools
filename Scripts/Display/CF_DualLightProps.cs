﻿using UnityEngine;
//using UnityEditor;
//using System.Collections;


[ExecuteInEditMode]
public class CF_DualLightProps : MonoBehaviour {



    public bool rtOn = true;
    public float rtRange = 1;
    public Color rtColor = new Color(1, 1, 1);
    public float rtIntensity = 1;
    public LightShadows rtShadows = LightShadows.None; 

    public bool bkOn = true;
    public float bkRange = 1;
    public Color bkColor = new Color(1, 1, 1);
    public float bkIntensity = 1;
    public LightShadows bkShadows = LightShadows.None; 



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
