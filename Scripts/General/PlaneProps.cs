using UnityEngine;
using System.Collections;

public class PlaneProps : MonoBehaviour
{
    [SerializeField]
    private float widthInch = 120;

    public float WidthInch {
        get {
            return widthInch;
        }
        set {
            widthInch = value;
            //widthFoot = value/12;
            transform.localScale = new Vector3(widthInch / 10, 1, heightInch / 10);
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
            //widthInch = value*12;
            //transform.localScale = new Vector3(widthInch / 10, 1, heightInch / 10);
        }
    }
    */
    [SerializeField]
    private float heightInch = 5;
    public float HeightInch {
        get {
            return heightInch;
        }
        set {
            heightInch = value;
            transform.localScale = new Vector3(widthInch / 10, 1, heightInch / 10);
        }
    }


    void OnValidate() {
        WidthInch = widthInch;
        HeightInch = heightInch;
    }
}
