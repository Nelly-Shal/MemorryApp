using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DailyCheck : MonoBehaviour
{
    [Space(5)]
    [SerializeField]
    private RewardPrefs rewardPref;
    [SerializeField]
    private Transform rewardGrid;

    private List<RewardPrefs> rewardPrefabs;

    private bool canClaimReward;
    private int maxStreakCount = 9;
    private float claimCoolDown = 24f;
    private float claimDeadLine = 48f;
    [SerializeField]
    public Text status;

    private void Start()
    {
        InitPrefabs();
        StartCoroutine(RewardsStateUpdater());

    }
    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

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
    private void InitPrefabs()
    {
        rewardPrefabs = new List<RewardPrefs>();

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
            status.text = "Claim your reward";
        else
        {
            var nextClaimTime = lastClaimTime.Value.AddHours(claimCoolDown);
            var currentClaimCoolDown = nextClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCoolDown.Hours:D2}:{currentClaimCoolDown.Minutes:D2}:{currentClaimCoolDown.Seconds:2D}";

            status.text = $"Come back in {cd} for your next check";
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

}