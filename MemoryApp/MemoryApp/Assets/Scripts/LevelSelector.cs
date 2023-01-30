using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LevelSelector : MonoBehaviour
{
    public GameObject[] stars;
    public List<Button> lvlButtons = new List<Button>();

    [SerializeField]
    public static int levelIndex;

    public static List<LevelStats> data = new List<LevelStats>();
    public static string path;

    private void Start()
    {
        
        data = SaveSystem.Load();
        GetButtons();
        AddListeners();

        path = Application.persistentDataPath + "/player.fun";

        int currentLevelCards = PlayerPrefs.GetInt("currentLevelCards", 2);

        for (int i = 0; i < lvlButtons.Count; i++)
        {
            if (i + 2 > currentLevelCards)
                lvlButtons[i].interactable = false;
        }
        GetStars();

    }

    void GetStars()
    {
        stars = GameObject.FindGameObjectsWithTag("ImageFill");
        foreach (Button btn in lvlButtons)
        {
            if (btn.interactable == true)
            {
                LevelStats e = data.Find((x) => x.currentLevel == int.Parse(btn.name));
                Debug.Log(e.starsLevel);
                Image img = btn.transform.Find("ImageFill").GetComponent<Image>();
                img.fillAmount = e.starsLevel;
            }
        }
    }
    void GetButtons()
    {

        GameObject[] objects = GameObject.FindGameObjectsWithTag("LevelButton");
        if (objects.Length != 0) { }
        Debug.Log(objects.Length);
        for (int i = 0; i < objects.Length; i++)
        {

            lvlButtons.Add(objects[i].GetComponent<Button>());
        }
    
    }

    void AddListeners()
    {
        foreach (Button btn in lvlButtons)
        {
           btn.onClick.AddListener(() => Select(btn.name));

        }
    }
    public void Select(string name)
    {

        levelIndex = int.Parse(name);
        Debug.Log(levelIndex);
        SceneManager.LoadScene("Cards_Level_" + levelIndex);
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);

    }

    


}
