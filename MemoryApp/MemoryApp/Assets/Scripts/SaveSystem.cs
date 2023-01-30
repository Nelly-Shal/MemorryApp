using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(List<LevelStats> list)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        //List<Stats> data = list;

        formatter.Serialize(stream, list);
        stream.Close();

    }

    public static List<LevelStats> Load()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<LevelStats> data = formatter.Deserialize(stream) as List<LevelStats>;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File not found in" + path);
            return null;
        }
    }
}
