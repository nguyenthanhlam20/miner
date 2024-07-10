using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

	[SerializeField] private Button play;
	[SerializeField] private Button quit;

    private void Awake()
    {
		play.onClick.AddListener(() => Play());
        quit.onClick.AddListener(() => QuitGame());
    }

    public void Play()
	{
		AudioManager.instance.PausePlay(AudioName.BG, paused: false);
		SceneManager.LoadScene(SceneName.Profile);
		AudioManager.instance.StopPlay(AudioName.Win);
	}

	public void QuitGame() => Application.Quit();
}
