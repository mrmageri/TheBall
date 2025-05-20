using Obstacle;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameStared;
    private GameObject _playerGameObject;
    private SphereCollider _playerCollider;
    private Rigidbody _playerRb;
    private ObstaclePlacer _obstaclePlacer;

    private void Awake()
    {
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _playerGameObject.GetComponent<SphereCollider>();
        _playerRb = _playerGameObject.GetComponent<Rigidbody>();
        _obstaclePlacer = GetComponent<ObstaclePlacer>();
        DisablePlayer();
    }

    private void DisablePlayer()
    {
        _playerRb.isKinematic = true;
        _playerCollider.enabled = false;
    }

    public void StartGame()
    {
        if(_gameStared) return;
        _gameStared = true;
        _playerCollider.enabled = true;
        _playerRb.isKinematic = false;
        _obstaclePlacer.DisablePlacing();
        _obstaclePlacer.RemoveObject();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
