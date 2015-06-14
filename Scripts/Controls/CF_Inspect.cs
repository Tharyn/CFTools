using UnityEngine;
using System.Collections;

public class CF_Inspect : MonoBehaviour {

    public float sensitivityX = 4F;
    public float sensitivityY = 4F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -70F;
    public float maximumY = 70F;

    float rotationY = 0F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);	

	}
}
