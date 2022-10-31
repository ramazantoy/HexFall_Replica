using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public static class Vector3Ext
    {
        public static Vector3 UpdateX(this Vector3 v, float val)
        {
            return new Vector3(val, v.y, v.z);
        }
        public static Vector3 UpdateY(this Vector3 v, float val) => new Vector3(v.x, val, v.z);
        public static Vector3 UpdateZ(this Vector3 v, float val) => new Vector3(v.x, v.y, val);
        
        public static float InverseLerp(this Vector3 value , Vector3 start, Vector3 end)
        {
            Vector3 difference = end - start;
            Vector3 progress = value - start;
            return Vector3.Dot(progress, difference) / Vector3.Dot(difference, difference);
        }
        
    
    }



