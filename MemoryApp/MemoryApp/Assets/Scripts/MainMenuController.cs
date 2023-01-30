using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject quickPanel;
    public GameObject longPanel;

   
    public void LoadScene(string nameScene) // *
    {
        SceneManager.LoadScene(nameScene);
        
    }
    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Delete() // *
    {
        PlayerPrefs.DeleteKey("currentLevelCards");
        PlayerPrefs.DeleteKey("bestResultCards");
    }
    public void QuickPanel()
    {
        quickPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void LongPanel()
    {
        longPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void backPanel()
    {
        longPanel.SetActive(false);
        quickPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void DeleteWords() // *
    {
       
        for (int i = 0; i < LongMemoryController.numberOfWords+1; i++)
            PlayerPrefs.DeleteKey($"Word{i}");
        PlayerPrefs.SetInt("firstDay", 1);
    }
}

