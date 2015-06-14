using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Shape_Circle: MonoBehaviour 
{
    public int subs = 10;

    public float radius;
    LineRenderer lineRenderer;
    Vector3 tempVector;


    void setShape()
    {
        lineRenderer.SetVertexCount(subs+1);

        for (int i = 0; i < (subs+1); i++)
        {
            //tempVector = Quaternion.AngleAxis(i, Vector3.up) * new Vector3(0, 1, 0);
            tempVector = Quaternion.Euler(0, 0, -i * 360/subs) * new Vector3(0, radius, 0);

            lineRenderer.SetPosition(i, gameObject.transform.TransformPoint(tempVector));
            //lineRenderer.SetPosition(i, tempVector);
        }
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        setShape();
    }


    void Update()
    {
        setShape();
    }
}
