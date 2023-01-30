using UnityEngine;

public class AddCards : MonoBehaviour
{
    
    public int numberOfCards;
    

    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    void Start()
    {
        //Debug.Log(PlayerPrefs.GetInt("bestResultCards"));
        for (int i = 0; i < numberOfCards; i++)
            {
                GameObject button = Instantiate(btn);
                button.name = "" + i;
                button.transform.SetParent(puzzleField, false);
            
        }
    }

}
