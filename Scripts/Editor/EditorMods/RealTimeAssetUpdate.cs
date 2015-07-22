using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
class RealTimeAssetUpdate {
    static int timer = 0;


    static void updateDataBase() {
        if (!Application.isPlaying) {
            timer += 1;
            if (timer > 1000) {
                timer = 0;
                AssetDatabase.Refresh();
                Debug.Log("test");
            }
        }
    }

    static RealTimeAssetUpdate() {
        Debug.Log("IT LOADED");

        //EditorApplication.update += updateDataBase;

    }

    public void OnGUI() {
        Debug.Log(Event.current);
    }
}
