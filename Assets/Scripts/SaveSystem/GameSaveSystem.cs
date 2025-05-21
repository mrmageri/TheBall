using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class GameSaveSystem : MonoBehaviour
    {
        [HideInInspector]public int lastLevel;
        
        public void SaveGameProgress(int levelID)
        {
            LoadGameProgress();
            if(lastLevel >= levelID) return;
            
            lastLevel = levelID;

            string dir = Application.persistentDataPath;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(this);
            
            File.WriteAllText(dir + "/game.theball",json);
        }
        
        public int LoadGameProgress()
        {
            string path = Application.persistentDataPath +  "/game.theball";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
                return lastLevel;
            }
            else
            {
                return 0;
            }
        }

        public void DeleteGameSave()
        {
            string path = Application.persistentDataPath + "/game.theball";
            File.Delete(path);
        }
    }
}
