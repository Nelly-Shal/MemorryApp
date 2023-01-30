using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LongMemoryController : MonoBehaviour
{
    public static bool firstDay=true;
    public static int numberOfWords=7;
    public GameObject wordsText;
    public GameObject inputText;
    public GameObject inputPanel;
    public GameObject finishPanel;
    public GameObject readyButton;
    // public string[] lines;
    public string[] wordsArray;
    System.Random rndGenerator = new System.Random();
    public Text txt;
    public Text inputTxt;
    public int count = 0;

    public Text hintText;

    [Space(5)]
    [SerializeField]
    private RewardPrefs rewardPref;
    [SerializeField]
    private Transform rewardGrid;

    private List<RewardPrefs> rewardPrefabs;

    private bool canClaimReward;
    public static int maxStreakCount = 9;
    private float claimCoolDown = 24f;
    private float claimDeadLine = 48f;
    [SerializeField]
    public Text status;

    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);
            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            else PlayerPrefs.DeleteKey("lastClaimedTime");
        }

    }
    void Start()
    {
        InitPrefabs();
        StartCoroutine(RewardsStateUpdater());
        wordsArray = new string[numberOfWords];
        if (PlayerPrefs.GetInt("firstDay")==0)   
        //if (PlayerPrefs.HasKey("Word0"))
            {
                for (int i = 0; i < numberOfWords + 1; i++)
                {
                    wordsArray[i] = PlayerPrefs.GetString($"Word{i}");
                   // txt.text = txt.text + wordsArray[i] + "\n";
                    hintText.text = hintText.text + wordsArray[i] + "\n";
                    Debug.Log(PlayerPrefs.GetString($"Word{i}"));
                inputPanel.SetActive(true);
                readyButton.SetActive(false);
                }
            }
            else
            {               

                var textFile = Resources.Load<TextAsset>("Text/Words").text;
                

                string[] lines = textFile.Split('\n');
                for (int i = 0; i < numberOfWords + 1; i++)
                {
                    int n = rndGenerator.Next(lines.Length);
                    wordsArray[i] = lines[n];
                    Debug.Log(wordsArray[i]);
                    txt.text = txt.text + wordsArray[i] + "\n";
                    hintText.text = hintText.text + wordsArray[i] + "\n";
                    PlayerPrefs.SetString($"Word{i}", wordsArray[i]);
                    Debug.Log(PlayerPrefs.GetString($"Word{i}"));
                readyButton.SetActive(true);
            }


            }
        

        



    }
    public void Again()
    {
        PlayerPrefs.SetInt("firstDay", 1);
    }

    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

    
    private void InitPrefabs()
    {
        rewardPrefabs = new List<RewardPrefs>();
        Debug.Log("It works");

        for (int i = 0; i < maxStreakCount; i++)
            rewardPrefabs.Add(Instantiate(rewardPref, rewardGrid, false));

    }

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateRewardsState()
    {
        canClaimReward = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

            if (timeSpan.TotalHours > claimDeadLine)
            {
                lastClaimTime = null;
                currentStreak = 0;

            }
            else if (timeSpan.TotalHours < claimCoolDown)
                canClaimReward = false;
        }
        UpdateRewardsUI();
    }
    private void UpdateRewardsUI()
    {
        if (canClaimReward)
            status.text = "Начинай проверку!";
        else
        {
            var NextClaimTime = lastClaimTime.Value.AddHours(claimCoolDown);
            var currentClaimCoolDown = NextClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCoolDown.Hours:D2}:{currentClaimCoolDown.Minutes:D2}:{currentClaimCoolDown.Seconds:D2}";

            status.text = $"Следующая проверка через {cd}";
        }

        for (int i = 0; i < rewardPrefabs.Count; i++)
            rewardPrefabs[i].SetRewardData(i, currentStreak);

    }

    public void ClaimReward()
    {
        if (!canClaimReward)
            return;
        lastClaimTime = DateTime.UtcNow;
        currentStreak = (currentStreak + 1) % maxStreakCount;
        UpdateRewardsState();
    }
    
    void Update()
    {
        
    }

    //public void NewColection()
    //{
    //    try
    //    {
    //        lines = File.ReadAllLines("C:/MemoryApp/Words.txt");
            
    //        for (int i = 0; i < wordsArray.Length; i++)
    //        {
    //            int n = rndGenerator.Next(lines.Length);
    //            wordsArray[i] = lines[n];
    //            Debug.Log(wordsArray[i]);
    //            txt.text = txt.text + wordsArray[i] + "\r\n";
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.Log("Ошибка при чтении файла");
    //    }
    //}
     public void ClickReady()
    {
        txt.text=null;
        inputPanel.SetActive(true);
        //b.gameObject.SetActive(false);
    }

    public void deleteKey()
    {
        PlayerPrefs.DeleteKey("lastClaimedTime");
        PlayerPrefs.DeleteKey("currentStreak");
    }
    public void ClickEnter()
    {
        string s = inputTxt.text ;
        string d = null;
        Debug.Log(count);
        for (int i = 0; i < wordsArray[count].Length - 1; i++)
            d = d + wordsArray[count][i];
        
        if (s == d)
        {
            txt.text = txt.text + inputTxt.text + "\n";
            //inputTxt.text.Remove(9);
            count++;
        }
        else
        {
            Debug.Log("Not Right");
        }
        if (count == numberOfWords)
        {
            PlayerPrefs.SetInt("firstDay",0);
            Debug.Log("Finish");
            finishPanel.SetActive(true);
            inputPanel.SetActive(false);
        }
    }


   
}
