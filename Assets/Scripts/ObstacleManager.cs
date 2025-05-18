using UI;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObjectUI[] objectUis;
    [SerializeField] private GameObject buttonGameObject;
    [SerializeField] private Transform layoutTransform;

    private void Awake()
    {
        for (int i = 0; i < objectUis.Length; i++)
        {
           GameObject obj =  Instantiate(buttonGameObject, layoutTransform);
           ObstacleButton newButton = obj.GetComponent<ObstacleButton>();
           newButton.SetObstacleSettings(objectUis[i].amount,objectUis[i].obstacleObject);
        }
    }
    
}
