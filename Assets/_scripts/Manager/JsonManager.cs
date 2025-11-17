using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public static class JsonManager
{
    static string path = Application.persistentDataPath + "/";

    public static bool HaveJson(string FileName)
    {
        return File.Exists(path + FileName + ".json");
    }

    public static T Load<T>(string FileName)
    {
        string json = File.ReadAllText(path + FileName + ".json");
        return JsonUtility.FromJson<T>(json);
    }


    public static void Save<T>(string FileName, T data)
    {        
        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path +  FileName + ".json", json);
        Debug.Log("DataSave");
    }
}
