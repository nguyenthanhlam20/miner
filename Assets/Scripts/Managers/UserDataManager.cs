using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance;

    public UserData UserData { get; set; }

    private void Awake()
    {
        if (Instance == null)
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

    public void ResetData()
    {
        UserData.TotalMoney = 0;
        UserData.Bomb = 0;
        UserData.Timer = 0;
        UserData.Speed = 0;
        UserData.CurrentLevel = 1;
    }
}

public class UserData
{
    public int HighestLevel { set; get; } = 1;
    public int CurrentLevel { set; get; } = 1;
    public long TotalMoney { set; get; } = 1000;
    public int Bomb { set; get; } = 5;
    public int Timer { set; get; } = 5;
    public int Speed { set; get; } = 5;

}
