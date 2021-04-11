using System;
using UnityEngine;

namespace Assets.Data.Models
{
    [Serializable]
    public struct SerializableQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public SerializableQuaternion(float xComponent, float yComponent, float zComponent, float wComponent)
        {
            x = xComponent;
            y = yComponent;
            z = zComponent;
            w = wComponent;
        }

        public Quaternion GetQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }
    }
}
