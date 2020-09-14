using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 static public class ControlOptions : System.Object
{
    static public double GetAngle(float x2 = 0, float y2 = 1)
    {
        float x1 = Input.GetAxis("RightStickX");
        float y1 = Input.GetAxis("RightStickY");
        double alpha = (x1 * x2 + y1 * y2)
            / (Math.Sqrt(x1 * x1 + y1 * y1) * Math.Sqrt(x2 * x2 + y2 * y2));
        double angle = Math.Acos(alpha) * 180 / Math.PI;
        return angle;
    }
}
