using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouWinHandler : MonoBehaviour
{
    [SerializeField] private Button next;
    [SerializeField] private Button btnBomb;
    [SerializeField] private Button btnTimer;
    [SerializeField] private Button btnSpeed;
    [SerializeField] private TextMeshProUGUI bombPrice;
    [SerializeField] private TextMeshProUGUI timerPrice;
    [SerializeField] private TextMeshProUGUI speedPrice;
    [SerializeField] private TextMeshProUGUI txtMoney;
    [SerializeField] private TextMeshProUGUI txtMessage;
    [SerializeField] private TextMeshProUGUI txtCurrentBomb;
    [SerializeField] private TextMeshProUGUI txtCurrentTimer;
    [SerializeField] private TextMeshProUGUI txtCurrentSpeed;

    private long totalMoney = 0;
    private void Awake()
    {
        AudioManager.instance.PlayWhenWinGame();
        next.onClick.AddListener(() => GoToNextLevel());
        btnBomb.onClick.AddListener(() => Buy(SpecialSkill.Bomb));
        btnTimer.onClick.AddListener(() => Buy(SpecialSkill.Timer));
        btnSpeed.onClick.AddListener(() => Buy(SpecialSkill.Speed));
    }

    private void Start()
    {
        totalMoney = UserDataManager.Instance.UserData.TotalMoney;
        ShowTexts();
        RandomPrice();
    }

    private void ShowTexts()
    {
        txtMoney.text = totalMoney.ToString();
        txtCurrentBomb.text = UserDataManager.Instance.UserData.Bomb.ToString();
        txtCurrentTimer.text = UserDataManager.Instance.UserData.Timer.ToString();
        txtCurrentSpeed.text = UserDataManager.Instance.UserData.Speed.ToString();
        txtMessage.text = string.Empty;
    }

    public enum SpecialSkill
    {
        Bomb = 0,
        Timer = 1,
        Speed = 2,
    }

    private void Buy(SpecialSkill item)
    {
        long price = 0;
        bool buySuccess = false;
        switch (item)
        {
            case SpecialSkill.Bomb:
                long.TryParse(bombPrice.text, out price);
                if (totalMoney - price >= 0)
                {
                    buySuccess = true;
                    totalMoney -= price;
                    UserDataManager.Instance.UserData.TotalMoney = totalMoney;
                    UserDataManager.Instance.UserData.Bomb += 1;
                    txtCurrentBomb.text = UserDataManager.Instance.UserData.Bomb.ToString();
                }
                break;
            case SpecialSkill.Timer:
                long.TryParse(timerPrice.text, out price);
                if (totalMoney - price >= 0)
                {
                    buySuccess = true;
                    totalMoney -= price;
                    UserDataManager.Instance.UserData.TotalMoney = totalMoney;
                    UserDataManager.Instance.UserData.Timer += 1;
                    txtCurrentTimer.text = UserDataManager.Instance.UserData.Timer.ToString();
                }
                break;
            case SpecialSkill.Speed:
                long.TryParse(speedPrice.text, out price);
                if (totalMoney - price >= 0)
                {
                    buySuccess = true;
                    totalMoney -= price;
                    UserDataManager.Instance.UserData.TotalMoney = totalMoney;
                    UserDataManager.Instance.UserData.Speed += 1;
                    txtCurrentSpeed.text = UserDataManager.Instance.UserData.Speed.ToString();
                }
                break;
        }
        StopAllCoroutines();
        StartCoroutine(ShowMessage(buySuccess));
        txtMoney.text = totalMoney.ToString();
        UserDataManager.Instance.SaveUserData();
    }

    private IEnumerator ShowMessage(bool buySuccess)
    {
        if (buySuccess)
        {
            txtMessage.color = Color.green;
            txtMessage.text = "buy item successful!!!";
        }
        else
        {
            txtMessage.color = Color.red;
            txtMessage.text = "don't have enough money :(((";
        }
        yield return new WaitForSeconds(1f);
        txtMessage.text = string.Empty;
    }

    private void RandomPrice()
    {
        int currentLevel = UserDataManager.Instance.UserData.CurrentLevel;
        int minRange = currentLevel <= 10 ? 1000 : 2000;
        int maxRange = currentLevel <= 10 ? 2000 : 9999;

        bombPrice.text = Random.Range(minRange, maxRange).ToString();
        timerPrice.text = Random.Range(minRange, maxRange).ToString();
        speedPrice.text = Random.Range(minRange, maxRange).ToString();
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneName.World);
        AudioManager.instance.PausePlay(AudioName.BG, paused: false);
    }
}
