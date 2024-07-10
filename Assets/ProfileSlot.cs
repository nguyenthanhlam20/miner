using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileSlot : MonoBehaviour
{
    [SerializeField] private int profileIndex;
    [SerializeField] private TextMeshProUGUI txtProfileName;
    [SerializeField] private Button btnAdd;
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnRemove;
    [SerializeField] private TextMeshProUGUI totalMoney;
    [SerializeField] private TextMeshProUGUI highestLevel;

    private string profileName = string.Empty;

    private void Awake()
    {
        btnAdd.onClick.AddListener(() => SceneManager.LoadScene(SceneName.AddProfile));
        btnPlay.onClick.AddListener(() => SelectProfile());
        btnRemove.onClick.AddListener(() => RemoveProfile());
        LoadProfileData();
    }

    private void SelectProfile()
    {
        UserDataManager.Instance.ProfileName = profileName;
        UserDataManager.Instance.LoadSpecificProfile();
        SceneManager.LoadScene(SceneName.World);
    }

    private void RemoveProfile()
    {
        UserDataManager.Instance.RemoveProfile(profileIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void LoadProfileData()
    {
        var multiplayers = UserDataManager.Instance.Multiplayers;
        if (multiplayers != null && profileIndex <= multiplayers.Count - 1)
        {
            var profileData = multiplayers[profileIndex];
            profileName = profileData.ProfileName;

            txtProfileName.text = profileName;
            totalMoney.text = profileData.TotalMoney.ToString();
            highestLevel.text = profileData.CurrentLevel.ToString();

            btnAdd.gameObject.SetActive(false);
            btnRemove.gameObject.SetActive(true);
            btnPlay.gameObject.SetActive(true);
        }
        else
        {
            txtProfileName.text = "empty";
            totalMoney.text = "0";
            highestLevel.text = "0";

            btnAdd.gameObject.SetActive(true);
            btnPlay.gameObject.SetActive(false);
            btnRemove.gameObject.SetActive(false);

        }
    }
}
