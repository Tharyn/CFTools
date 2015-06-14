using UnityEngine;
using System;

public class Gizmo_Sphere : MonoBehaviour 
{
    public float radius = 1F;
    public Color color = new Color(.8f, .8f, 0f, .5F);

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    // DRAW GIZMOS
    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = color;
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), radius);
    }
}
