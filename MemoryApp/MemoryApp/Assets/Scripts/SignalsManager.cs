using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignalsManager : MonoBehaviour
{
    public GameObject failPanel;
    public GameObject finishPanel;
    public Signal[] signalsArray;
    public Signal[] signalsOrder;

    public int easyLevel = 4;
    public int mediumLevel = 6;
    public int hardLevel = 8;

    public int currentButton=0;
    public Signal[] currentSignals;

    public static int currentLevel;

    System.Random rndGenerator = new System.Random();

    public void CurrentLevel(int lvl)
    {
        currentLevel = lvl;
        signalsOrder = new Signal[currentLevel];
        currentSignals = new Signal[currentLevel];
        for (int i = 0; i <= currentLevel; i++)
        {
            int n = rndGenerator.Next(signalsArray.Length);
            signalsOrder[i] = signalsArray[n];
        }
    }

    public void GetLevel(Text txt)
    {
        currentLevel = int.Parse(txt.text);
        signalsOrder = new Signal[currentLevel];
        currentSignals = new Signal[currentLevel];
        for (int i = 0; i <= currentLevel; i++)
        {
            int n = rndGenerator.Next(signalsArray.Length);
            signalsOrder[i] = signalsArray[n];
        }
    }

    
    public void StartSignals()
    {
        bool ready = false;
        int i=0;
        StartCoroutine(PlayOrder());
        IEnumerator PlayOrder()
        {

            for (i = 0; i <= currentLevel; i++)
            {
                signalsOrder[i].HighlightColor();
                yield return new WaitForSeconds(1f);
                signalsOrder[i].NormalColor();

            }

        }

    }



    public void CheckOrder(Button btn)
    {
        
        if (btn == signalsOrder[currentButton].signal)
        {
            currentButton++;
            if (currentButton > currentLevel-1)
                finishPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Wrong");
            failPanel.SetActive(true);
        }
        

    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
