using System;
using UnityEngine;

namespace Assets.Data.Models
{
    [Serializable]
    public struct SerializableVector2
    {
        public float x;
        public float y;

        public SerializableVector2(float xPos, float yPos)
        {
            x = xPos;
            y = yPos;
        }

        public Vector2 GetVector2()
        {
            return new Vector2(x, y);
        }
    }
}
