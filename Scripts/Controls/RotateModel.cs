using UnityEngine;
using System.Collections;
using System;

public class RotateModel : MonoBehaviour 
{
    float lerpSpeed = .1f;
    float xDeg ;
    float yDeg ;
    Quaternion fromRotation ;
    Quaternion toRotation ;

    //public float smooth = 10.0F;
    public float Yspeed = 5.0F;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetMouseButton(0))
        {


            float tiltAroundY = Input.GetAxis("Mouse X") * -Yspeed;
            //float tiltAroundX = Input.GetAxis("Mouse Y") * tiltAngle;
            //Debug.Log(tiltAroundZ + " " + tiltAroundX);
            //Quaternion target = Quaternion.Euler(270, transform.rotation.eulerAngles.y - tiltAroundZ, 0);
            Quaternion target = Quaternion.Euler(0, transform.rotation.eulerAngles.y - tiltAroundY, 0);
            transform.rotation = target;
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);


            //RotateTransform(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        //else
            //RotateTransform(0f, 0f); 	
	}

    void RotateTransform(float xNum, float yNum) 
    {
        xDeg -= xNum; // *speed * friction; 
        yDeg -= yNum; //*speed * friction; 
        fromRotation = transform.rotation; 
        toRotation = Quaternion.Euler(yDeg, xDeg, 0); 
        transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed); 
    }
}



