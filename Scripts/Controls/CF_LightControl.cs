using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CF_LightControl : MonoBehaviour {

    public float LightIntensity;
    public Vector2 RotationRange = new Vector2(0,180);
    public Vector2 MappingRange = new Vector2(.1f, .8f);
    public Light ctrlLight;

    void adjustLight()
    {
 
        float angle = Quaternion.Angle(new Quaternion(0, 1, 0, 0), this.transform.rotation);
        float normAngle = angle / RotationRange.y;
        ctrlLight.intensity = (normAngle * (MappingRange.y - MappingRange.x)) + MappingRange.x;
        LightIntensity = ctrlLight.intensity; 

    }

	// Use this for initialization
	void Start () {
        adjustLight();
      
	}
	
	// Update is called once per frame
	void Update () {

        adjustLight();
	}
}
