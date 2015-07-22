using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LoadGGX : MonoBehaviour {
    public Texture2D GGXlut;

	// Use this for initialization
	void Start () {
        if (GGXlut != null) {
            Shader.SetGlobalTexture("_GGXlut", GGXlut);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
}
