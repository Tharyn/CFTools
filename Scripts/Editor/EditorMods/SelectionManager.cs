using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)
using mset;

public class SelectionManager : EditorWindow {

    List<List<GameObject>> SelLists;

    public static EditorWindow window;


    [MenuItem("CF/Selection Manager")]
    public static void Launch() {
        window = GetWindow(typeof(SelectionManager));

    }










	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
