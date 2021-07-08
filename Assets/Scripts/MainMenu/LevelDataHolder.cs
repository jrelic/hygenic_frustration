using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int Id;
    public int StarCount;
    public bool Locked;
    public string LevelName;
}

public class LevelDataHolder : MonoBehaviour
{
    private static LevelDataHolder _inst;

    public static LevelDataHolder Inst
    {
        get
        {
            if(_inst == null)
            {
                _inst = new GameObject("LevelDataHolder").AddComponent<LevelDataHolder>();
                DontDestroyOnLoad(_inst);
            }

            return _inst;
        }
    }

    public bool GameLoaded;

    public bool TutorialCompleted;

    private List<LevelData> LevelData;

    private LevelData currentLevelData;

    private void Awake()
    {
        LevelData = new List<LevelData>();

        LevelData.Add(new LevelData()
        {
            Id = 1,
            Locked = false,
            StarCount = 0,
            LevelName = "Level1"
        });

        LevelData.Add(new LevelData()
        {
            Id = 2,
            Locked = true,
            StarCount = 0,
            LevelName = "Level2"
        });

        LevelData.Add(new LevelData()
        {
            Id = 3,
            Locked = true,
            StarCount = 0,
            LevelName = "Level3"
        });

        LevelData.Add(new LevelData()
        {
            Id = 4,
            Locked = true,
            StarCount = 0,
            LevelName = "Level4"
        });
    }

    public LevelData GetLevelDataForId(int id)
    {
        foreach(var item in LevelData)
        {
            if(item.Id == id)
            {
                return item;
            }
        }

        return null;
    }

    public void UpdateCurrentLevelData(int stars)
    {
        if(currentLevelData != null)
        {
            currentLevelData.StarCount = stars;
        }
    }

    public void SetCurrentLevelData(LevelData data)
    {
        currentLevelData = data;
    }

    public void UnlockLevel(int levelId)
    {
        foreach(var item in LevelData)
        {
            if(item.Id == levelId)
            {
                item.Locked = false;
            }
        }
    }
}
