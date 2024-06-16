using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldHandler : MonoBehaviour
{
    [SerializeField] private Button playAgain;

    private void Awake() => playAgain.onClick.AddListener(() => PlayAgain());

    private void PlayAgain() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
