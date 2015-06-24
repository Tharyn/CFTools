using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mset;

[ExecuteInEditMode]
public class CF_RoomLighting : MonoBehaviour {



    public GameObject volume;
    public Bounds bounds;
    public Color GI;
    public Shader shader;
    public Sky sky;
    public List<Material> Materials = new List<Material>();
    public Light L1;
    public float L1rangeM = 1;

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
        if (L1 != null) {
            for (int i = 0; i < Materials.Count; i++) {
                Materials[i].SetFloat("_L1Intensity", L1.intensity);
                Materials[i].SetFloat("_L1Falloff", L1.range * L1rangeM);
                Materials[i].SetVector ("_L1Pos", L1.gameObject.transform.position);
                
            }

        }

	}
}
