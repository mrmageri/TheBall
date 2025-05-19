using System;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] cellsObjects;
        public const int YMapSize = 9;
        public const int XMapSize = 15;
        [HideInInspector] public MultiBoolArray cellMap;
        private ObstacleManager _obstacleManager;
        
        
         private void Awake()
         {
             _obstacleManager = GetComponent<ObstacleManager>();
            GenerateCells();
         }
        

        public void GenerateCells()
        {
            ClearCells();
            for (int i = 0; i < XMapSize; i++)
            {
                for (int j = 0; j < YMapSize; j++)
                {
                    if (cellMap[i, j]) cellsObjects[j * XMapSize + i].SetActive(true);
                }
            }
            _obstacleManager.PresetObstacleObjects();
        }

        public void ClearCells()
        {
            foreach (var elem in cellsObjects)
            {
                elem.SetActive(false);
            }

            foreach (var elem in FindObjectsByType<ObstacleObject>(FindObjectsSortMode.InstanceID))
            {
                elem.SetCell(null);
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
