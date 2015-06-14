using UnityEngine;
using System.Collections;


public class CFSignWaveRadius : MonoBehaviour {



	// Use this for initialization
	void Start () {
        SphereProps sphereProps = this.GetComponent<SphereProps>();
        if (sphereProps!= null) {
            StartCoroutine(CF_Coroutines.SinRad(sphereProps));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
