using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Shape_Arrow : MonoBehaviour 
{
    public float length = 1;
    public float width = .1F;
    public int pointCount = 0;
    public int totalPoints = 5;
    LineRenderer lineRenderer;

    void setShape()
    {
        lineRenderer.SetVertexCount(totalPoints);

        lineRenderer.SetPosition(pointCount, new Vector3(0, 0, 0));
        pointCount++;

        lineRenderer.SetPosition(pointCount, new Vector3(0, length-.01F, 0));
        pointCount++;
        lineRenderer.SetPosition(pointCount, new Vector3(0, length, 0));
        pointCount++;
        lineRenderer.SetPosition(pointCount, new Vector3(0, length + .01F, 0));
        pointCount++;

        lineRenderer.SetPosition(pointCount, new Vector3(width - .01F, length - width - .01F, 0));
        pointCount++;
        lineRenderer.SetPosition(pointCount, new Vector3(width, length - width, 0));
        pointCount++;
        lineRenderer.SetPosition(pointCount, new Vector3(width + .01F, length - width + .01F, 0));
        pointCount++;

        lineRenderer.SetPosition(pointCount, new Vector3(-width, length - width, 0));
        pointCount++;

        lineRenderer.SetPosition(pointCount, new Vector3(0, length, 0));

        pointCount = 0;
    }

    void Start()
    {
        totalPoints = 6;

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
