using UnityEngine;
using System.Collections;

public class FFvMatUpdate : MonoBehaviour {




    static public void UpdateMaterials() {

        FFvControl[] controls = FindObjectsOfType(typeof(FFvControl)) as FFvControl[];
        for (int i = 0; i < controls.Length; i ++) {
            controls[i].UpdateDependants();
            Debug.Log("Updated " + controls[i].gameObject.name);
        }
   
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //Renderer[] renderers = FindObjectsOfType(typeof(Renderer)) as Renderer[];
    //FFvControl[] texture = FindObjectOfType(typeof(FFvControl) as FFvControl[]);
}
