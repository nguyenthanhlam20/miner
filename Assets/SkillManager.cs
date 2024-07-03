using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static YouWinHandler;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtBomb;
    [SerializeField] private TextMeshProUGUI txtTimer;
    [SerializeField] private TextMeshProUGUI txtSpeed;
    [SerializeField] private Button btnBomb;
    [SerializeField] private Button btnTimer;
    [SerializeField] private Button btnSpeed;
    [SerializeField] private Image countDownBomb;
    [SerializeField] private Image countDownTimer;
    [SerializeField] private Image countDownSpeed;

    private int currentBomb = 0;
    private int currentTimer = 0;
    private int currentSpeed = 0;

    private bool allowBomb = true;
    private bool allowTimer = true;
    private bool allowSpeed = true;
    private void Awake()
    {
        btnBomb.onClick.AddListener(() => UseBomb());
        btnTimer.onClick.AddListener(() => UseTimer());
        btnSpeed.onClick.AddListener(() => UseSpeed());
        DeactiveCountDown();
    }

    private void DeactiveCountDown()
    {
        countDownBomb.gameObject.SetActive(false);
        countDownTimer.gameObject.SetActive(false);
        countDownSpeed.gameObject.SetActive(false);
    }

    private void Start()
    {
        currentBomb = UserDataManager.Instance.UserData.Bomb;
        currentTimer = UserDataManager.Instance.UserData.Timer;
        currentSpeed = UserDataManager.Instance.UserData.Speed;
        txtBomb.text = currentBomb.ToString();
        txtTimer.text = currentTimer.ToString();
        txtSpeed.text = currentSpeed.ToString();
    }

    private IEnumerator ShowCountDown(Image target, SpecialSkill skill)
    {
        target.gameObject.SetActive(true);
        target.fillAmount = 1f;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            target.fillAmount -= 0.1f;
            yield return new WaitForSeconds(1f);
        }
        target.gameObject.SetActive(false);

        switch (skill)
        {
            case SpecialSkill.Bomb:
                allowBomb = true;
                break;
            case SpecialSkill.Timer:
                allowTimer = true;
                break;
            case SpecialSkill.Speed:
                Hook.Instance.boostSpeed = 0f;
                allowSpeed = true;
                break;
        }
    }
    private void UseBomb()
    {
        if (currentBomb > 0 && allowBomb)
        {
            AudioManager.instance.Play(AudioName.Explosion);

            currentBomb--;
            txtBomb.text = currentBomb.ToString();
            allowBomb = false;
            Hook.Instance.ExplodeItem();
            StartCoroutine(ShowCountDown(countDownBomb, SpecialSkill.Bomb));
            UserDataManager.Instance.UserData.Bomb = currentBomb;
            UserDataManager.Instance.SaveUserData();
        }
    }

    private void UseTimer()
    {
        if (currentTimer > 0 && allowTimer)
        {
            AudioManager.instance.Play(AudioName.Drinking);

            currentTimer--;
            txtTimer.text = currentTimer.ToString();
            allowTimer = false;
            StartCoroutine(ShowCountDown(countDownTimer, SpecialSkill.Timer));
            GameManager.instance.CountDownTimer += 10;
            UserDataManager.Instance.UserData.Timer = currentTimer;
            UserDataManager.Instance.SaveUserData();
        }
    }
    private void UseSpeed()
    {
        if (currentSpeed > 0 && allowSpeed)
        {
            AudioManager.instance.Play(AudioName.Drinking);

            currentSpeed--;
            txtSpeed.text = currentSpeed.ToString();
            allowSpeed = false;
            StartCoroutine(ShowCountDown(countDownSpeed, SpecialSkill.Speed));
            Hook.Instance.boostSpeed = 1f;
            UserDataManager.Instance.UserData.Speed = currentSpeed;
            UserDataManager.Instance.SaveUserData();
        }
    }
}
