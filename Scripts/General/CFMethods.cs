using UnityEngine;
using System;
using CFStructs;

public class CFMethods : MonoBehaviour 
{
    public static Rectangle CalculateCameraFrustRect(Camera cam, float dist)
    {
        Rectangle rect = new Rectangle();
        float frustumHeight = 2.0F * dist * Mathf.Tan(cam.fieldOfView * 0.5F * Mathf.Deg2Rad);
        rect.p1 = new Vector3(-frustumHeight * cam.aspect / 2, frustumHeight / 2, dist);
        rect.p2 = new Vector3(frustumHeight * cam.aspect / 2, frustumHeight / 2, dist);
        rect.p3 = new Vector3(frustumHeight * cam.aspect / 2, -frustumHeight / 2, dist);
        rect.p4 = new Vector3(-frustumHeight * cam.aspect / 2, -frustumHeight / 2, dist);

        return (rect);
    }

    // Casts a ray to find the coord in perspective camera
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
 

}
