using System;
using System.Collections;
using Obstacle;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.25f;
    [SerializeField] private float losingDelayDuration;
    [SerializeField] private UnityEvent onLose;

    private bool _gameStared;
    private GameObject _playerGameObject;
    private SphereCollider _playerCollider;
    private Rigidbody _playerRb;
    private ObstaclePlacer _obstaclePlacer;
    private ObstacleManager _obstacleManager;
    private LevelSaveManager _levelSaveManager;
    private GameSaveSystem _gameSaveManager;
    private int _sceneID;
    private bool _lostGame = false;
    private bool _wonGame;
    private bool _isWaitingToLose;
    private Vector3 _playerStartPos;
    private void Awake()
    {
        _levelSaveManager = GetComponent<LevelSaveManager>();
        _gameSaveManager = GetComponent<GameSaveSystem>();
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _playerGameObject.GetComponent<SphereCollider>();
        _playerRb = _playerGameObject.GetComponent<Rigidbody>();
        _obstaclePlacer = GetComponent<ObstaclePlacer>();
        _obstacleManager = GetComponent<ObstacleManager>();
        _playerStartPos = _playerGameObject.transform.position;
        DisablePlayer();
        _sceneID = SceneManager.GetActiveScene().buildIndex;
        if (_obstacleManager.GenerateObstacleButtons()) _levelSaveManager.LoadLevel(_sceneID);
    }

    private void Update()
    {
        if (_playerRb.linearVelocity.magnitude <= minSpeed && !_lostGame && _gameStared && !_isWaitingToLose && !_wonGame) StartCoroutine(WaitForLosing());
        if (_lostGame && Input.anyKey) RestartLevel();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveLevel();
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DisablePlayer();
        LoseGame();
    }

    public void SaveLevel()
    {
        _levelSaveManager.SaveLevel(_sceneID);
    }

    private void LoseGame()
    {
        if(_playerGameObject.transform.position == _playerStartPos) return;
        Time.timeScale = 1;
        _lostGame = true;
        onLose.Invoke();
        StartCoroutine(LosingDelay());
    }
    
    private IEnumerator LosingDelay()
    {
        yield return new WaitForSeconds(losingDelayDuration);
        RestartLevel();
    }

    public void WinGame()
    {
        Time.timeScale = 1;
        _wonGame = true;
    }

    private IEnumerator WaitForLosing()
    {
        _isWaitingToLose = true;
        yield return new WaitForSeconds(losingDelayDuration);
        if (_playerRb.linearVelocity.magnitude <= minSpeed && !_wonGame)
        {
            LoseGame();
        }
        else
        {
            _isWaitingToLose = false;
        }
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
