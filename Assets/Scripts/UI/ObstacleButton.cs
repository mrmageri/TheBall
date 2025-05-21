using Obstacle;
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
        private ObstaclePlacer _obstaclePlacer;
        private GameObject _thisObstacle;
        private ObstacleInHandUI _obstacleInHand;

        private void Awake()
        {
            _obstaclePlacer = FindFirstObjectByType<ObstaclePlacer>();
            _obstacleInHand = FindFirstObjectByType<ObstacleInHandUI>();
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

        public void AddObstacleAmount()
        {
            SetObstacleAmount(_obstacleAmount+1);
        }

        public void DecreaseObstacleAmount()
        {
            SetObstacleAmount(_obstacleAmount-1);
        }

        public Sprite GetObstacleSprite()
        {
            return _obstacleObj.icon;
        }

        private void SetObstacleAmount(int newAmount)
        {
            _obstacleAmount = newAmount;
            amountText.text = _obstacleAmount.ToString();
        }

        public void OnButtonClick()
        {
            if(_obstacleAmount == 0) return;
            _obstacleAmount--;
            _obstaclePlacer.SetSelectedObject(Instantiate(_thisObstacle,spawnPos,quaternion.identity));
            _obstacleInHand.gameObject.SetActive(true);
            _obstacleInHand.SetObstacleInHand(_obstacleObj.icon);
            amountText.text = _obstacleAmount.ToString();
        }
    }
}
