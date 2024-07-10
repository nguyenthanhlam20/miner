using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    private static string UserDataPath = Application.persistentDataPath + "/user_data.json";

    public static void SaveUserData(List<UserData> myData)
    {
        try
        {
            // Serialize the UserData object to JSON
            var jsonData = JsonConvert.SerializeObject(myData, Formatting.Indented);

            // Write the JSON data to the file
            File.WriteAllText(UserDataPath, jsonData);
        }
        catch (Exception e)
        {
            // An exception occurred during the saving process, log the error
            Debug.LogError("Error saving user data: " + e.Message);
        }
    }
    public static List<UserData> LoadUserData()
    {
        if (File.Exists(UserDataPath))
        {
            try
            {
                // Read the JSON data from the file
                var jsonData = File.ReadAllText(UserDataPath);

                // Deserialize the JSON data to a UserData object
                var userData = JsonConvert.DeserializeObject<List<UserData>>(jsonData);

                return userData;
            }
            catch (Exception e)
            {
                // An exception occurred during the loading process, log the error
                Debug.LogError("Error loading user data: " + e.Message);
            }
        }
        return new List<UserData>();
    }

}

