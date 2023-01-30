using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddLevelButtons : MonoBehaviour
{
    public int numberOfCards;


    [SerializeField]
    private Transform levelField;

    [SerializeField]
    private GameObject btn;

    void Start()
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + (i + 1).ToString();
            button.transform.SetParent(levelField, false);
            Text txt = button.GetComponentInChildren<Text>();
            txt.text = (i+1).ToString();
        }
    }
}
