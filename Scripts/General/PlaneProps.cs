using UnityEngine;
using System.Collections;

public class PlaneProps : MonoBehaviour
{
    [SerializeField]
    private float width = 5;

    public float WidthInch {
        get {
            return width;
        }
        set {
            width = value;
            //widthFoot = value/12;
            transform.localScale = new Vector3(width/10, 1, height/10);
        }
    }
    /*
    [SerializeField]
    private float widthFoot = 10;
    public float WidthFoot
    {
        get {
            return widthFoot;
        }
        set {
            widthFoot = value;
            //width = value*12;
            //transform.localScale = new Vector3(width / 10, 1, height / 10);
        }
    }
    */
    [SerializeField]
    private float height = 5;
    public float HeightInch {
        get {
            return height;
        }
        set {
            height = value;
            transform.localScale = new Vector3(width/10, 1, height/10);
        }
    }


    void OnValidate() {
        WidthInch = width;
        HeightInch = height;
    }
}
