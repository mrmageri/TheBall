using SaveSystem;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private LevelButton[] levelButtons;
    private int _currentLevel;
    private GameSaveSystem _gameSaveSystem;
    private void Awake()
    {
        _gameSaveSystem = GetComponent<GameSaveSystem>();
        _currentLevel = _gameSaveSystem.LoadGameProgress();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].PresetButton(i + 1,this,i <= _currentLevel);
        }
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
