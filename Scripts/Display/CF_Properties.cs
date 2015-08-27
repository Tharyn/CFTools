using UnityEngine;
using CFStructs;

[ExecuteInEditMode]
public class CF_Properties : MonoBehaviour
{
    [HideInInspector]
    public bool selected = true;

    public bool gizmo = true;

    public float size = 1f;
    public Color color = new Color(.75f, .75f, .75f, .5F);

    Color whiteLine = new Color(.5F, .5F, .5F, .5F);
    Color grayLine = new Color(0F, 0F, 0F, .5F);
    Color whiteSelected = new Color(1, 1, 1, .75F);
    Color redLine = new Color(1, 0, 0, .5F);
    Color greenLine = new Color(0, 1, 0, .5F);
    Color blueLine = new Color(0, 0, 1, .5F);

    Color yellowLight = new Color(.75F, .75F, .25F, .5F);

    public Transform lookAtTarget;
    public bool invertZ;
    public bool selectTarget;


    Light aLight;
    Camera aCamera;

    void Start()
    {

    }

    void Update()
    {
        if (!Application.isPlaying) {
            if (lookAtTarget != null) {
                transform.LookAt(lookAtTarget, Vector3.up);
                if (invertZ)
                    transform.Rotate(0, 180, 0);
            }
        }
    }



    void FixedUpdate() {
        if (Application.isPlaying) {
            if (lookAtTarget != null) {
                transform.LookAt(lookAtTarget, Vector3.up);
                if (invertZ)
                    transform.Rotate(0, 180, 0);
            }
        }
    }






    // Add code to delete target with dialog (Can not use Unity Editor dialog here)
    /*
    void OnDestroy()
    {
        if (lookAtTarget != null)
            
        Debug.Log(lookAtTarget);
    }
    */
	
	void OnEnable () 
	{
		if (aCamera != null) 
		{
			aCamera.depthTextureMode = DepthTextureMode.Depth;
		}
	}

    #if UNITY_EDITOR 
    void OnDrawGizmos()
    {
        aCamera = gameObject.camera;
        aLight = gameObject.light;

        // Look At Target 
        if (lookAtTarget != null)
        {
            if (aCamera != null)
            {
                Gizmos.color = grayLine;
                Gizmos.DrawLine(transform.position, lookAtTarget.transform.position);
            }
            else if (aLight != null)
            {
                Gizmos.color = yellowLight;
                Gizmos.DrawLine(transform.position, lookAtTarget.transform.position);
            }
            else
            {
                Gizmos.color = grayLine;
                Gizmos.DrawLine(transform.position, lookAtTarget.transform.position);
            }
        }

        Gizmos.matrix = Matrix4x4.TRS(gameObject.transform.position, gameObject.transform.rotation, Vector3.one);

        // CAMERA
        if (aCamera != null)
        {
            if (!aCamera.orthographic && gizmo)
            {
                Gizmos.color = whiteLine;

                Rectangle far = CFMethods.CalculateCameraFrustRect(aCamera, aCamera.farClipPlane);
                Rectangle near = CFMethods.CalculateCameraFrustRect(aCamera, aCamera.nearClipPlane);

                Gizmos.DrawLine(far.p1, far.p2);
                Gizmos.DrawLine(far.p2, far.p3);
                Gizmos.DrawLine(far.p3, far.p4);
                Gizmos.DrawLine(far.p4, far.p1);
                Gizmos.DrawLine(near.p1, near.p2);
                Gizmos.DrawLine(near.p2, near.p3);
                Gizmos.DrawLine(near.p3, near.p4);
                Gizmos.DrawLine(near.p4, near.p1);
                Gizmos.DrawLine(near.p1, far.p1);
                Gizmos.DrawLine(near.p2, far.p2);
                Gizmos.DrawLine(near.p3, far.p3);
                Gizmos.DrawLine(near.p4, far.p4);

                Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(size, size, size));
                Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(size, size, size));
            }
            else if (gizmo)
            {
                // Needs VIEWPORT RECT adjustment added.
                Gizmos.color = whiteLine;

                // topLeftDown
                Vector3 nearTopLeft = new Vector3(aCamera.aspect  * -aCamera.orthographicSize, aCamera.orthographicSize, aCamera.nearClipPlane);
                Vector3 nearTopRight = new Vector3(aCamera.aspect * aCamera.orthographicSize, aCamera.orthographicSize, aCamera.nearClipPlane);
                Vector3 nearBotRight = new Vector3(aCamera.aspect * aCamera.orthographicSize, -aCamera.orthographicSize, aCamera.nearClipPlane);
                Vector3 nearBotLeft = new Vector3(aCamera.aspect * -aCamera.orthographicSize, -aCamera.orthographicSize, aCamera.nearClipPlane);

                Vector3 farTopLeft = new Vector3(aCamera.aspect * -aCamera.orthographicSize, aCamera.orthographicSize, aCamera.farClipPlane);
                Vector3 farTopRight = new Vector3(aCamera.aspect * aCamera.orthographicSize, aCamera.orthographicSize, aCamera.farClipPlane);
                Vector3 farBotRight = new Vector3(aCamera.aspect * aCamera.orthographicSize, -aCamera.orthographicSize, aCamera.farClipPlane);
                Vector3 farBotLeft = new Vector3(aCamera.aspect * -aCamera.orthographicSize, -aCamera.orthographicSize, aCamera.farClipPlane);


                //Near
                Gizmos.DrawLine(nearTopLeft, nearTopRight);
                Gizmos.DrawLine(nearTopRight, nearBotRight);
                Gizmos.DrawLine(nearBotRight, nearBotLeft);
                Gizmos.DrawLine(nearBotLeft, nearTopLeft);
                //Far
                Gizmos.DrawLine(farTopLeft, farTopRight);
                Gizmos.DrawLine(farTopRight, farBotRight);
                Gizmos.DrawLine(farBotRight, farBotLeft);
                Gizmos.DrawLine(farBotLeft, farTopLeft);
                // Connect
                Gizmos.DrawLine(nearTopLeft, farTopLeft);
                Gizmos.DrawLine(nearTopRight, farTopRight);
                Gizmos.DrawLine(nearBotRight, farBotRight);
                Gizmos.DrawLine(nearBotLeft, farBotLeft);

                Gizmos.color = whiteLine;
                Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(size, size, size));
                Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(size, size, size));
            }
        }
        else if (aLight != null)
        {

        }
        else // If nothing else draw cube
        {
            
            if (selected && gizmo)
            {
                Gizmos.color = color;
                Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(size * gameObject.transform.localScale.x, size * gameObject.transform.localScale.y, size * gameObject.transform.localScale.z));
                Gizmos.color = whiteSelected;
                Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(size * gameObject.transform.localScale.x, size * gameObject.transform.localScale.y, size * gameObject.transform.localScale.z));
            }
            else if (gizmo)
            {
                Gizmos.color = color;
                Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(size * gameObject.transform.localScale.x, size * gameObject.transform.localScale.y, size * gameObject.transform.localScale.z));
                Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(size * gameObject.transform.localScale.x, size * gameObject.transform.localScale.y, size * gameObject.transform.localScale.z));
            }
            Gizmos.color = redLine;
            Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(size * 4 * gameObject.transform.localScale.x, 0, 0));
            Gizmos.color = greenLine;
            Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, size * 4 * gameObject.transform.localScale.y, 0));
            Gizmos.color = blueLine;
            Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 0, size * 4 * gameObject.transform.localScale.z));
        }

    }
    #endif
}
