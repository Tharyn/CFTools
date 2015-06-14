using UnityEngine;
using System.Collections;



/* QUERY BOX
           string sMessageBoxText = "Do you want to continue?";
           string sCaption = "My Test Application";

           MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
           MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

           MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

           switch (rsltMessageBox)
           {
               case MessageBoxResult.Yes:
               break;

               case MessageBoxResult.No:
               break;

               case MessageBoxResult.Cancel:
               break;
           }
           */

[ExecuteInEditMode]
public class CF_HandControl : MonoBehaviour {

    public bool CanTurnOnFlash = true;


    public StartUpScript OpeningControl;
    public GameObject Base;

    public GameObject Master;

    public GameObject Target;

    // BASE TRANSFORM
    public bool RestoreBaseTransform = false;
    public bool CaptureBaseTransform = false;
    public string BaseNotes;

    public Vector3 BasePosition;
    [HideInInspector]
    public Quaternion BaseRotation;
    [HideInInspector]
    public Vector3 BaseScale = new Vector3(1,1,1);

    // INSPECT TRANSFORM
    public bool RestoreInspectTransform = false;
    public bool CaptureInspectTransform = false;
    public string InspectNotes;

    public Vector3 InspectPosition;
    [HideInInspector]
    public Quaternion InspectRotation;
    [HideInInspector]
    public Vector3 InspectScale = new Vector3(1, 1, 1);

    // HAND TRANSFORM
    public bool RestoreHandTransform = false;
    public bool CaptureHandTransform = false;
    public string HandNotes;

    public Vector3 HandPosition;
    [HideInInspector]
    public Quaternion HandRotation;
    [HideInInspector]
    public Vector3 HandScale = new Vector3(1, 1, 1);

    // PUTDOWN TRANSFORM
    public bool RestorePutdownTransform = false;
    public bool CapturePutdownTransform = false;
    public string PutdownNotes;

    public Vector3 PutdownPosition;
    [HideInInspector]
    public Quaternion PutdownRotation;
    [HideInInspector]
    public Vector3 PutdownScale = new Vector3(1, 1, 1);


    // FLASH LIGHT RELATED
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    public Material FlashMat;


