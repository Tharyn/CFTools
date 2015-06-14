using UnityEngine;
using System.Collections;

public class AwakeMe : MonoBehaviour
{
    
    void Awake()
    {
        Debug.Log("test");
    }

    void OnApplicationFocus()
    {
         Debug.Log("lost");
    }

    void OnApplicationQuit()
    {
        Debug.Log("quit");
    }
}