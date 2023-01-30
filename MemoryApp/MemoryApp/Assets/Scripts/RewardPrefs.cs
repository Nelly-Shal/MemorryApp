using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPrefs : MonoBehaviour
{
    [SerializeField]
    private Image backRound;
    [SerializeField]
    private Image backTick;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color doneColor;
    [SerializeField]
    private Text dayText;

    public void SetRewardData(int day, int currentStreak)
    {
        dayText.text = $"Δενό{day + 1}";
        //backRound.GetComponent<Image>().color = doneColor;
       // backRound.color
            backRound.GetComponent<Image>().color = day < currentStreak ? doneColor : defaultColor;
        //backTick.color
        backTick.GetComponent<Image>().color = day < currentStreak ? doneColor : defaultColor;

    }
}
