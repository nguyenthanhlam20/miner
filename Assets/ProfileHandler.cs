using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileHandler : MonoBehaviour
{
    [SerializeField]
    private Button quitBtn;

    private void Awake()
    {
        quitBtn.onClick.AddListener(() => SceneManager.LoadScene(SceneName.MainMenu));
    }
}
