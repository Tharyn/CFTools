using UnityEngine;
using System.Collections;
using System;

public class AutoRotate : MonoBehaviour 
{
    public int speed = 10;
    public int x = 45; 

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

        Quaternion target = Quaternion.Euler(x, transform.rotation.eulerAngles.y - speed, 0);
        transform.rotation = target;
  	
	}

}



