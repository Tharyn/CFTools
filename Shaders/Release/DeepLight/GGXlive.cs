using UnityEngine;
using System.Collections;
using System.IO;

[ExecuteInEditMode]
public class GGXlive : MonoBehaviour {

    float width = 512;
    float height = 512;
    public Texture2D GGXlut;

    float LightingFuncGGX_D(float dotNH, float roughness) {
        float fixRough = Mathf.Max(roughness, .1f);
        float alpha = roughness * roughness;
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
    void Start() {
        Debug.Log("Building GGX");
        Shader.SetGlobalTexture("_GGXlut", null);
        GGXlut = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, true);



        Color[] pixels = new Color[(int)(width * height)];

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {

                float D = LightingFuncGGX_D((x / width), (y / height));
                Vector2 FV = LightingFuncGGX_FV((x / width), (y / height));

                Vector3 InterV3 = new Vector3(D, FV.x, FV.y);
                float maxComp = MaxComp(InterV3);

                Color InterC = new Color(0, 0, 0, 0);
                /*
                float r = (InterV3.x / maxComp);
                float g = (InterV3.y / maxComp);
                float b = (InterV3.z / maxComp);
                float a = (maxComp);

                
                Color InterV3 = new Color(0,0,0,0);
                InterV3.r = D;
                InterV3.g = FV.x;
                InterV3.b = FV.y;
                InterV3.a = 1;
                pixels[x + (y * (int)width)] = InterV3;
                 */
                InterC.r = D * 100;
                InterC.g = FV.x * 100;
                InterC.b = FV.y * 100;
                InterC.a = 1;

                pixels[x + (y * (int)width)] = InterC;
            }
        }
        GGXlut.SetPixels(pixels);
        GGXlut.Apply();

        if (GGXlut != null) {
            
            Shader.SetGlobalTexture("_GGXlut", GGXlut);
            byte[] bytes = GGXlut.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "\\CurrentBuffer.png", bytes);      
            Debug.Log("GGX Done");

        } else Debug.Log("Didn't work");
        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
