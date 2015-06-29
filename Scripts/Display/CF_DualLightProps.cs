using UnityEngine;
//using UnityEditor;
//using System.Collections;

[RequireComponent(typeof(Light))]
[ExecuteInEditMode]
public class CF_DualLightProps : MonoBehaviour {



    public bool rtOn = true;
    public float rtRange = 20;
    public Color rtColor = new Color(1, 1, 1);
    public float rtIntensity = 1;
    public LightShadows rtShadows = LightShadows.None; 

    public bool bkOn = true;
    public float bkRange = 20;
    public Color bkColor = new Color(1, 1, 1);
    public float bkIntensity = 1;
    public LightShadows bkShadows = LightShadows.Soft;

    public bool reOn = true;
    public float reRange = 20;
    public Color reColor = new Color(1, 1, 1);
    public float reIntensity = 1;
    public LightShadows reShadows = LightShadows.Soft;


}