    // BRING TO COROUTINE
    public static  IEnumerator BringTo(GameObject go, CF_HandControl goHC, int tran)
    {

        //Debug.Log("In coroutine");
        float increment = -.01f;

        if (tran == 0)
        {
            for (float f = 1f; f >= 0; f += increment)
            {
                go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, goHC.InspectPosition, 1 - f);
                //go.transform.localRotation = Quaternion.Lerp(go.transform.localRotation, goHC.InspectRotation, 1 - f);

                yield return null;
            }
        }
        if (tran == 1)
        {
            for (float f = 1f; f >= 0; f += increment)
            {
                go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, goHC.HandPosition, 1 - f);
                go.transform.localRotation = Quaternion.Lerp(go.transform.localRotation, goHC.HandRotation, 1 - (f*.1f));

                //Debug.Log(f);
                yield return null;
            }
        }
        if (tran == 2)
        {
            for (float f = 1f; f >= 0; f += increment)
            {
                go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, goHC.PutdownPosition, 1 - f);
                go.transform.localRotation = Quaternion.Lerp(go.transform.localRotation, goHC.PutdownRotation, 1 - (f * .1f));

                //Debug.Log(f);
                yield return null;
            }
        }

    }


    // TOGGLE CAMERA CONTROL ON/OFF
    void ToggleCameraControl(GameObject go, bool onOff, int propSt)
    {
        //Debug.Log(go);

        go.transform.parent.GetComponent<MouseLook>().enabled = onOff;
        go.transform.parent.GetComponent<CharacterMotor>().enabled = onOff;

        go.GetComponent<CF_CameraRayCaster>().PropState = propSt;

    }

    // ACTIVATE WHEN CLICKED
    public void Activate(CF_CameraRayCaster crc) {

        //Disable Camera
        ToggleCameraControl(Master, false, 1);


       
        // GET THE RIDGED BODY COLLIDER
        /*
        GameObject RBCollider = this.transform.Find("RBCollider").gameObject;

        // DISABLE COLLIDERS
        if (RBCollider.GetComponent<CapsuleCollider>() != null)
            RBCollider.GetComponent<CapsuleCollider>().enabled = false;
        if (RBCollider.GetComponent<BoxCollider>() != null)
            RBCollider.GetComponent<BoxCollider>().enabled = false;
        if (RBCollider.GetComponent<MeshCollider>() != null)
            RBCollider.GetComponent<MeshCollider>().enabled = false;
        */

        this.GetComponent<Rigidbody>().isKinematic = true;

        GameObject SelCollider = this.transform.Find("SelectionCollider").gameObject;
        if (SelCollider!= null)
            SelCollider.SetActive(false);

        this.transform.parent = Master.transform;
        StartCoroutine(BringTo(this.gameObject, this, 0));
        this.GetComponent<CF_Inspect>().enabled = true;
        if (this.name == "FlashLight_Base(ColliderCapsule,z,.1,1,false,PropGo,Ridged).max")
        {
            
            A.SetActive(true);
            B.SetActive(false);
            C.SetActive(false);
            D.SetActive(false);
            E.SetActive(false);
            FlashMat.SetFloat("_LightIntensity", 0);
        }

        // Only RUN IF ITS THE FIRST TIME PUTTING THE FLASH LIGHT TO HAND
        if (OpeningControl.Stage < 9)
            StartCoroutine(OpeningControl.StartPickedUpFlashLightGUI());
    }

    // PUT TO HAND
    public void PutToHand(CF_CameraRayCaster crc)
    {
        //Debug.Log("Pickup");
        ToggleCameraControl(Master, true, 2);

        this.GetComponent<CF_Inspect>().enabled = false;
        StartCoroutine(BringTo(this.gameObject, this, 1));
        ToggleCameraControl(Master, true, 2);
        

        // IF THIS IS THE FLASH LIGHT TURN IT ON IF YOU STILL HAVE A BATTERY
        if (this.name == "FlashLight_Base(ColliderCapsule,z,.1,1,false,PropGo,Ridged).max"  && CanTurnOnFlash)
        {
            // MOVED TO 'TurnOffFlashLight' in StartUpScript
            //CanTurnOnFlash = false;
            A.SetActive(false);
            B.SetActive(true);
            C.SetActive(true);
            D.SetActive(true);
            E.SetActive(true);
            FlashMat.SetFloat("_LightIntensity", 1);
        }
    }



    // DROP IT
    public IEnumerator DropIt(CF_CameraRayCaster crc)
    {
        GameObject.Find("FlashLightTrigger").GetComponent<FlashLightBattery>().PlayInstructions = false;


        this.GetComponent<CF_Inspect>().enabled = false;
        yield return  StartCoroutine(BringTo(this.gameObject, this, 2));
        ToggleCameraControl(Master, true, 0);

        // GET THE RIDGED BODY COLLIDER
        /*
        GameObject RBCollider = this.transform.Find("RBCollider").gameObject;

        if (RBCollider.GetComponent<CapsuleCollider>() != null)
            RBCollider.GetComponent<CapsuleCollider>().enabled = true;
        if (RBCollider.GetComponent<BoxCollider>() != null)
            RBCollider.GetComponent<BoxCollider>().enabled = true;
        if (RBCollider.GetComponent<MeshCollider>() != null)
            RBCollider.GetComponent<MeshCollider>().enabled = true;
        */
        this.GetComponent<Rigidbody>().isKinematic = false;

        GameObject SelCollider = this.transform.Find("SelectionCollider").gameObject;
        if (SelCollider != null)
            SelCollider.SetActive(true);

        this.transform.parent = null;
        crc.activeProp = null;
        crc.activeHC = null;
        crc.CastActive = true;       
    }

    // BRING TO COROUTINE
    public static IEnumerator DoorTo(GameObject go, CF_HandControl goHC, int tran)
    {

        //Debug.Log("In coroutine");
        float increment = -.00001f;

        if (tran == 0)
        {
            for (float f = 1f; f >= 0; f += increment)
            {
                go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, goHC.InspectPosition, 1 - f);
                //go.transform.localRotation = Quaternion.Lerp(go.transform.localRotation, goHC.InspectRotation, 1 - f);

                yield return null;
            }
        }
        if (tran == 1)
        {
            for (float f = 1f; f >= 0; f += increment)
            {
                go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, goHC.HandPosition, 1 - f);
                go.transform.localRotation = Quaternion.Lerp(go.transform.localRotation, goHC.HandRotation, 1 - (f * .1f));
                yield return new WaitForSeconds(.1f);
                //Debug.Log(f);
                yield return null;
            }
        }
        if (tran == 2)
        {
            for (float f = 1f; f >= 0; f += increment)
            {
                go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, goHC.PutdownPosition, 1 - f);
                go.transform.localRotation = Quaternion.Lerp(go.transform.localRotation, goHC.PutdownRotation, 1 - (f * .1f));

                //Debug.Log(f);
                yield return null;
            }
        }

    }

    // OPEN DOOR
    public void OpenDoor()
    {
       // this.GetComponent<CF_Inspect>().enabled = false;

        StartCoroutine(DoorTo(this.gameObject, this, 1));
    }

    // Use this for initialization
    void Start()
    {
        OpeningControl = GameObject.Find("_OpeningController").GetComponent<StartUpScript>();
    }

    // Update is called once per frame
	void Update () {

        // BASE
        if (CaptureBaseTransform)
        {
            BasePosition = transform.localPosition;
            BaseRotation = transform.localRotation;
            //BaseScale = transform.localScale;
            CaptureBaseTransform = false;
        }
        if (RestoreBaseTransform)
        {
            transform.parent = Base.transform;
            transform.localPosition = BasePosition;
            transform.localRotation = BaseRotation;
            //transform.localScale = BaseScale;
            RestoreBaseTransform = false;
        }

        // INSPECT
        if (CaptureInspectTransform) {
            InspectPosition = transform.localPosition;
            InspectRotation = transform.localRotation;
            //InspectScale = transform.localScale;
            CaptureInspectTransform = false;
        }
        if (RestoreInspectTransform) {

            transform.parent = Master.transform;
            transform.localPosition = InspectPosition;
            transform.localRotation = InspectRotation;
            //transform.localScale = InspectScale;
            RestoreInspectTransform = false;
        }

        // HAND
        if (CaptureHandTransform) {
            transform.parent = Master.transform;
            HandPosition = transform.localPosition;
            HandRotation = transform.localRotation;
            //HandScale = transform.localScale;
            CaptureHandTransform = false;
        }

        if (RestoreHandTransform)
        {
            transform.parent = Master.transform;
            transform.localPosition = HandPosition;
            transform.localRotation = HandRotation;
            //transform.localScale = HandScale;
            RestoreHandTransform = false;
        }

        // PUT DOWN
        if (CapturePutdownTransform)
        {
            PutdownPosition = transform.localPosition;
            PutdownRotation = transform.localRotation;
            //PutdownScale = transform.localScale;
            CapturePutdownTransform = false;
        }

        if (RestorePutdownTransform)
        {
            transform.parent = Master.transform;
            transform.localPosition = PutdownPosition;
            transform.localRotation = PutdownRotation;
            //transform.localScale = PutdownScale;
            RestorePutdownTransform = false;
        }

	}



}
