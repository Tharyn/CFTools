using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CF_EmmissionController : MonoBehaviour {



    public GameObject LightSource;
    //public Transform PositionNull;
    public Color LightColor;
    public float LightIntensity;


    public float Multiplier = 1;
    public float Distance = 10;
    public Material TargetMaterial;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (LightSource != null)
        {
            LightIntensity = LightSource.GetComponent<Light>().intensity;
            LightColor = LightSource.GetComponent<Light>().color ;
            if (TargetMaterial != null && TargetMaterial.shader.name == "DM/Emis_00" || TargetMaterial.shader.name == "DM/DM_SS_Adv_Emis" || TargetMaterial.shader.name == "DM/DM_SS_Adv_Emis_LM")
            {
                TargetMaterial.SetColor("_LightColor", LightColor * Multiplier);
                TargetMaterial.SetFloat("_LightIntensity", LightIntensity);
                TargetMaterial.SetFloat("_LightDistance", Distance);
                TargetMaterial.SetVector("_LightPos", this.transform.position);

                //Debug.Log(this.transform.position);
            }
        }




	}
}
