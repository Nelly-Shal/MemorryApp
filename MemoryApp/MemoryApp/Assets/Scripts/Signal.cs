using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signal : MonoBehaviour
{
    public Color defaultColor;
    public Color hilghlightColor;

    public Button signal;

   public void HighlightColor()
    {
        signal.GetComponent<Image>().color = hilghlightColor;
    }
    public void NormalColor()
    {
        signal.GetComponent<Image>().color = defaultColor;
    }
}
