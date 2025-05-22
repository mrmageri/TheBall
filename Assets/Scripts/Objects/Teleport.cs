using Obstacle;
using UnityEngine;


namespace Objects
{
    public class Teleport : ObstacleObject
    {
        private Teleport _linkedTeleport;
        [SerializeField] private ParticleSystem particle;
        private Transform _playerTransform;
        private bool _isReceivingPlayer;
        
        public bool SetTeleport()
        {
            foreach (var elem in FindObjectsByType<Teleport>(FindObjectsSortMode.InstanceID))
            {
                if (elem != this)
                {
                    _linkedTeleport = elem;
                    elem.SetTeleport(this);
                    return true;
                }
            }
            return false;
        }

        public void SetTeleport(Teleport newTeleport)
        {
            _linkedTeleport = newTeleport;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (SetTeleport())
                {
                    particle.Play();
                    if (_isReceivingPlayer) return;
                    if (_playerTransform == null) _playerTransform = other.gameObject.transform;
                    _linkedTeleport.TeleportPlayer(_playerTransform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _isReceivingPlayer = false;
        }

        private void TeleportPlayer(Transform playerTr)
        {
            _isReceivingPlayer = true;
            if (_playerTransform == null) _playerTransform = playerTr;
            _playerTransform.position = transform.position;
        }
    }
}
