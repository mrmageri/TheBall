using UnityEngine;

namespace Objects
{
    public class SlowingGoo : ObstacleObject
    {
        [SerializeField] private float punchForce;
        private Rigidbody _playerRb;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_playerRb == null) if (other.TryGetComponent(out Rigidbody rb)) _playerRb = rb;
                _playerRb.linearVelocity *= punchForce;
            }
        }
    }
}
