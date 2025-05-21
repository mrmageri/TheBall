using Obstacle;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameStared;
    private GameObject _playerGameObject;
    private SphereCollider _playerCollider;
    private Rigidbody _playerRb;
    private ObstaclePlacer _obstaclePlacer;
    private ObstacleManager _obstacleManager;
    private LevelSaveManager _levelSaveManager;
    private GameSaveSystem _gameSaveManager;
    private int _sceneID;

    private void Awake()
    {
        _levelSaveManager = GetComponent<LevelSaveManager>();
        _gameSaveManager = GetComponent<GameSaveSystem>();
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _playerGameObject.GetComponent<SphereCollider>();
        _playerRb = _playerGameObject.GetComponent<Rigidbody>();
        _obstaclePlacer = GetComponent<ObstaclePlacer>();
        _obstacleManager = GetComponent<ObstacleManager>();
        DisablePlayer();
        _sceneID = SceneManager.GetActiveScene().buildIndex;
        if (_obstacleManager.GenerateObstacleButtons()) _levelSaveManager.LoadLevel(_sceneID);
    }

    public void SaveLevel()
    {
        _levelSaveManager.SaveLevel(_sceneID);
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
        _gameSaveManager.SaveGameProgress(_sceneID);
        SaveLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_sceneID);
    }

    public void ResetLevel()
    {
        _levelSaveManager.DeleteSave(_sceneID);
        SceneManager.LoadScene(_sceneID);
    }
    
}
