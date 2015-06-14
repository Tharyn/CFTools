using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class LineRender00 : MonoBehaviour
{
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;

    void Start()
    {
      /*
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
         */
    }
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        int i = 0;
        while (i < lengthOfLineRenderer)
        {
            Vector3 pos = new Vector3(i * 0.05F, Mathf.Sin(i*.2F + Time.time), 0);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
}