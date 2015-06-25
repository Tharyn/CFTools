using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
class RealTimeAssetUpdate {
    static int timer = 0;

    static void updateDataBase() {
        if (!Application.isPlaying) {
            timer += 1;
            if (timer > 500) {
                timer = 0;
                //Debug.Log("Updateing");
                AssetDatabase.Refresh();
            }

        }
    }

    static RealTimeAssetUpdate() {
        Debug.Log("IT LOADED");
        EditorApplication.update += updateDataBase;
    }
}
