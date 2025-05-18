using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObstacleButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text amountText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Vector3 spawnPos;
        private ObstacleScrObj _obstacleObj;
        private int _obstacleAmount;
        private ObjectPlacer _objectPlacer;
        private GameObject _thisObstacle;

        private void Awake()
        {
            _objectPlacer = FindFirstObjectByType<ObjectPlacer>();
        }

        public void SetObstacleSettings(int amount, ObstacleScrObj newObstacle)
        {
            _obstacleObj = newObstacle;
            _obstacleAmount = amount;
            _thisObstacle = _obstacleObj.obstacle;
            button.image.sprite = _obstacleObj.icon;
            nameText.text = _obstacleObj.obstacleName;
            SetObstacleAmount(amount);
        }

        private void SetObstacleAmount(int newAmount)
        {
            _obstacleAmount = newAmount;
            amountText.text = _obstacleAmount.ToString();
        }
        
        public void OnButtonClick()
        {
            _obstacleAmount--;
            if (_obstacleAmount < 0)
            {
                button.interactable = false;
                return;
            }
            _objectPlacer.SetSelectedObject(Instantiate(_thisObstacle,spawnPos,quaternion.identity));
            amountText.text = _obstacleAmount.ToString();
        }
    }
}
