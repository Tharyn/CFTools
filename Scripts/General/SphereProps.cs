using UnityEngine;
using System.Collections;

public class SphereProps : MonoBehaviour 
{
    [SerializeField]
    private float radius = .5F;

    public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            radius = value;
            transform.localScale = new Vector3(radius*2,radius*2,radius*2);
        }
    }

    void OnValidate()
    {
        Radius = radius;
    }

    void Awake()
    {

    }

	void Start () 
	{
        //print("start");
	}


    //GUILayout.Button("Debug 0 ", GUILayout.Width(100));

	// Update is called once per frame
	void Update () 
	{
        //print("update");
       // transform.localScale = new Vector3(2, 1, 1);
	}
}
