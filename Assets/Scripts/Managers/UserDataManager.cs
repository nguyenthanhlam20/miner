using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance;

    public string ProfileName { get; set; }
    public List<UserData> Multiplayers { get; set; }
    public UserData UserData { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Multiplayers = FileManager.LoadUserData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSpecificProfile()
    {
        UserData = Multiplayers.FirstOrDefault(x => x.ProfileName == ProfileName);
    }

    public void RemoveProfile(int index)
    {
        if (index <= Multiplayers.Count - 1)
        {
            Multiplayers.RemoveAt(index);
            FileManager.SaveUserData(Multiplayers);
        }
    }

    public (bool, string) AddNewProfile(string fullname)
    {
        if (Multiplayers.Count == 3)
            return (false, "you have reach the profile limitation of 3.");

        var anyUser = Multiplayers.Any(x => x.ProfileName == fullname);
        if (anyUser) return (false, "profile name is already exist.");

        Multiplayers.Add(new UserData { ProfileName = fullname });
        FileManager.SaveUserData(Multiplayers);
        return (true, "Add new profile success.");
    }



    public void SaveUserData()
    {
        var index = Multiplayers.FindIndex(x => x.ProfileName == ProfileName);
        Multiplayers[index] = UserData;
        FileManager.SaveUserData(Multiplayers);
    }

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
    public string ProfileName { get; set; }
    public int HighestLevel { set; get; } = 1;
    public int CurrentLevel { set; get; } = 1;
    public long TotalMoney { set; get; } = 1000;
    public int Bomb { set; get; } = 5;
    public int Timer { set; get; } = 5;
    public int Speed { set; get; } = 5;

}
