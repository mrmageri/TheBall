using Obstacle;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] cellsObjects;
        public const int YMapSize = 9;
        public const int XMapSize = 15;
        [SerializeField] public bool[] cellMap = new bool[YMapSize * XMapSize];
        [SerializeField] private ObstacleManager obstacleManager;
        
        
         private void Awake()
         { 
             obstacleManager = GetComponent<ObstacleManager>();
             GenerateCells();
         }
        

        public void GenerateCells()
        {
            ClearCells();
            for (int i = 0; i < XMapSize; i++)
            {
                for (int j = 0; j < YMapSize; j++)
                {
                    if (cellMap[j * XMapSize + i]) cellsObjects[j * XMapSize + i].SetActive(true);
                }
            }
            obstacleManager.PresetObstacleObjects();
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
            cellMap = new bool[XMapSize * YMapSize];
                for (int i = 0; i < XMapSize; i++)
                {
                    for (int j = 0; j < YMapSize; j++)
                    {
                        cellMap[j * XMapSize + i] = isFull;
                    }
                }
        }
    }
}
