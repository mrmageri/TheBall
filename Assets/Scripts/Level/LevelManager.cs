using System;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LayerMask cellLayerMask;
        [SerializeField] private GameObject[] cellsObjects;
        public const int YMapSize = 9;
        public const int XMapSize = 15;
        [HideInInspector] public MultiBoolArray cellMap;
        
        
         private void Awake()
         {
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
            SetObstacleObjects();
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

        private void SetObstacleObjects()
        {
            foreach (var elem in FindObjectsByType<ObstacleObject>(FindObjectsSortMode.InstanceID))
            {
                if (Physics.Raycast(elem.transform.position, -elem.transform.up, out RaycastHit hit, 50f,
                    cellLayerMask))
                {
                    if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                    {
                        elem.SetCell(cell);
                        cell.PlaceObject();
                    }
                }
            }
        }
    }
}
