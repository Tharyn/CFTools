using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CF_DaisyLight : MonoBehaviour {

    public Light LightMaster;
    public Light LightSlave;
    public float multiplier = 1;

	// Use this for initialization
	void Start () {
        LightSlave = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (LightSlave != null && LightMaster != null)
            LightSlave.intensity = LightMaster.intensity * multiplier;
	}
}
