using UI;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleManager : MonoBehaviour
    {
        public ObjectUI[] objectUis;
        private ObstacleButton[] _buttons;
        [SerializeField] private GameObject buttonGameObject;
        [SerializeField] private Transform layoutTransform;
        [SerializeField] private LayerMask cellLayerMask;


        private void Awake()
        {
            _buttons = new ObstacleButton[objectUis.Length];
            for (int i = 0; i < objectUis.Length; i++)
            {
                GameObject obj =  Instantiate(buttonGameObject, layoutTransform);
                ObstacleButton newButton = obj.GetComponent<ObstacleButton>();
                _buttons[i] = newButton;
                newButton.SetObstacleSettings(objectUis[i].amount,objectUis[i].obstacleObject);
            }
        }
    
        public void PresetObstacleObjects()
        {
            foreach (var elem in FindObjectsByType<ObstacleObject>(FindObjectsSortMode.InstanceID))
            {
                if (Physics.Raycast(elem.transform.position, -elem.transform.up, out RaycastHit hit, 50f,
                    cellLayerMask))
                {
                    if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                    {
                        elem.SetCell(cell);
                        elem.SetMovableStatus(false); //TODO REMOVE and Remake
                        cell.PlaceObject();
                    }
                }
            }
        }

        public ObstacleButton GetObstacleButton(int id)
        {
            if(id >= _buttons.Length) return null;
            return _buttons[id];
        }

        public void ReturnObstacle(int id)
        {
            if(id >= _buttons.Length) return;
            _buttons[id].AddObstacleAmount();
        }
    
    }
}
