using UnityEngine;
using System.Collections;

public class CF_IteractControl : MonoBehaviour {


    public bool zoomOn = false;
    public int propState = 0;

    public GameObject cursor;
    public GameObject activeProp;
    public GameObject examineNull;
    public Renderer meshRend;
    public MouseLook camControl;
    public Light contLight;
    public GameObject contGo;

    public LayerMask mask = 19;



    public static IEnumerator BringToInspect(GameObject go, GameObject target)
    {
        // A->B     Slerp
        // A<->B    PingPong
        Debug.Log("In coroutine") ;
        float increment = -.01f;
        for (float f = 1f; f >= 0; f += increment)
        {
            go.transform.position = Vector3.Lerp(go.transform.position, target.transform.position, 1-f);
            //go.transform.position += new Vector3(f * 10, 0, 0) ;

            Debug.Log(f);
            yield return null;
        }

    }

    public static IEnumerator ZoomIn(Camera cam, float dir)
    {
    
        Debug.Log("In coroutine Camera");
        float increment = -.1f;
        for (float f = 4f; f >= 0; f += increment)
        {
            
            cam.fieldOfView -= f * .2f * dir;
            //go.transform.position += new Vector3(f * 10, 0, 0) ;

            Debug.Log(f);
            yield return new WaitForSeconds(.01f) ;
        }

    }

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        if (cursor != null)
            meshRend = cursor.GetComponent<MeshRenderer>();
	}

    void Pickup (GameObject go, GameObject target)
    {
        //activeProp.transform.position = examineNull.transform.position;
        //activeProp.transform.rotation = examineNull.transform.rotation;
        StartCoroutine(BringToInspect(go, target));
        Debug.Log(go);
        Debug.Log(target);
    }
    void Drop(GameObject go)
    {
        go.collider.rigidbody.isKinematic = false;
        go.transform.parent = null;
        propState = 0;
    }
    

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            contLight.intensity += .02f;
            contGo.transform.Rotate(new Vector3(4f, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            contLight.intensity += -.02f;
            contGo.transform.Rotate(new Vector3(-4f, 0, 0));
        }


            activeProp = null;
            cursor.SetActive(false);
        
        if (cursor != null)
        {
            RaycastHit hit;
            Ray targetRay = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(targetRay, out hit))
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Prop")
                {
                    //Debug.Log("hand hit");
                    activeProp = hit.collider.gameObject;
                    cursor.SetActive(true);
                }

            }
            if (Input.GetMouseButtonDown(0))
            {
                if (activeProp != null)
                {
                    /*
                    propState = 1;
                    //camControl.active = false;
                    Debug.Log("Pickup");
                    activeProp.collider.rigidbody.isKinematic = true;
                    activeProp.transform.parent = examineNull.transform;
                    Pickup(activeProp, examineNull);
                    */
                    if (activeProp.name == "Props_Globe_Ball_World.max")
                    {
                        activeProp.rigidbody.AddTorque(new Vector3(1000,6000,0) );
                        Debug.Log("rotate");
                    }

                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                if (zoomOn == false)
                {
                    Debug.Log("Camera");
                    zoomOn = true;
                    StartCoroutine(ZoomIn(camera, 1));
                }
                else
                {
                    zoomOn = false;
                    StartCoroutine(ZoomIn(camera, -1));
                }
            }

            if (Input.GetMouseButtonDown(1) && propState == 1)
            {
                Drop(activeProp);
            }
        }
       
	}
}
