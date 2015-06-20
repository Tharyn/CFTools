using UnityEngine;
//using UnityEditor;
//using System.Collections;


[ExecuteInEditMode]
public class CF_DualLightProps : MonoBehaviour {



    public bool rtOn = true;
    public int rtRange = 1;
    public Color rtColor = new Color(1, 1, 1);
    public int rtIntensity = 1;
    public bool rtShadows = false;

    public bool bkOn = true;
    public int bkRange = 1;
    public Color bkColor = new Color(1, 1, 1);
    public int bkIntensity = 1;
    public bool bkShadows = false; 



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
