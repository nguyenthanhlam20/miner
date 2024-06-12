using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
	[SerializeField]
	private string whichScene;

	public void LoadScene()
	{
		Debug.Log("Loading Scene: " + whichScene);
		AudioManager.instance.PausePlay("BG", paused: false);
		SceneManager.LoadScene(whichScene);
		AudioManager.instance.StopPlay("Win");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadMainMenu()
	{
		Debug.Log("Loading Scene: MainMenu");
		AudioManager.instance.PausePlay("BG", paused: false);
		SceneManager.LoadScene("MainMenu");
		AudioManager.instance.StopPlay("Win");
	}
}
