using UnityEngine;
using System.Collections;
using System.IO;

public class GGXlutBuilder : MonoBehaviour {

    float width = 512;
    float height = 512;
    public Texture2D tex;

    public Vector3[] val3;
    public Vector4[] val4;
    public Color32[]  col4;
    public Color[] colc4;
    public Vector4[] val4b;
    public Vector3[] val3b;

    float LightingFuncGGX_D(float dotNH, float roughness) {
        float fixRough = Mathf.Max(roughness, .1f);
        float alpha = fixRough * fixRough;
        float alphaSqr = alpha * alpha;
        float pi = 3.14159f;
        float denom = dotNH * dotNH * (alphaSqr - 1.0f) + 1.0f;

        float D = alphaSqr / (pi * denom * denom);
        return D;
    }


    Vector2 LightingFuncGGX_FV(float dotLH, float roughness) {
        float alpha = roughness * roughness;

        // F
        float F_a, F_b;
        float dotLH5 = Mathf.Pow(1.0f - dotLH, 5);
        F_a = 1.0f;
        F_b = dotLH5;

        // V
        float vis;
        float k = alpha / 2.0f;
        float k2 = k * k;
        float invK2 = 1.0f - k2;
        vis = dotLH * dotLH * invK2 + k2;

        return (new Vector2(F_a * vis, F_b * vis));
    }

    // Finds the largest componant of a Vector3
    float MaxComp (Vector3 vect3) {

        float max = 0;
        if (vect3.x > max) max = vect3.x;
        if (vect3.y > max) max = vect3.y;
        if (vect3.z > max) max = vect3.z;

        return max;
    }

	// Use this for initialization
	void Start () {
        val3 = new Vector3[(int)(width * height)];
        val4 = new Vector4[(int)(width * height)];
        col4 = new Color32[(int)(width * height)];
        colc4 = new Color[(int)(width * height)];
        val4b = new Vector4[(int)(width * height)];
        val3b = new Vector3[(int)(width * height)];

        tex = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);


        Color[] pixels = new Color[(int)(width * height)];

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {

                float D = LightingFuncGGX_D((x / width), (y / height));
                Vector2 FV = LightingFuncGGX_FV((x / width), (y / height));

                Vector3 InterV3 = new Vector3(D, FV.x, FV.y);
                val3[x + (y * (int)width)] = InterV3;

                float maxComp = MaxComp(InterV3);
                
                float r =   (InterV3.x / maxComp);
                float g =   (InterV3.y / maxComp);
                float b =   (InterV3.z / maxComp);
                float a =   (maxComp);

                pixels[x + (y * (int)width)] = new Color(r, g, b, a);

                val4[x + (y * (int)width)] = new Vector4(r, g, b, a);
                col4[x + (y * (int)width)] = new Color(r, g, b, a);
                colc4[x + (y * (int)width)] = col4[x + (y * (int)width)];
                val4b[x + (y * (int)width)] = colc4[x + (y * (int)width)];

                Vector3 tempV = new Vector3();
                tempV.x = val4b[x + (y * (int)width)].w * val4b[x + (y * (int)width)].x;
                tempV.y = val4b[x + (y * (int)width)].w * val4b[x + (y * (int)width)].y;
                tempV.z = val4b[x + (y * (int)width)].w * val4b[x + (y * (int)width)].z;
                val3b[x + (y * (int)width)] = tempV;



            }
        }
        tex.SetPixels(pixels);
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "\\testLut2.png", bytes);       

	}

    // Update is called once per frame
    void Update() {
	
	}
}
