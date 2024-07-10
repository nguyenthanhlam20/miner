using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateProfileHandler : MonoBehaviour
{
    [SerializeField] Button playBtn;
    [SerializeField] Button quitBtn;
    [SerializeField] TMP_InputField txtFullname;
    [SerializeField] TextMeshProUGUI txtError;

    private void Awake()
    {
        txtError.text = "";
        playBtn.onClick.AddListener(() => CreateProfile());
        quitBtn.onClick.AddListener(() => SceneManager.LoadScene(SceneName.Profile));
        txtFullname.onValueChanged.AddListener((value) => txtError.text = "");
    }

    private void CreateProfile()
    {
        var fullname = txtFullname.text.Trim();

        if (string.IsNullOrEmpty(fullname))
        {
            ShowError("please enter your profile name.");
            return;
        }

        if (fullname.Length >= 20)
        {
            ShowError("profile name can be maximum of 20 characters.");
            return;
        }

        var response = UserDataManager.Instance.AddNewProfile(fullname: fullname);
        if (!response.Item1)
        {
            ShowError(response.Item2);
            return;
        }

        txtError.color = Color.green;
        txtError.text = response.Item2;
        StartCoroutine(GoToProfile());
    }

    private void ShowError(string message)
    {
        txtError.color = Color.red;
        txtError.text = message;
    }

    private IEnumerator GoToProfile()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneName.Profile);
    }
}
