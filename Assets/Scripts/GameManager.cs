using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public int countDownTimer = 60;

	public int scoreCount;

	public int scoreToWin;

	public Text countDownText;

	public Text scoreCountText;

	public Image scoreFillUI;

	public Image gameOverImage;

	public bool IsGameOver = false;

	private void Awake()
	{
		IsGameOver = false;

        if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
        StartCoroutine("Countdown");
    }

    private IEnumerator Countdown()
	{
		yield return new WaitForSeconds(1f);
		countDownTimer--;
		countDownText.text = countDownTimer.ToString();
		if (countDownTimer <= 10)
		{
			AudioManager.instance.Play("Time Running Out");
		}
		StartCoroutine("Countdown");
		if (countDownTimer <= 0 && scoreCount != scoreToWin)
		{
			StopGame();
        }
	}

	public void DisplayScore(int scoreValue)
	{
		if (!(scoreCountText == null))
		{
			scoreCount += scoreValue;
			scoreCountText.text = scoreCount + "/" + scoreToWin;
			scoreFillUI.fillAmount = (float)scoreCount / (float)scoreToWin;
			if (scoreCount >= scoreToWin)
			{
				StopCoroutine("Countdown");
				SceneManager.LoadScene("YouWin");
				AudioManager.instance.StopPlay("Time Running Out");
				AudioManager.instance.PausePlay("BG", paused: true);
				AudioManager.instance.Play("Win");
			}
		}
	}

	private IEnumerator RestartGame()
	{
		yield return new WaitForSeconds(4f);
		AudioManager.instance.PausePlay("BG", paused: false);
		gameOverImage.enabled = false;
		SceneManager.LoadScene("SampleScene");
	}

	public void StopGame()
	{
		IsGameOver = true;
        StopCoroutine("Countdown");
        gameOverImage.enabled = true;
        AudioManager.instance.StopPlay("Time Running Out");
        AudioManager.instance.PausePlay("BG", paused: true);
        AudioManager.instance.Play("Game End");
        StartCoroutine("RestartGame");
    }
}
