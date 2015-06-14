using UnityEngine;
using System.Collections;

public class TestAnimationEvent : MonoBehaviour {

    public GameObject Cam;
    public GameObject Master;
    public AudioSource PlayerAudio;
    public StartUpScript OpeningController;

    // This C# function can be called by an Animation Event
    public void StartFPS(float theValue)
    {

        Cam.transform.parent = Master.transform;
        Master.GetComponent<MouseLook>().enabled = true;
        Master.GetComponent<CharacterMotor>().enabled = true;
        
    }

    public void PlayWhereAmI(float theValue)
    {
        PlayerAudio.Play();
    }

    public void StartMainGUI(float theValue)
    {
        Debug.Log("In start main gui A");
        StartCoroutine(OpeningController.GetComponent<StartUpScript>().StartInGameGUI());

    }


}
