using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResultService
{
    private List<PlayerResult> playerResults = new List<PlayerResult>();

    public void SaveResult(int levelNumber, int movesCount)
    {
        PlayerResult result = new PlayerResult(levelNumber, movesCount);
        playerResults.Add(result);
        SaveToFile();
    }

    public List<PlayerResult> LoadResults()
    {
        LoadFromFile();
        return playerResults;
    }

    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(new SerializationWrapper<PlayerResult>(playerResults));
        string path = Path.Combine(Application.persistentDataPath, "playerResults.json");
        File.WriteAllText(path, json);
    }

    private void LoadFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerResults.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializationWrapper<PlayerResult> wrapper = JsonUtility.FromJson<SerializationWrapper<PlayerResult>>(json);
            playerResults = wrapper.list;
        }
    }
}

[Serializable]
public class SerializationWrapper<T>
{
    public List<T> list;

    public SerializationWrapper(List<T> list)
    {
        this.list = list;
    }
}

