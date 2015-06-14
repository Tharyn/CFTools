using UnityEngine;
using System.Collections;

public class StartUpScript : MonoBehaviour {



    public GameObject Plate;
    public GameObject PlateMain;

    public Camera CameraA;
    public Camera CameraB;
    public Animation StartCamera;

    public AudioSource Sarah;
    public AudioSource MrsQ;
    public AudioSource PlayerWoo;

    public int Stage = 0;
    public GameObject FPS;

    public ScreenOverlay SO;

    ///////////////////////////////////////////
    // IN INTRO
    ///////////////////////////////////////////

    // FADES IN A PLATE (BRIGHTNESS, AUDIO etc)
    IEnumerator FadeIn(Material mat)
    {
        for (float f = 0f; f <= 1.1; f += 0.05f)
        {
            mat.SetFloat ("_Fade",  f);
            yield return new WaitForSeconds(.05f);
        }
    }

    // FADES OUT A PLATE (BRIGHTNESS, AUDIO etc)
    IEnumerator FadeOut(Material mat)
    {
        print("In Fade out");
        for (float f = 1f; f >= -0.1; f -= 0.05f)
        {
            //print(f);
            mat.SetFloat("_Fade", f);
            yield return new WaitForSeconds(.05f);
        }

    }

    // Fade Plate Out
    IEnumerator FadeIntoCamera(FadeToColor F2T)
    {

        for (float f = 0f; f <= 1.01; f += 0.025f)
        {
            F2T.Solid = new Color(f,f,f,1);
            yield return new WaitForSeconds(.05f);
        }
    }

    // Fade Plate Out
    IEnumerator FadeBloom(Bloom F2T)
    {

        for (float f = 40f; f >= 2.1; f -= 0.1f)
        {
            F2T.bloomIntensity = f;
            yield return new WaitForSeconds(.005f);
        }
    }

    // BEGINNING INTRO
    public IEnumerator BeginIntro()
    {
        print ("Fading IN  Brightness Plate:");
        print(Stage);
        yield return StartCoroutine(FadeIn(Plate.renderer.material));
        Stage = 1;
        print(Stage);
    }

    // TRANSITION FROM BRIGHTNESS TO AUDIO
    public IEnumerator BrightnessToAudio()
    {
        print("Fading OUT  Brightness Plate:");
        Stage = 2;
        print(Stage);
        
        print("FADE OUT BRIGHTNESS");
        yield return StartCoroutine(FadeOut(Plate.renderer.material));

        print("SWAP TO AUDIO MATERIAL");
        Material Screen_AudioMat = Resources.Load("Screen_Audio", typeof(Material)) as Material;
        Plate.renderer.material = Screen_AudioMat;

        print("FADE IN AUDIO SCREEN");
        Stage = 3;
        print(Stage);
        yield return StartCoroutine(FadeIn(Plate.renderer.material));

        print("AUDIO SCREEN VISIBLE");
        Stage = 4;
        print(Stage);
    }

    // ENDING INTRO SWAPING CAMERAS
    public IEnumerator EndIntro()
    {
        
        print("FADE OUT AUDIO SCREEN");
        yield return StartCoroutine(FadeOut(Plate.renderer.material));
        Stage = 5;
        print(Stage);

        print ("DISABLE CURSOR");
        Screen.showCursor = false;

        print ("SWAP CAMERAS");
        CameraA.enabled = false;
        PlateMain.SetActive(true);
        CameraB.enabled = true;
        CameraB.gameObject.GetComponent<CF_CameraRayCaster>().enabled = true;

        print ("PLAY CAMERA ANIMATION");
        StartCamera.Play();
 
        print ("FADE OUT SOLID");
        yield return StartCoroutine(FadeIntoCamera(CameraB.GetComponent<FadeToColor>()));
        CameraB.GetComponent<FadeToColor>().enabled = false;

        print ("FADE OUT BLOOM");
        yield return StartCoroutine(FadeBloom(CameraB.GetComponent<Bloom>()));
        Stage = 6;
        print(Stage);

    }

    ///////////////////////////////////////////
    // IN GAME AND MAIN CAMERA
    ///////////////////////////////////////////

