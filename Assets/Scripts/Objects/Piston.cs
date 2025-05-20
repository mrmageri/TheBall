using Obstacle;
using UnityEngine;

namespace Objects
{
    public class Piston : ObstacleObject
    {
        [SerializeField] private float punchForce;
        private Rigidbody _playerRb;
        private Animator _animator;
        private static readonly int Push = Animator.StringToHash("Push");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_playerRb == null) if (other.TryGetComponent(out Rigidbody rb)) _playerRb = rb;
                //_playerRb.angularVelocity = Vector3.zero;
                _playerRb.linearVelocity = Vector3.zero;
                _playerRb.AddForce(transform.forward * punchForce,ForceMode.Impulse);
                _animator.SetTrigger(Push);
            }
        }
    }
}
