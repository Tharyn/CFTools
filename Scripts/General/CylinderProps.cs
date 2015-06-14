using UnityEngine;
using System.Collections;

public class CylinderProps : MonoBehaviour 
{
    [SerializeField]
    private float radius = .5F;
    [SerializeField]
    private float height = 1;

    public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            radius = value;
            transform.localScale = new Vector3(radius * 2, height/2, radius * 2);
        }
    }

    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
            transform.localScale = new Vector3(radius * 2, height / 2, radius * 2);
        }
    }
    void OnValidate()
    {
        Radius = radius;
        Height = height;
    }
}
