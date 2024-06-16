using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    [SerializeField] private GameOverPopup gameOverPopup;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameOverPopup.gameObject.SetActive(false);
    }

    public void ShowGameOverPopup() => gameOverPopup.gameObject.SetActive(true);

}
