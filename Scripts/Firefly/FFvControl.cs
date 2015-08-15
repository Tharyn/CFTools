using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mset;

[ExecuteInEditMode]
public class FFvControl : MonoBehaviour {

    
    public bool ForceUpdate = false;
    public Sky sky;
    
    public float GI_Amt = 0f;
    public float GI_Bias = 0f;
    public Color GI_Tint = Color.white;
    public float GI_RefAmt = 0f;


    //public float Ref_GI_Amt = 0f;
    //public Color Ref_GI_Tint = Color.white;
    //public bool reflectionRendering = false;
    

    // Was used for volume search. Now useing child search.
    // public GameObject volume;
    // public Bounds bounds;

    // If dynamic then the amount of GI is calculated from available lights, else it is fixed but the provided user values.
    public bool dynamic = false;
    public Light L1;
    public Light L2;
    public Light L3;
    public Light L4;

    public List<Material> Materials = new List<Material>();

    public List<GameObject> GoChildren = new List<GameObject>();

    /* FUNCTIONS */

    // RECURSIVLY GET ALL CHILDREN uses ref and palaces children in supplied list
    public static void CollectChildren(Transform GoTran, ref List<GameObject> GoChildren) {
        
        if (GoTran.childCount > 0)
            for (int i = 0; i < GoTran.childCount; i++)                 // FOR THE NUMBER OF CHILDREN(Transforms)
            {
                GoChildren.Add(GoTran.GetChild(i).gameObject);          // ADD THE GAME OBJECT THE TRANSFORM IS ATTACHED TO
                if (GoTran.GetChild(i).childCount != 0)                 // IF THE CHILD HAS CHILDREN RECURSE     
                    CollectChildren(GoTran.GetChild(i), ref GoChildren);
            }
    }

    // Find managed materials by shader name
    void CollectMaterials(List<GameObject> gos, ref List<Material> Materials ,string sName ) {
        Materials = new List<Material>();

        for (int i = 0; i < gos.Count; i++) {
            MeshRenderer thisRend = gos[i].GetComponent<MeshRenderer>();
            if (thisRend != null) {
                for (int j = 0; j < thisRend.sharedMaterials.Length; j++) {
                    //Debug.Log(thisRend);
                    string name = thisRend.sharedMaterials[j].shader.name;
                    //Debug.Log(name);
                    if (name == sName)
                        Materials.Add(thisRend.sharedMaterials[j]);
                }
            }
        }
    }

    // If the scene is dynamic then the RefAmb is tied to the GI_Amt
    void SetMaterialProperties() {
        for (int i = 0; i < Materials.Count; i++) {
            if (dynamic) {
                Materials[i].SetVector("_RoomAmb", (GI_Tint * GI_Amt));
                Materials[i].SetFloat("_AmbBias", GI_Bias);
                Materials[i].SetVector("_RefAmb", (GI_Tint * GI_Amt));
            } else {
                Materials[i].SetVector("_RoomAmb", (GI_Tint * GI_Amt));
                Materials[i].SetFloat("_AmbBias", GI_Bias);
                Materials[i].SetVector("_RefAmb", (GI_Tint * GI_RefAmt));
            }
        }
    }

    void UpdateDependants() {
        GoChildren = new List<GameObject>();
        CollectChildren(this.gameObject.transform, ref GoChildren);
        CollectMaterials(GoChildren, ref Materials, "Firefly/Firefly_Adv");
    }

    // Use this for initialization
    void Start() {
        UpdateDependants();
	}
	
	// Update is called once per frame
	void Update () {
        if (ForceUpdate == true) {
            UpdateDependants();
            ForceUpdate = false;
        }
        SetMaterialProperties();
	}
}
