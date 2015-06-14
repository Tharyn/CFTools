

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CF_FlickerLight : MonoBehaviour {

    public float intensity = 1;
	public float amount = 1;
	public int flickerSpeed = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Random.Range(1,flickerSpeed) == 1){
            light.intensity = Random.Range(intensity - amount, intensity + amount);
		}
	}
}
