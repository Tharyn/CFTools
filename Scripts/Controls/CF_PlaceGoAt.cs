using UnityEngine;
using System.Collections;


public class CF_PlaceGoAt : MonoBehaviour {


    public GameObject target;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            this.transform.position = target.transform.position;
            this.transform.rotation = target.transform.rotation;
        }
	}
}
