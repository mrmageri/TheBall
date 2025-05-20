using Obstacle;
using UnityEngine;

namespace Objects
{
    public class SlowingGoo : ObstacleObject
    {
        [SerializeField] private float velocityDivider;
        [SerializeField] private float velocityMaxStep;
        private Rigidbody _playerRb;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_playerRb == null) if (other.TryGetComponent(out Rigidbody rb)) _playerRb = rb;
                if (_playerRb.linearVelocity.magnitude >= velocityMaxStep)
                {
                    _playerRb.linearVelocity /= (velocityDivider*2);
                }
                else
                {
                    _playerRb.linearVelocity /= velocityDivider;
                }
            }
        }
    }
}
