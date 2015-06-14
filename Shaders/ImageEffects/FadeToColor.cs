using UnityEngine;


[ExecuteInEditMode]
[AddComponentMenu("CFEffects/FadeToColor")]
public class FadeToColor : ImageEffectBase {

    //public Texture textureRamp;
   // public float rampOffset = 1;
    public Color Solid =  new Color (0,0,0,1);

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //material.SetTexture("_RampTex", textureRamp);
        //material.SetFloat("_RampOffset", rampOffset);
        material.SetColor("_SolidColor",  Solid);
        Graphics.Blit(source, destination, material);
    }
}
