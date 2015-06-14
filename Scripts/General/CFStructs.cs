using UnityEngine;
using System;

namespace CFStructs
{
    // Rectangle of four points, clockwise from top left
    public struct Rectangle
    {
        public Vector3 p1, p2, p3, p4; // Past Position, Current Position, Velocity 

        public Rectangle(Vector3 a1, Vector3 a2, Vector3 a3, Vector3 a4)
        {
            p1 = a1;
            p2 = a2;
            p3 = a3;
            p4 = a4;
        }
    }
}
