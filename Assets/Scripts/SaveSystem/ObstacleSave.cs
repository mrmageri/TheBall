

using System;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class ObstacleSave
    {
        public int obstacleID;
        public bool isMovable;
        public float posX;
        public float posY;
        public float posZ;
        public float rotX;
        public float rotY;
        public float rotZ;
        public float rotW;
    }
}
