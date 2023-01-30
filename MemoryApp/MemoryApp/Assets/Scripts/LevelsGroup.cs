using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsGroup : MonoBehaviour
{

    public int levelGroup = 1;
    public GameObject group1;
    public GameObject group2;
    public GameObject group3;

    public void StartButton()
    {
        if (levelGroup == 1)
        {
            group1.SetActive(true);
            group2.SetActive(true);
            group3.SetActive(true);

        }
        else if (levelGroup == 2)
        {
            group1.SetActive(true);
            group2.SetActive(true);
        }
        else
        {
            group1.SetActive(true);
            group2.SetActive(true);
            group3.SetActive(true);
        }
    }

}
