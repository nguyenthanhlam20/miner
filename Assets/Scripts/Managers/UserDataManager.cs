using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance;

    public UserData UserData {  get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            UserData = FileManager.LoadUserData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveUserData() => FileManager.SaveUserData(UserData);
}

public class UserData
{
    public int HighestLevel { set; get; } = 1;
    public int CurrentLevel { set; get; } = 1;
}
