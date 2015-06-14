using UnityEngine;
using System.Collections;

public class TriggerSound : MonoBehaviour {
    public bool HasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Intriger");
        if (HasPlayed == false)
        {
            HasPlayed = true;

            this.audio.Play();
        }

    }
}
