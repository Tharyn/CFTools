using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class RenderFames : MonoBehaviour {




    
    public int width = 1280;
    public int height = 720;
    public RenderTexture theFrame;
    public int frameNum = 0;
    //public Camera camera;
    public string FileName;
    byte[] bytes;

	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        
        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = theFrame;
        tex.ReadPixels(new Rect(0, 0, theFrame.width, theFrame.height), 0, 0);


        //bytes = tex.EncodeToPNG();
        //File.WriteAllBytes((Application.dataPath + "/Frames/" + FileName + "." + frameNum + ".png"), bytes);
        Debug.Log(tex);
	}
}

    /*
        bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/" + cubemap.name + "_PositiveX.png", bytes);       
    */