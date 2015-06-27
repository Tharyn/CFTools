using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class CF_DeepEmis : MonoBehaviour {

    public Light E1;
    public float multi = 1;
    public Material mat;



    void SetMaterialProperties() {
        if (E1 != null && mat != null) {

            float L1mag;
            if (E1.enabled && E1.gameObject.activeSelf)
                L1mag = E1.intensity * multi;
            else
                L1mag = 0;

            mat.SetFloat("_E1Intensity", L1mag);
            mat.SetVector("_E1Color", E1.color);
        }
    }

	// Use this for initialization
	void Start () {
        mat = gameObject.renderer.sharedMaterial;
	}

    // Update is called once per frame
    void Update() {
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
