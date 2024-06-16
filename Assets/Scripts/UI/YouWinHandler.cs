using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouWinHandler : MonoBehaviour
{
    [SerializeField] private Button next;
    [SerializeField] private Button mainMenu;

    private void Awake()
    {
        AudioManager.instance.PlayWhenWinGame();
        next.onClick.AddListener(() => GoToNextLevel());
        mainMenu.onClick.AddListener(() => GoToMainMenu());
    }

    private void GoToMainMenu()
    {
        AudioManager.instance.PausePlay(AudioName.BG, paused: false);
        SceneManager.LoadScene(SceneName.MainMenu);
        AudioManager.instance.StopPlay(AudioName.Win);
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneName.World);
        AudioManager.instance.PausePlay(AudioName.BG, paused: false);
    }

}