    // FADES IN MAIN GUI BAR
    public IEnumerator FadeInMainGUI(Material mat)
    {
        for (float f = 0f; f < .4f; f += 0.005f)
        {
            mat.SetFloat("_Amount", f);
            yield return new WaitForSeconds(.01f);
        }
    }

    // FADES OUT MAIN GUI BAR
    public IEnumerator FadeOutMainGUI(Material mat)
    {
        for (float f = .4f; f > 0f; f -= 0.005f)
        {
            mat.SetFloat("_Amount", f);
            yield return new WaitForSeconds(.01f);
        }
    }


    // FADES IN MAIN GUI BAR
    public IEnumerator FadeInMainSO(ScreenOverlay SO)
    {
        for (float f = 0f; f < .8f; f += 0.005f)
        {
            SO.intensity = f;
            yield return new WaitForSeconds(.004f);
        }
    }


    // FADES OUT MAIN GUI BAR
    public IEnumerator FadeOutMainSO(ScreenOverlay SO)
    {
        for (float f = .8f; f > 0f; f -= 0.005f)
        {
            SO.intensity = f;
            yield return new WaitForSeconds(.004f);
        }
    }


    // IN GAME INSZTRUCTION GUI BEGIN
    public IEnumerator StartInGameGUI()
    {
        print("FADE IN MOVEMENT BAR FOR 2.5 Sec:");
        yield return StartCoroutine(FadeInMainSO(SO));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeOutMainSO(SO));
        Stage = 7;
        print(Stage);

        print("FADE IN PICK UP BAR");
        Texture2D Screen_PickupMat = Resources.Load("Screen_Pickup", typeof(Texture2D)) as Texture2D;
        SO.texture = Screen_PickupMat;
        yield return StartCoroutine(FadeInMainSO(SO));
        Stage = 8;
        print(Stage);

