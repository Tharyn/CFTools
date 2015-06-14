using UnityEngine;
//using UnityEditor;
using System.Collections;
using System;

[ExecuteInEditMode]
public class Poly_Screen : MonoBehaviour
{
    public float length = 1f;
    public float width = 1f;
    public int resX = 2; // 2 minimum
    public int resZ = 2;

    public float yOff;
    Vector3[] vertices;
    Vector3[] normales;
    int[] triangles;
    public int triCount;
    public int nbFaces;

    int t;
    int i;

    MeshFilter voxelPlane;
    MeshRenderer geoRenderer;
    Transform lookAtCamera;

    public GameObject aCamera;

    void buildMesh()
    {
        vertices = new Vector3[resX * resZ];
        for (int z = 0; z < resZ; z++)
        {
            // [ -length / 2, length / 2 ]
            float zPos = ((float)z / (resZ - 1) - .5f) * length;
            for (int x = 0; x < resX; x++)
            {
                // [ -width / 2, width / 2 ]
                float xPos = ((float)x / (resX - 1) - .5f) * width;
                vertices[x + z * resX] = new Vector3(xPos, 0f, zPos);
            }
        }

        normales = new Vector3[vertices.Length];
        for (int n = 0; n < normales.Length; n++)
            normales[n] = Vector3.up;

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int v = 0; v < resZ; v++)
        {
            for (int u = 0; u < resX; u++)
            {
                uvs[u + v * resX] = new Vector2((float)u / (resX - 1), (float)v / (resZ - 1));
            }
        }

        nbFaces = (resX - 1) * (resZ - 1);
        //triangles = new int[(resX - 1) * 3 + (resZ - 1) * 3 ];
        triangles = new int[nbFaces * 6];
        t = 0;
        for (int face = 0; face < nbFaces; face++)
        {
            // Retrieve lower left corner from face ind
            int i = face % (resX - 1) + (face / (resZ - 1) * resX);

            triangles[t++] = i + resX;
            triangles[t++] = i + 1;
            triangles[t++] = i;

            triangles[t++] = i + resX;
            triangles[t++] = i + resX + 1;
            triangles[t++] = i + 1;
        }
        triCount = triangles.Length;

        voxelPlane.sharedMesh.vertices = vertices;
        voxelPlane.sharedMesh.normals = normales;
        voxelPlane.sharedMesh.uv = uvs;
        voxelPlane.sharedMesh.triangles = triangles;

        voxelPlane.sharedMesh.RecalculateBounds();
        voxelPlane.sharedMesh.Optimize();
        voxelPlane.sharedMesh.name = "Poly_Screen";
    }

    // Use this for initialization
    void Start()
    {
        voxelPlane = GetComponent<MeshFilter>();
        if (voxelPlane == null)
            voxelPlane = gameObject.AddComponent<MeshFilter>();
        voxelPlane.mesh = new Mesh();

        geoRenderer = GetComponent<MeshRenderer>();
        if (geoRenderer == null)
            geoRenderer = gameObject.AddComponent<MeshRenderer>();
        geoRenderer.castShadows = false;
        geoRenderer.receiveShadows = false;

        aCamera = GameObject.Find("Camera_Main");

        Material myNewMaterial = new Material(Shader.Find("Diffuse"));
        myNewMaterial.SetColor("_Color", new Color(0, 1f, 0, 0));
        //myNewMaterial.SetTexture("_MainTex", theTexture);
        geoRenderer.material = myNewMaterial;

        buildMesh();
    }

    // Update is called once per frame
    void Update()
    {
        buildMesh();
    }
}
