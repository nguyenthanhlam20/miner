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
        btnReturn.onClick.AddListener(() => SceneManager.LoadScene(SceneName.Profile));
    }

    private void PlayAgain() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
