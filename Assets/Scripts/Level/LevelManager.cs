using System;
using Unity.Mathematics;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        public const int YMapSize = 9;
        public const int XMapSize = 15;
        private readonly Vector2 _firstCellPos = new Vector2(-7,4);
        [HideInInspector] public MultiBoolArray cellMap;

        private void OnValidate()
        {
            if (cellMap == null)
            {
                cellMap = new MultiBoolArray(XMapSize, YMapSize);
                for (int i = 0; i < XMapSize; i++)
                {
                    for (int j = 0; j < YMapSize; j++)
                    {
                        cellMap[i, j] = true;
                    }
                }
            }
        }

        private void Awake()
        {
        
            for (int i = 0; i < XMapSize; i++)
            {
                for (int j = 0; j < YMapSize; j++)
                {
                    if (cellMap[i, j]) Instantiate(cellPrefab, new Vector3(_firstCellPos.x + i, 0, _firstCellPos.y - j),quaternion.identity);
                }
            }
        }

        public void ResetMap(bool isFull = false)
        {
            cellMap = new MultiBoolArray(XMapSize, YMapSize);
                for (int i = 0; i < XMapSize; i++)
                {
                    for (int j = 0; j < YMapSize; j++)
                    {
                        cellMap[i, j] = isFull;
                    }
                }
        }
    }
}
