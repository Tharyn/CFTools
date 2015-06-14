using UnityEngine;
using System.Collections;
using System;
//using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)

public class CF_Coroutines : MonoBehaviour {

    public int CurveSteps = 100; 
    public List<float> CurveLinearTable;
    //public List<float> CurveEaseInTable;
    //public List<float> CurveEaseOutTable;
    public List<float> CurveEaseInOutTable;


    public static IEnumerator SinRad(SphereProps sphereProps)
    {
        // A->B     Slerp
        // A<->B    PingPong

        float increment = -.01f;
        for (float f = 1f; f >= 0; f += increment)
        {
            sphereProps.Radius = f * 10;
            //Color c = renderer.material.color;
            //c.a = f;
            //renderer.material.color = c;
            if (f < .1f)
                increment = +.01f;
            else if (f > 1.1f)
                increment = -.01f;

            yield return null;
        }

    }

    void BuildCurveTables()
    {
        for (float i = 0; i < CurveSteps; i++)
        {
            CurveEaseInOutTable.Add(-(100f / 100) / 2 * (Mathf.Cos(Mathf.PI * i / CurveSteps) - 1) + 0);

            CurveLinearTable.Add(i / CurveSteps);
        }

    }

	// Use this for initialization
	void Start () {
        BuildCurveTables();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
