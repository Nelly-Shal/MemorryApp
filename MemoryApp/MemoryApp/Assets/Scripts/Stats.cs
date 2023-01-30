using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Stats 
{
    string path; 
    [SerializeField]
    public static List<LevelStats> list = new List<LevelStats>();
    LevelStats item;

    public void Stat(int countGuesses, float time, float imageFill)
    {
        path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            list = SaveSystem.Load();
            LevelStats e = list.Find((x) => x.currentLevel == LevelSelector.levelIndex);
            if (e != null)
            {
                if (e.bestResult != 0)
                {
                    if (e.bestResult > countGuesses)
                    {
                        e.bestResult = countGuesses;
                    }
                }
                else
                {
                    e.bestResult = countGuesses;
                }


                if (e.bestTime != 0)
                {
                    if (e.bestTime > time)
                    {
                        e.bestTime = time;
                    }
                }
                else
                {
                    e.bestTime = time;
                }
            }
            else
            {
                item = new LevelStats();
                item.bestResult = countGuesses;
                item.bestTime = time;
                item.currentLevel = LevelSelector.levelIndex;
                list.Add(item);
                SaveSystem.Save(list);
                e = list.Find((x) => x.currentLevel == LevelSelector.levelIndex);
                Debug.Log(e.bestResult);
            }
            e.starsLevel = imageFill;
            list.Remove(list.Find((x) => x.currentLevel == LevelSelector.levelIndex));
            list.Add(e);
            SaveSystem.Save(list);

        }
        else
        {
            item = new LevelStats();
            item.bestResult = countGuesses;
            item.bestTime= time;
            item.currentLevel = LevelSelector.levelIndex;
            item.starsLevel = imageFill;
            list.Add(item);
            SaveSystem.Save(list);
            LevelStats e = list.Find((x) => x.currentLevel == LevelSelector.levelIndex);
            Debug.Log(e.bestResult);

        }
        
    }

    public static void Create(int levelIndex)
    {

    }

    


}