        /*
        print("FADE IN MOVEMENT BAR FOR 2.5 Sec:");
        yield return StartCoroutine(FadeInMainGUI(PlateMain.renderer.material));
        yield return new WaitForSeconds(2.5f);
        yield return StartCoroutine(FadeOutMainGUI(PlateMain.renderer.material));
        Stage = 7;
        print(Stage);

        print("FADE IN PICK UP BAR");
        Material Screen_PickupMat = Resources.Load("Screen_Pickup", typeof(Material)) as Material;
        PlateMain.renderer.material = Screen_PickupMat;
        yield return StartCoroutine(FadeInMainGUI(PlateMain.renderer.material));
        Stage = 8;
        print(Stage);
         */
    }

    // first time the flash light is picked up give put to hand instructions
    public IEnumerator StartPickedUpFlashLightGUI()
    {

        yield return StartCoroutine(FadeOutMainSO(SO));

        Texture2D Screen_EquipMat = Resources.Load("Screen_Equip", typeof(Texture2D)) as Texture2D;
        SO.texture = Screen_EquipMat;
        yield return StartCoroutine(FadeInMainSO(SO));
        Stage = 9;
        print(Stage);
        Debug.Log("Flash light in Inspect");




        /*
        yield return StartCoroutine(FadeOutMainGUI(PlateMain.renderer.material));

        Material Screen_EquipMat = Resources.Load("Screen_Equip", typeof(Material)) as Material;
        PlateMain.renderer.material = Screen_EquipMat;
        yield return StartCoroutine(FadeInMainGUI(PlateMain.renderer.material));
        Stage = 9;
        print(Stage);
        Debug.Log("Flash light in Inspect");
         * */
    }
    
    // FADE OFF FLASH LIGHT
    public IEnumerator FadeFlash(CF_HandControl HC)
    {
        for (float f = .5f; f > 0f; f -= 0.001f)
        {
            HC.B.light.intensity = f;
            HC.C.renderer.material.SetFloat("_SideFade", (f * .09f));
            HC.D.renderer.material.SetFloat("_SideFade", (f * .09f));
            HC.E.renderer.material.SetFloat("_SideFade", (f * .09f));
            //mat.SetFloat("_Amount", f);
            yield return new WaitForSeconds(.01f);
        }
    }

    // RUN ONLY IF FLASH LIGHT IS IN HAND
    public IEnumerator TurnOffFlashLight(CF_HandControl HC, bool PlayInstructions)
    {

        HC.CanTurnOnFlash = false;
        Stage = 11;
        print (Stage);
        // Fad the Flashlight
        yield return StartCoroutine(FadeFlash(HC));

        // IF it is still  in your hand play instyructions
        if (PlayInstructions) {
            Texture2D Screen_EquipMat = Resources.Load("Screen_DropFlash", typeof(Texture2D)) as Texture2D;
            SO.texture = Screen_EquipMat;
            yield return StartCoroutine(FadeInMainSO(SO));
            Stage = 12;
            print (Stage);
        }
        
        Debug.Log("Flash light OFF");
    }

    // Only triggered if the intrsuctions was started
    public IEnumerator FadeFlashGui()
    {
        yield return StartCoroutine(FadeOutMainSO(SO));

    }



    // FADES OUT MAIN GUI BAR
    public IEnumerator FadeOutForMemoryGUI(Material mat, GameObject cam)
    {
        for (float f = .4f; f > 0f; f -= 0.005f)
        {
            mat.SetFloat("_Amount", f);
            cam.GetComponent<Vignetting>().intensity = 4 - (f*10);
            yield return new WaitForSeconds(.01f);
        }
    }

    // FADES IN MAIN GUI BAR
    public IEnumerator FadeInMemoryGUI(Material mat, GameObject cam)
    {
        for (float f = 0f; f < .4f; f += 0.005f)
        {
            mat.SetFloat("_Amount", f);
            cam.GetComponent<Vignetting>().intensity = 4 - (f * 10);
            yield return new WaitForSeconds(.01f);
        }
    }

    // Only triggered if the intrsuctions was started
    public IEnumerator StartRegestrySeq()
    {
        Stage = 13;
        print(Stage);
        yield return new WaitForSeconds(1.5f);
        Material Screen_White = Resources.Load("Screen_White", typeof(Material)) as Material;
        PlateMain.renderer.material = Screen_White;
        yield return StartCoroutine(FadeInMainGUI(PlateMain.renderer.material));
        GameObject mainCamera = CameraB.gameObject;
        mainCamera.GetComponent<AmplifyColorEffect>().BlendAmount = 0;
        mainCamera.GetComponent<Vignetting>().enabled = true;
        yield return StartCoroutine(FadeOutForMemoryGUI(PlateMain.renderer.material, mainCamera));

        Sarah.Play();
        yield return new WaitForSeconds(4.1f);
        MrsQ.Play();
        yield return new WaitForSeconds(6.1f);

        yield return StartCoroutine(FadeInMemoryGUI(PlateMain.renderer.material, mainCamera));
        mainCamera.GetComponent<AmplifyColorEffect>().BlendAmount = 1;
        mainCamera.GetComponent<Vignetting>().enabled = false;
        yield return StartCoroutine(FadeOutMainGUI(PlateMain.renderer.material));
        mainCamera.GetComponent<CF_CameraRayCaster>().CastActive = true;
        yield return new WaitForSeconds(1f);

        PlayerWoo.Play();

    }








	// Use this for initialization
	void Start () {

        Plate.SetActive(true);
        CameraA.enabled = true;
        CameraB.enabled = false;
        CameraB.GetComponent<FadeToColor>().enabled = true;
        //CameraB.GetComponent<Bloom>().enabled = true;
        CameraB.GetComponent<CF_HandControl>().enabled = false;

        // PropRayCast ignore IgnorRaycast (Set collisions)
        Physics.IgnoreLayerCollision(21, 2);

        // FPS Ignore PropGo
        Physics.IgnoreLayerCollision(22, 20);

        // FPS Ignore in prop collisions
        Physics.IgnoreLayerCollision(21, 20);

        Physics.IgnoreLayerCollision(21, 23);
        Physics.IgnoreLayerCollision(20, 23);

        // FPS Ignore PropRayCast
        Physics.IgnoreLayerCollision(22, 21);

        // FADEING IN BRIGHTNESS SCREEN
        //Stage = 1; // IN
        StartCoroutine(BeginIntro());

	}
	
	// Update is called once per frame
	void Update () {
        // TO EXIT BRIGHTNESS
        if (Input.GetMouseButtonDown(0) && Stage == 1)
        {
            StartCoroutine(BrightnessToAudio());
        }

        // TO EXIT AUDIO
        if (Input.GetMouseButtonDown(0) && Stage == 4)
        {
            StartCoroutine(EndIntro());
        }
	}
}
