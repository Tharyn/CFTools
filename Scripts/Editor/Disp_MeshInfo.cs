using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic; // NEEDED FOR (Lists)

/* 
 Notes:
 Add offset if the Scene view is Docked.
 http://answers.unity3d.com/questions/62594/is-there-an-editorwindow-is-docked-value.html
  
  
 */



[ExecuteInEditMode]
public class Disp_MeshInfo : MonoBehaviour 
{
    public int vertexCount;
    public bool vertexNumbers;
    
    public bool dark;
    public Vector3[] vertices;
    public int[] triangles;
    public MeshFilter meshFilter;
    public Mesh mesh;

    public bool showNormals = false;
    public bool showTangents = false;
    public float displayLengthScale = 0.5f;
    public int nth = 1;
    public Color normalColor = Color.red;
    public Color tangentColor = Color.blue;

    public bool vertexTicks;
    Texture2D vertexTick;

    Texture2D screenDrawTex;

    public bool screenDrawSVD;
    public bool screenDrawSVF;
    public bool screenDrawGUID;


	// Use this for initialization
	void Start () 
	{
        vertexTick = new Texture2D(3, 3);
        vertexTick.SetPixel(0, 0, Color.white);
        vertexTick.SetPixel(0, 1, Color.white);
        vertexTick.SetPixel(0, 2, Color.white);
        
        vertexTick.SetPixel(1, 0, Color.white);
        vertexTick.SetPixel(1, 1, Color.white);
        vertexTick.SetPixel(1, 2, Color.white);
        
        vertexTick.SetPixel(2, 0, Color.white);
        vertexTick.SetPixel(2, 1, Color.white);
        vertexTick.SetPixel(2, 2, Color.white);
        
        vertexTick.Apply();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

    public static void OnScene(SceneView sceneview)
    {

    }

    void OnGUI()
    {
        if (screenDrawGUID)
        {
            int x = 100;
            int y = 100;

            screenDrawTex = new Texture2D(x, y);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i < 1 | i > x - 2 | j < 1 | j > y - 2)
                        screenDrawTex.SetPixel(i, j, Color.red);
                    else
                        screenDrawTex.SetPixel(i, j, new Color(0,0,0,0) );
                }
            }
            screenDrawTex.Apply();

            GUI.DrawTexture(new Rect(0, 0, x, y), screenDrawTex, ScaleMode.ScaleToFit);
        }
    }


   
    void OnDrawGizmosSelected()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogWarning("Cannot find MeshFilter");
            return;
        }
		mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            Debug.LogWarning("Cannot find mesh");
            return;
        }
        vertices = mesh.vertices;

        Handles.BeginGUI();
        Camera camera = SceneView.lastActiveSceneView.camera;

        if (vertexNumbers)
        {
            if (!dark)
                GUI.color = Color.white;
            else
                GUI.color = Color.black;

            vertexCount = vertices.Length;
            for (int i = 0; i < vertices.Length; i += nth)
            {
                Vector2 screenPos = camera.WorldToScreenPoint(transform.TransformPoint(vertices[i]));
                GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y - 50, 40, 20), i.ToString());
            }
        }

        if (vertexTicks)
        {
            vertexCount = vertices.Length;
            for (int i = 0; i < vertices.Length; i += nth)
            {
                Vector2 screenPos = camera.WorldToScreenPoint(transform.TransformPoint(vertices[i]));
                GUI.DrawTexture(new Rect(screenPos.x - 2, Screen.height - screenPos.y - 40, 3, 3), vertexTick, ScaleMode.ScaleToFit);
                
            }
        }

        // SceneView Docked
        if (screenDrawSVD)
        {
            screenDrawTex = new Texture2D(Screen.width, Screen.height);
            for (int i = 0; i < Screen.width; i++ )
            {
                for (int j = 0; j < Screen.height; j++)
                {
                    if (i < 1 | i > Screen.width - 6 | j < 39 | j > Screen.height - 2)
                        screenDrawTex.SetPixel(i, j, Color.red);
                }
            }
            screenDrawTex.Apply();
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenDrawTex, ScaleMode.ScaleToFit);
        }

        // SceneView Float
        if (screenDrawSVF)
        {
            screenDrawTex = new Texture2D(Screen.width, Screen.height);
            for (int i = 0; i < Screen.width; i++)
            {
                for (int j = 0; j < Screen.height; j++)
                {
                    if (i < 1 | i > Screen.width - 2 | j < 40 | j > Screen.height - 2)
                        screenDrawTex.SetPixel(i, j, Color.red);
                }
            }
            screenDrawTex.Apply();
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenDrawTex, ScaleMode.ScaleToFit);
        }




        bool doShowNormals = showNormals && mesh.normals.Length == mesh.vertices.Length;
        bool doShowTangents = showTangents && mesh.tangents.Length == mesh.vertices.Length;

        triangles = mesh.triangles;
        //foreach (int idx in mesh.triangles)
        for (int idx = 0; idx < mesh.triangles.Length; idx += nth)
        {
            Vector3 vertex = transform.TransformPoint(mesh.vertices[ mesh.triangles[idx] ]);

            if (doShowNormals)
            {
                Vector3 normal = transform.TransformDirection(mesh.normals[ mesh.triangles[idx] ]);
                Gizmos.color = normalColor;
                Gizmos.DrawLine(vertex, vertex + normal * displayLengthScale);
            }
            if (doShowTangents)
            {
                Vector3 tangent = transform.TransformDirection(mesh.tangents[ mesh.triangles[idx] ]);
                Gizmos.color = tangentColor;
                Gizmos.DrawLine(vertex, vertex + tangent * displayLengthScale);
            }
        }

        GUI.color = Color.white; // Switch back to white or other GUI elements will adopt the color(like buttons drawn after)

        // GUI.Button(new Rect(0, Screen.height - 80, Screen.width - 4, 20), "Exit Isolation Mode");

        Handles.EndGUI();	
    }
}

/*
10 = 






*/