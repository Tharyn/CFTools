using UnityEngine;
using System.Collections;

public class CubeProps : MonoBehaviour 
{
    [SerializeField]
    private float width = 1;
    public float Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
            transform.localScale = new Vector3(width, height, length);
        }
    }


    [SerializeField]
    private float height = 1;
    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
            transform.localScale = new Vector3(width, height, length);
        }
    }

    [SerializeField]
    private float length = 1;
    public float Length
    {
        get
        {
            return length;
        }
        set
        {
            length = value;
            transform.localScale = new Vector3(width, height, length);
        }
    }

    void OnValidate()
    {
        Width = width;
        Height = height;
        Length = length;
    }
}
