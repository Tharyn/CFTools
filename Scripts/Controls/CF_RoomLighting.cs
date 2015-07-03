using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mset;

[ExecuteInEditMode]
public class CF_RoomLighting : MonoBehaviour {

    public bool reflectionRendering = false;
    public float GI_Amt;
    public Color GI_Tint;
    public Color Ref_Ambient;
    public Color ReflectionGI = Color.white;

    public GameObject volume;
    public Bounds bounds;
    public Shader shaderLM;
    public Shader shaderEM;
    public Shader shaderPX;

    public Shader shaderSS;

    public Light L1;
    public Light L2;

    // ADD MY OWN SKY MANAGER !!!
    public Sky sky;

    public List<Material> Materials = new List<Material>();

    void CollectMaterials() {
        Materials = new List<Material>();
        MeshRenderer[] mRenderers = FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];

        for (int i = 0; i < mRenderers.Length; i++) {
            if ( bounds.Contains(mRenderers[i].gameObject.transform.position) ) {
                for (int j = 0; j < mRenderers[i].sharedMaterials.Length; j++ ) {
                    string name = mRenderers[i].sharedMaterials[j].shader.name;
                    if (name == shaderLM.name || name == shaderEM.name || name == shaderPX.name || name == shaderSS.name)
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


    void SetMaterialProperties() {
        if (L1 != null && L2 != null) {

            // STUPID EXPENSIVE
            //CollectMaterials();

            float L1mag;
            if (L1.enabled && L1.gameObject.activeSelf)
                L1mag = L1.intensity;
            else
                L1mag = 0;

            float L2mag;
            if (L2.enabled && L2.gameObject.activeSelf)
                L2mag = L2.intensity;
            else
                L2mag = 0;

            Color giTotal = GI_Tint * GI_Amt * (L1mag + L2mag);


            for (int i = 0; i < Materials.Count; i++) {
                Materials[i].SetVector("_RoomAmb", giTotal);

                Materials[i].SetFloat("_L1Intensity", L1mag);
                Materials[i].SetFloat("_L2Intensity", L2mag);

                Materials[i].SetVector("_L1Color", L1.color);
                Materials[i].SetVector("_L2Color", L2.color);

                Materials[i].SetVector("_L1Pos", L1.gameObject.transform.position);
                Materials[i].SetVector("_L2Pos", L2.gameObject.transform.position);

                if (reflectionRendering)
                    Materials[i].SetVector("_RefAmb", ReflectionGI);
                else
                    Materials[i].SetVector("_RefAmb", Ref_Ambient);
                    
            }
          
        }
    }

    // If not to color it is restored to black
    public bool SetReflectionGI(bool toColor) {
        if (toColor)
            for (int i = 0; i < Materials.Count; i++) {
                Materials[i].SetVector("_RefAmb", ReflectionGI);
            }
        else
            for (int i = 0; i < Materials.Count; i++) {
                Materials[i].SetVector("_RefAmb", new Color (0,0,0,1));
            }

        return true;
    }

	// Update is called once per frame
	void Update () {

        if (!Application.isPlaying) {
            SetMaterialProperties();
        }
	}

    void FixedUpdate() {
        if (Application.isPlaying) {
            SetMaterialProperties();
        }
	}

}
