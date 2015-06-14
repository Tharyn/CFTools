using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Shape_Sin : MonoBehaviour
{
    public int subdivisions = 20;
    public int offset_X;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.SetVertexCount(subdivisions);
    }
    void Update()
    {
        lineRenderer.SetVertexCount(subdivisions);

        for (int i = 0; i < subdivisions; i++)
        {
            Vector3 pos = new Vector3(i * 0.05F - offset_X, Mathf.Sin(i * .2F + Time.time), 0);
            lineRenderer.SetPosition(i, gameObject.transform.TransformPoint(pos));
        }

    }
}
