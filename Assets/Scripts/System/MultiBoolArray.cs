using UnityEngine;

namespace System
{
    [Serializable]public class MultiBoolArray{

        [SerializeField]
        private int width;

        [SerializeField]
        private int height;

        [SerializeField]
        private bool[] values;

        public int Width { get { return width; } }
    
        public int Height { get { return height; } }

        public bool this[int x, int y]
        {
            get { return values[y * width + x]; }
            set { values[y * width + x] = value; }
        }

        public MultiBoolArray(int width, int height)
        {
            this.width = width;
            this.height = height;

            values = new bool[width * height];
        }

    }
}
