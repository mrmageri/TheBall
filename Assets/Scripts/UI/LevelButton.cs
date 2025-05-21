using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text numberText;
        [SerializeField] private Button button;

        public void PresetButton(int levelID,MainMenuManager mainMenuManager, bool isEnabled)
        {
            numberText.text = levelID.ToString();
            button.onClick.AddListener(delegate {mainMenuManager.LoadScene(levelID); });
            button.interactable = isEnabled;
        }
    }
}
