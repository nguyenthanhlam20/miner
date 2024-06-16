using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public int CountDownTimer { set; get; } = 0;

    public int scoreCount = 0;

    public int scoreToWin = 0;

    public Text countDownText;

    public Text scoreCountText;

    public Image scoreFillUI;

    public bool IsGameOver = false;

    public bool IsWinLevel = false;
    public int CurrentLevel { get; set; } = 1;

    private void Awake()
    {
        IsGameOver = false;
        CurrentLevel = UserDataManager.Instance.UserData.CurrentLevel;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Countdown()
    {
        countDownText.text = CountDownTimer.ToString();
        CountDownTimer--;
        if (CountDownTimer <= 10) AudioManager.instance.Play(AudioName.TimeRunningOut);
        if (CountDownTimer <= 0 && scoreCount != scoreToWin) GameOver();
        yield return new WaitForSeconds(1f);
        StartCoroutine("Countdown");
    }

    public void ShowTimer() => StartCoroutine("Countdown");

    public void DisplayScore(int scoreValue)
    {
        if (scoreCountText != null && scoreFillUI != null && scoreToWin > 0)
        {
            scoreCount += scoreValue;
            scoreCountText.text = scoreCount + "/" + scoreToWin;
            scoreFillUI.fillAmount = (float)scoreCount / (float)scoreToWin;
            if (scoreCount >= scoreToWin && !IsWinLevel) WinGame();
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        AudioManager.instance.PlayWhenGameStop();
        StopAllCoroutines();
        SaveWhenGameOver();
        PopupManager.Instance.ShowGameOverPopup();
    }

    private void WinGame()
    {
        IsWinLevel = true;
        UserDataManager.Instance.UserData.CurrentLevel = CurrentLevel + 1;
        UserDataManager.Instance.SaveUserData();
        SceneManager.LoadScene(SceneName.YouWin);
    }

    private void SaveWhenGameOver()
    {
        var highestLevel = UserDataManager.Instance.UserData.HighestLevel;

        if (CurrentLevel > highestLevel)
        {
            UserDataManager.Instance.UserData.HighestLevel = CurrentLevel;
            UserDataManager.Instance.UserData.CurrentLevel = 1;
            UserDataManager.Instance.SaveUserData();
        }
    }
}
