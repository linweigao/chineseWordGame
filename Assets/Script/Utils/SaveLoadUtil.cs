using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadUtil
{
    public static void SaveData<T>(T data, string relativePath)
    {
        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath +  "/" + relativePath;
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
    }

    public static T LoadData<T>(string relativePath) where T: class
    {
        string path = Application.persistentDataPath + "/" + relativePath;
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                object data = formatter.Deserialize(stream);
                return (T)data;
            }
        }
        else
        {
            return null;
        }
    }

}
