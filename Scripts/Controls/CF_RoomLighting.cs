using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mset;

[ExecuteInEditMode]
public class CF_RoomLighting : MonoBehaviour {



    public GameObject volume;
    public Bounds bounds;
    public Color GIColor;
    public float GIAmt;
    public Shader shader;
    public Sky sky;
    public List<Material> Materials = new List<Material>();
    public Light L1;
    public Light L2;
    //public float L1rangeM = 1;

    void CollectMaterials() {
        Materials = new List<Material>();
        MeshRenderer[] mRenderers = FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];

        for (int i = 0; i < mRenderers.Length; i++) {

            if ( bounds.Contains(mRenderers[i].gameObject.transform.position) ) {
                Debug.Log(mRenderers[i].gameObject);
                
                for (int j = 0; j < mRenderers[i].sharedMaterials.Length; j++ ) {
                    if (mRenderers[i].sharedMaterials[j].shader.name == shader.name)
                        Materials.Add(mRenderers[i].sharedMaterials[j]);
                }
            }

        }
    }

    // Use this for initialization
    void Start() {
       
        bounds = volume.renderer.bounds;
        CollectMaterials();

	}
	
	// Update is called once per frame
	void Update () {
        if (L1 != null && L2 != null) {
    
            Color giTotal = GIColor * GIAmt * (L1.intensity + L2.intensity);

            for (int i = 0; i < Materials.Count; i++) {
                Materials[i].SetVector("_RoomAmb", giTotal);

                Materials[i].SetFloat("_L1Intensity", L1.intensity);
                Materials[i].SetFloat("_L2Intensity", L2.intensity);
                
                Materials[i].SetVector ("_L1Pos", L1.gameObject.transform.position);
                Materials[i].SetVector("_L2Pos", L2.gameObject.transform.position);

                //Materials[i].SetFloat("_L1Falloff", L1.range * L1rangeM);
                
            }

        }

	}
}
