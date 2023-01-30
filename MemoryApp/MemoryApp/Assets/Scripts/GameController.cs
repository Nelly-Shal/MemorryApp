using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Image img;
    public List<LevelStats> data;
    //public LevelStats stats = new LevelStats();
    //public List<LevelStats> list = new List<LevelStats>();
    // public List<LevelStats> statsList = new List<LevelStats>();
    public float time;
    public LevelStats l;
    
    public int stars3;
    public int stars2;
    public int stars1;
    public GameObject ImageFill;
    public int nextSceneLoad;
    
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzles, secondGuessPuzzles;

    public GameObject finishPanel;
    [SerializeField]
    private Animation _animation;
    [SerializeField]
    private bool _isBackSide = true;

    void Awake()
    {
        //puzzles = Resources.LoadAll<Sprite>("Sprites/Cards_Icons");

    }
    void Start()
    {
        
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        puzzles = Resources.LoadAll<Sprite>("Sprites/Cards_Icons");
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    void Update()
    {
        if (finishPanel.activeSelf == false)
        {
            time += Time.deltaTime;
        }
    }
    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("CardButton");
        
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }
        
    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for (int i =0; i <looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);

            index++;

        }
    }
    void AddListeners()
    {
        foreach (Button btn  in btns)
        {
           btn.onClick.AddListener(() => PlayAnimF(btn));           
           btn.onClick.AddListener(() =>Invoke(nameof(PickAPuzzle),0.3f));
            
        }
    }

    public void PlayAnimF(Button btn)
    {
        _animation = btn.GetComponent<Animation>();
        _isBackSide = false;
        if (!_isBackSide)
        {
            _animation.Play("ToFrontCard");

        }

    }
    public void PlayAnimB(Button btn)
    {
        _animation = btn.GetComponent<Animation>();
        _isBackSide = true;
        if (_isBackSide)
        {
            _animation.Play("ToBackCard");

        }

    }

    public void PickAPuzzle()
        {
        
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzles= gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[firstGuessIndex].interactable = false;
        } else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzles = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;
            StartCoroutine(CheckIfThePuzzlesMatch());
            
        }

        IEnumerator CheckIfThePuzzlesMatch(){
            yield return new WaitForSeconds(0.1f);
            if (firstGuessPuzzles == secondGuessPuzzles)
            {
                yield return new WaitForSeconds(.1f);
                btns[firstGuessIndex].interactable = false;
                btns[secondGuessIndex].interactable = false;
                btns[firstGuessIndex].image.color = new Color(0,0,0,0);
                btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
                CheckIfTheGameIsFinished();
            }
            else
            {
                yield return new WaitForSeconds(.3f);
                PlayAnimB(btns[firstGuessIndex]);
                PlayAnimB(btns[secondGuessIndex]);
                foreach (Button btn in btns)
                {
                    if (btn.transform.rotation.y != 0)
                        // btn.transform.rotation.y = 0;
                        Debug.Log("Проблемы с ебаной карточкой");
                    

                }
                btns[firstGuessIndex].interactable = true;
                btns[firstGuessIndex].image.sprite = bgImage;
                btns[secondGuessIndex].image.sprite = bgImage;
                foreach(Button btn  in btns)
                {
                    btn.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                //btns[firstGuessIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
                //btns[secondGuessIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            foreach (Button btn in btns)
            {
                btn.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //yield return new WaitForSeconds(.1f);
            firstGuess = secondGuess = false;
            foreach (Button btn in btns)
            {
                btn.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        void CheckIfTheGameIsFinished()
        {
            countCorrectGuesses++;

            if (countCorrectGuesses == gameGuesses)
            {
                
                finishPanel.SetActive(true);
                GameObject result=GameObject.FindGameObjectWithTag("Result");
                GameObject bestresult = GameObject.FindGameObjectWithTag("BestResult");
                GameObject tim = GameObject.FindGameObjectWithTag("Time");
                GameObject timm = GameObject.FindGameObjectWithTag("BestTime");

                Text t = result.GetComponent<Text>();
                Text tt = bestresult.GetComponent<Text>();
                Text ti = tim.GetComponent<Text>();
                Text tii = timm.GetComponent<Text>();

                t.text = "Результат: " + countGuesses;
                ti.text = "Время: " + System.Math.Round(time,2);
                
                
                if (countGuesses <= stars3) 
                {
                    img = ImageFill.GetComponent<Image>();
                    img.fillAmount = 1;
                    if (nextSceneLoad > PlayerPrefs.GetInt("currentLevelCards"))
                    {
                        PlayerPrefs.SetInt("currentLevelCards", nextSceneLoad);
                    }
                }else
                if (countGuesses <= stars2 && countGuesses>stars3)
                {
                    img = ImageFill.GetComponent<Image>();
                    img.fillAmount = 0.7f;
                    Debug.Log(img.fillAmount);
                    if (nextSceneLoad > PlayerPrefs.GetInt("currentLevelCards"))
                    {
                        PlayerPrefs.SetInt("currentLevelCards", nextSceneLoad);
                    }

                }
                else
                if (countGuesses <= stars1 && countGuesses > stars2)
                {
                    img = ImageFill.GetComponent<Image>();
                    img.fillAmount = 0.3f;
                    if (nextSceneLoad > PlayerPrefs.GetInt("currentLevelCards"))
                    {
                        PlayerPrefs.SetInt("currentLevelCards", nextSceneLoad);
                    }
                }
                else
                if (countGuesses > stars1)
                {
                    img = ImageFill.GetComponent<Image>();
                    img.fillAmount = 0;
                    GameObject nxt = GameObject.FindGameObjectWithTag("Next");
                    nxt.SetActive(false);
                }



                //////////////////////  ПРОВЕРКА ДАННЫХ И СОХРАНЕНИЕ В СПИСОК ПОД ИНДЕКСОМ //////////////////////////////////////////
                ///////Сохранение и Проверка (countGuesses, time);
                //Stats(countGuesses, time);

                Stats lst = new Stats();
                lst.Stat(countGuesses, time,img.fillAmount);
                Debug.Log(img.fillAmount);
                LevelStats e = Stats.list.Find((x) => x.currentLevel == LevelSelector.levelIndex);
                tt.text = "Лучший результат: " + e.bestResult;
                tii.text = "Лучшее время: " + System.Math.Round(e.bestTime,2);
                
            }
        }
    }
    void Shuffle(List<Sprite> list)
    {
        for (int i=0; i<list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            SceneManager.LoadScene("MainMenu");
            
        }
        else
        {
            //data = SaveSystem.Load();
            //Stats.list[LevelSelector.levelIndex + 1].bestResult = data[LevelSelector.levelIndex + 1].bestResult;
            //Stats.list[LevelSelector.levelIndex + 1].bestTime = data[LevelSelector.levelIndex + 1].bestTime;
            LevelSelector.levelIndex += 1;
            SceneManager.LoadScene(nextSceneLoad);
            if (nextSceneLoad > PlayerPrefs.GetInt("currentLevelCards"))
            {

                PlayerPrefs.SetInt("currentLevelCards", nextSceneLoad);
            }
        }
        
        
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void PauseIn()
    {
        Time.timeScale = 0;
    }
    public void PauseOut()
    {
        Time.timeScale = 1;
    }

    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
