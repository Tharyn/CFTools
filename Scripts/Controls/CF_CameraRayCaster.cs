using UnityEngine;
using System.Collections;

public class CF_CameraRayCaster : MonoBehaviour {

    public StartUpScript OpeningControl;

    public bool CastActive = true;
    public int RayFilter = 0 ;
    public float CastDistance = 10f;
    public GameObject cursor;
    public MouseLook camControl;
    public CharacterMotor charMotor;
    public GameObject activeProp = null;
    public CF_HandControl activeHC = null;
    public int PropState = 0;

    public bool doorsClosed = true;
    public CF_HandControl doorRT = null;
    public CF_HandControl doorLF = null;

    public AudioSource Unlock;
    public AudioSource Creak;
    /*
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    */ 

	// Use this for initialization
	void Start () {
        OpeningControl = GameObject.Find("_OpeningController").GetComponent<StartUpScript>();
	}

    // FADES IN MAIN GUI BAR
    public IEnumerator OpenDoors()
    {
        yield return new WaitForSeconds(2f);
        Unlock.Play();
        yield return new WaitForSeconds(1f);
        Creak.Play();
        doorRT.OpenDoor();
        doorLF.OpenDoor();
    }
	
    // MAIN UPDATE
	void Update () {

        // RAY CASTER FOR PROPS
        if (CastActive) {
            RaycastHit hit;
            Ray targetRay = new Ray(transform.position, transform.forward);

            // IF NO PROP
            if (PropState == 0) {
                // IF PROP IS HIT
                if (Physics.Raycast(targetRay, out hit, CastDistance, RayFilter))
                {
                    // GET PROP
                    activeProp = hit.collider.gameObject.transform.parent.gameObject;
                    // GET PROPS HAND CONTROL
                    activeHC = activeProp.GetComponent<CF_HandControl>();
                    // TURN ON PROP OUTLINE
                    GameObject PropOutline = activeProp.transform.Find("PropOutline").gameObject;
                    if (PropOutline != null)
                        PropOutline.SetActive(true);
                }
                else
                {
                    // TURN OFF PROP OUTLINE
                    if (activeProp != null)
                    {
                        GameObject PropOutline = activeProp.transform.Find("PropOutline").gameObject;
                        if (PropOutline != null)
                            PropOutline.SetActive(false);
                    }
                    // NULL PROP AND HAND CONTROL
                    this.activeProp = null;
                    this.activeHC = null;
                }
            }
        }



        // PICK UP PROP
        // IF LEFT MOUSE and WE HAVE A HAND CONTROL
        if (Input.GetMouseButtonDown(0) && activeProp != null && (PropState == 0) && CastActive) 
        {
            print("PROP  PICKEDUP");
            // TURN ON RAY CAST
            CastActive = false;

            // IF REGESTRY
            if (activeProp.name == "Regestry_Root.max") {
                Debug.Log("Got the book");
                GameObject sel = activeProp.transform.FindChild("SelectionCollider").gameObject;
                sel.SetActive(false);
                activeProp.GetComponent<Animation>().Play();
                StartCoroutine(OpeningControl.StartRegestrySeq());
 
                GameObject PropOutline = activeProp.transform.Find("PropOutline").gameObject;
                if (PropOutline != null)
                    PropOutline.SetActive(false);
                this.activeProp = null;
                this.activeHC = null;
                /// Trigger big script here
            } else  { // EVERY THINSD ELSE
                if (activeProp.name == "Key(RidgedBody)" && doorsClosed)
                {
                    doorsClosed = false;
                    StartCoroutine(OpenDoors());
                }

                activeHC.Activate(this);
            }

            // TURN OFF PROP OUTLINE
            if (activeProp != null)
            {
                GameObject PropOutline = activeProp.transform.Find("PropOutline").gameObject;
                if (PropOutline != null)
                    PropOutline.SetActive(false);
            }
        }

        /// SEND BACK TO INSPECT
        if (Input.GetKeyDown(KeyCode.E) && PropState == 2)
        {
            print ("Entered E -> Back to Inspect");
            activeHC.Activate(this);
        } else if (Input.GetKeyDown(KeyCode.E) && PropState == 1) // PUT TO HAND
        {
            print("Entered E -> Put to hand");
            if (OpeningControl.Stage == 9 )
            {
                print("IN HAND FIRST TIME");
                OpeningControl.Stage = 10;
                print(OpeningControl.Stage);
                StartCoroutine(OpeningControl.FadeOutMainSO(OpeningControl.SO));
            }
            if (OpeningControl.Stage > 8)
                 activeHC.PutToHand(this);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && PropState == 1)
        {
            StartCoroutine(activeHC.DropIt(this));


            this.activeProp = null;
            this.activeHC = null;
            this.CastActive = true;
            if (OpeningControl.Stage == 12)
            {
                OpeningControl.Stage = 13;
                StartCoroutine(OpeningControl.FadeFlashGui());
            }

            
        }

        if (Input.GetKey("escape"))
            Application.Quit();
	}
}
