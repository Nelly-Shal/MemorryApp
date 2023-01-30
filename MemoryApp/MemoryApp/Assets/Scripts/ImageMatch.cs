using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageMatch : MonoBehaviour
{
    public GameObject FailPanel;
    public GameObject FinishPanel;

    public List<Sprite> images;
    public List<Color> colors;
    public Image imagePref;
    public Sprite lastImageS;
    public Color lastImageC;


    System.Random rndGenerator = new System.Random();

    public int currentCount=-1;
    public static int maxCount=30;

    public bool gameStart = false;


    

    public void StartButton()
    {
        gameStart = true;
        ChangeImage();
        
    }
    public void StartButtonSecond()
    {
        lastImageS = imagePref.sprite;
        lastImageC = imagePref.color;
        ChangeImageSecond();

    }

        public void ChangeImage()
    {
        if (gameStart) { 

            lastImageS = imagePref.sprite;
            lastImageC = imagePref.color;
        }
        int n = rndGenerator.Next(images.Count);
        imagePref.sprite= images[n];
        int c = rndGenerator.Next(colors.Count);
        imagePref.color= colors[c];
        currentCount++;
    }
    public void ChangeImageSecond()
    {
        
        int n = rndGenerator.Next(images.Count);
        imagePref.sprite = images[n];
        int c = rndGenerator.Next(colors.Count);
        imagePref.color = colors[c];
        currentCount++;
    }

    public void CheckAnswerRight()
    {
        if (lastImageS == imagePref.sprite && lastImageC == imagePref.color)
        {
            if (currentCount != maxCount)
                ChangeImage();
            else
                FinishPanel.SetActive(true);
        }
        else
        {
            FailPanel.SetActive(true);
        }
    }

    public void CheckAnswerWrong()
    {
        if (lastImageS != imagePref.sprite || lastImageC != imagePref.color)
        {
            if (currentCount != maxCount)
                ChangeImage();
            else
                FinishPanel.SetActive(true);
        }
        else
        {
            FailPanel.SetActive(true);
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
    public void Quit()
    {
        Application.Quit();
    }
}
