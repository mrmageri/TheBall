using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObstacleInHandUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private float imageShift;
        [SerializeField] private Sprite emptySprite;

        private void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            image.transform.position = new Vector3(mousePos.x + imageShift,mousePos.y - imageShift,mousePos.z);
        }

        public void SetObstacleInHand(Sprite newSprite)
        {
            image.sprite = newSprite;
        }
        public void SetObstacleInHand()
        {
            image.sprite = emptySprite;
        }
    }
}
