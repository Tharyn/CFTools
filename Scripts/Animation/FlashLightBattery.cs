using UnityEngine;
using System.Collections;

public class FlashLightBattery : MonoBehaviour {

    public bool HasPlayed = false;
    public bool PlayInstructions = true;
    public StartUpScript SUS;
    public CF_HandControl HC;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("In Flash Light Trigger");
        if (HasPlayed == false)
        {
            HasPlayed = true;
            StartCoroutine(SUS.TurnOffFlashLight(HC, PlayInstructions));

        }

    }
}
