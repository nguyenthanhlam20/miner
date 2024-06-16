using TMPro;
using UnityEngine;

public class LevelDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI highest;

    private void Awake()
    {
        if (current != null) current.text = "Level " + GameManager.instance.CurrentLevel;
        highest.text = "Highest Level: " + UserDataManager.Instance.UserData.HighestLevel;
    }
}
