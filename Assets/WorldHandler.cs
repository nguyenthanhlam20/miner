using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldHandler : MonoBehaviour
{
    [SerializeField] private Button playAgain;
    [SerializeField] private Button btnReturn;

    private void Awake()
    {
        playAgain.onClick.AddListener(() => PlayAgain());
        btnReturn.onClick.AddListener(() => GoToPreviousScene());
    }

    private void GoToPreviousScene()
    {
        AudioManager.instance.StopPlay(AudioName.RopeStretch);
        SceneManager.LoadScene(SceneName.Profile);
    }

    private void PlayAgain()
    {
        AudioManager.instance.StopPlay(AudioName.RopeStretch);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
