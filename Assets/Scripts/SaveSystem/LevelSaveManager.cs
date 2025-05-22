using System.Collections.Generic;
using System.IO;
using Obstacle;
using UnityEngine;

namespace SaveSystem
{
    public class LevelSaveManager : MonoBehaviour
    {
        public GameObject[] obstacleTypes;
        [HideInInspector] public List<ObstacleSave> obstacleSaves = new List<ObstacleSave>();
        [SerializeField]private ObstacleManager obstacleManager;

        

        public void SaveLevel(int levelID)
        {
            obstacleSaves.Clear();
            var obstacles = FindObjectsByType<ObstacleObject>(FindObjectsSortMode.InstanceID);
            for (int i = 0; i < obstacles.Length; i++)
            {
                ObstacleSave obstacleSave = new ObstacleSave
                {
                    obstacleID = obstacles[i].GetId(),
                    posX = obstacles[i].transform.position.x,
                    posY = obstacles[i].transform.position.y,
                    posZ = obstacles[i].transform.position.z,
                    rotX = obstacles[i].transform.rotation.x,
                    rotY = obstacles[i].transform.rotation.y,
                    rotZ = obstacles[i].transform.rotation.z,
                    rotW = obstacles[i].transform.rotation.w
                };
                if (obstacles[i].TryGetComponent(out ObstacleObject obstacleObject))
                    obstacleSave.isMovable = obstacleObject.GetMovableStatus();
                obstacleSaves.Add(obstacleSave);
            }
            

            string dir = Application.persistentDataPath;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(this);
            
            File.WriteAllText(dir + "/" + levelID + "level.theball",json);
        }
        
        public void LoadLevel(int levelID)
        {
            string path = Application.persistentDataPath +  "/" + levelID + "level.theball";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
            }
            else
            {
                return;
            }

            var obstacles = FindObjectsByType<ObstacleObject>(FindObjectsSortMode.InstanceID);
            foreach (var elem in obstacles)
            {
                Destroy(elem.gameObject);
            }

            if (!obstacleManager) obstacleManager = GetComponent<ObstacleManager>();
            
            for (int i = 0; i < obstacleSaves.Count; i++)
            {
                Vector3 lastPoint = new Vector3(obstacleSaves[i].posX, obstacleSaves[i].posY, obstacleSaves[i].posZ);
                Quaternion quaternion = new Quaternion(obstacleSaves[i].rotX,obstacleSaves[i].rotY,obstacleSaves[i].rotZ,obstacleSaves[i].rotW);
                GameObject newObstacle = Instantiate(obstacleTypes[obstacleSaves[i].obstacleID], lastPoint, quaternion);
                if (newObstacle.TryGetComponent(out ObstacleObject obstacleObject))
                {
                    obstacleObject.SetMovableStatus(obstacleSaves[i].isMovable);
                    if (obstacleSaves[i].isMovable)
                    {
                        obstacleManager.GetObstacleButton(obstacleSaves[i].obstacleID).DecreaseObstacleAmount();
                    }
                }
                obstacleManager.PresetObstacleObjects(false);
            }
        }

        public void DeleteSave(int levelID)
        {
            string path = Application.persistentDataPath + "/" + levelID + "level.theball";
            File.Delete(path);
        }
    }
}
