using UnityEngine;
using System.Collections;

public class BuildMeshGameObject : MonoBehaviour
{

    // reference to new object, with mesh/texture
    private GameObject newGO;

    // reference to mesh/texture new object will use
    public Mesh theMesh;
    public Texture2D theTexture;

    // Use this for initialization
    void Start()
    {
        BuildNew();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildNew()
    {
        if (newGO == null)
        {
            MeshFilter MFC;
            MeshRenderer MRC;

            // create object
            newGO = new GameObject();
            newGO.name = "Created";

            // add meshfilter, keeping reference to access it
            MFC = newGO.AddComponent<MeshFilter>();
            MFC.mesh = theMesh;

            // add mesh renderer
            MRC = newGO.AddComponent<MeshRenderer>();

            // loop thru all materials
            for (int i = 0; i < MRC.materials.Length; i++)
            {
                Debug.Log("material:" + i);

                // assign shader
                MRC.materials[i].shader = Shader.Find("Diffuse");

                // bit of color, mid green
                MRC.materials[i].SetColor("_Color", new Color(0, 0.5f, 0, 0));

                // assign texture
                MRC.materials[i].SetTexture("_MainTex", theTexture);
            }
        }
    }
}
