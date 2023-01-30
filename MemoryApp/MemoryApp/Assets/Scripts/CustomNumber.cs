using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomNumber : MonoBehaviour
{

    public Text numberText;
    public int minM,maxM,minP,maxP;
    
    
    public void NumberPlus(int n)
    {
        int num = int.Parse(numberText.text);
        if (num > minP && num< maxP)
        {
            num += n;
            numberText.text = num.ToString();
        }
    }
    public void NumberMinus (int n)
    {
        int num = int.Parse(numberText.text);
        if (num > minM && num < maxM)
        {
            num -= n;
            numberText.text = num.ToString();
        }
    }

    public void LoadCusomLevel()
    {
        try
        {
            int a = int.Parse(numberText.text);
            switch (a)
            {
                case 6: 
                    SceneManager.LoadScene("Cards_Level_1");
                    break;
                case 8:
                    SceneManager.LoadScene("Cards_Level_2");
                    break;
                case 10:
                    SceneManager.LoadScene("Cards_Level_3");
                    break;
                case 12:
                    SceneManager.LoadScene("Cards_Level_4");
                    break;
                case 14:
                    SceneManager.LoadScene("Cards_Level_5");
                    break;
                case 16:
                    SceneManager.LoadScene("Cards_Level_6");
                    break;
                case 18:
                    SceneManager.LoadScene("Cards_Level_7");
                    break;
                case 20:
                    SceneManager.LoadScene("Cards_Level_8");
                    break;
                case 22:
                    SceneManager.LoadScene("Cards_Level_9");
                    break;
                case 24:
                    SceneManager.LoadScene("Cards_Level_10");
                    break;
                default: SceneManager.LoadScene("Cards_Level_1");
                    break;
            }
        }
        catch
        {
            
        }
    }

}
