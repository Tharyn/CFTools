using UnityEngine;
using System.Collections;

public class Firefly_Output : MonoBehaviour {

    public bool render = false;
    public int FPS = 24; 

    public int resWidth = 4096; 
    public int resHeight = 2232;

    public Vector2 range = new Vector2( 0, 100);
    public int currentFrame = 0;
    float secondsThisFrame = 0;

    string PushNumString(int num) {
        string pushNumString = num.ToString();
        while (pushNumString.Length < 4) {
            pushNumString = "0" + pushNumString;
        }

        return pushNumString;
    }

    void Start() {

    }

    void LateUpdate () {
        if (render) {
            secondsThisFrame += Time.deltaTime;
            if (secondsThisFrame < 1.0f / FPS) {
                return;
            } else {

                secondsThisFrame = 0;
                currentFrame += 1;
                string frameStringNum = PushNumString(currentFrame);

                RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
                camera.targetTexture = rt;
                Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
                camera.Render();
                RenderTexture.active = rt;
                screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                camera.targetTexture = null;
                RenderTexture.active = null; // JC: added to avoid errors 

                Destroy(rt);
                byte[] bytes = screenShot.EncodeToPNG();
                string filename = Application.dataPath + "/Screenshots/FireflyFrame." + frameStringNum + ".png";
                System.IO.File.WriteAllBytes(filename, bytes);

                if (Time.deltaTime < 1)
                    Debug.Log("Frame " + frameStringNum + " took: " + Time.deltaTime + " of a second.");
                else
                    Debug.Log("Frame " + frameStringNum + " took: " + Time.deltaTime + " seconds.");

                //Debug.Log(string.Format("Took screenshot to: {0}", filename)); 
                //
                //Debug.Log(PushNumString(currentFrame));
            }
        }
        
    } 

} 