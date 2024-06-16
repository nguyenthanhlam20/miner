using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField] private Button playAgain;
    [SerializeField] private Button mainMenu;

    private void Awake()
    {
        playAgain.onClick.AddListener(() => PlayAgain());
        mainMenu.onClick.AddListener(() => GoToMainMenu());
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.PausePlay(AudioName.BG, paused: false);
    }
    private void GoToMainMenu()
    {
        AudioManager.instance.PausePlay(AudioName.BG, paused: false);
        SceneManager.LoadScene(SceneName.MainMenu);
        AudioManager.instance.StopPlay(AudioName.Win);
    }
}



