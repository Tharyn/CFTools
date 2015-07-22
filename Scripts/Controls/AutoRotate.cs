using UnityEngine;
using System.Collections;
using System;

public class AutoRotate : MonoBehaviour 
{
    public float speed = 10;
    public int x = 45;
    public int FPS = 24;
    float secondsThisFrame = 0;
	// Use this for initialization
	void Start () 
	{
	

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        secondsThisFrame += Time.deltaTime;
        if (secondsThisFrame < 1.0f / FPS) {
            return;
        } else {
            Quaternion target = Quaternion.Euler(x, transform.rotation.eulerAngles.y - speed, 0);
            transform.rotation = target;
        }
  	
	}

}



