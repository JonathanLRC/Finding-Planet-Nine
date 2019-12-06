using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    int levelNumber;
    float currentTime;
    float maxTime;

    // ///////////////
    public RectTransform mPanelGameOver;
    public Text mTxtGameOver;
    public RectTransform LeftButton;
    public RectTransform RightButton;
    public RectTransform NextLevelButton;
    public RectTransform clock;
    public Text timeText;

    int intTime;
    bool levelFinished = false;
    float copyTime;
    int i = 0;
    void Start()
    {
        mPanelGameOver.gameObject.SetActive(false);
        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);
        NextLevelButton.gameObject.SetActive(false);
        levelNumber = DataBase.ins.XmlDataBase.gameDB.status;
        foreach(var lvl in DataBase.ins.XmlDataBase.levelDB.list)
        {
            if(lvl.number == levelNumber)
            {
                maxTime = lvl.duration;
                break;
            }
        }
        currentTime = 0.0f;
        //setProgress(0.0f);

        //
        GameControl.instance.GameOverEvent += GameOverEvent;
        timeText.transform.position =  new Vector3(UnityEngine.Screen.width - 55, UnityEngine.Screen.height - 40, 1);
        clock.position = new Vector3(UnityEngine.Screen.width - 105, UnityEngine.Screen.height - 40, 1);
    }


    private void GameOverEvent(object sender, System.EventArgs e)
    {
        if (levelFinished)
        {
            return;
        }
        mPanelGameOver.gameObject.SetActive(true);
        mTxtGameOver.text = GameControl.instance.IsWon ? "NIVEL PASADO" : "GAME OVER";
        LeftButton.gameObject.SetActive(true);
        RightButton.gameObject.SetActive(true);
        NextLevelButton.gameObject.SetActive(false);
        levelFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelFinished)
        {
            return;
        }

        if (Player.isInTutorial)
        {
            timeText.text = "--:--";
            return;
        }

        if(levelNumber == 9 && AlienBoss.resistance <= 0)
        {
            GameCompleted();
            return;
        }

        maxTime -= Time.deltaTime;
        copyTime = maxTime;
        //timeText.text = " " + maxTime.ToString("f0");

        if(copyTime >= 60)
        {
            while (copyTime >=60)
            {
                copyTime = copyTime -60;
                i++;
            }
            if(copyTime <= 9.5)
            {
                timeText.text = "0" + i + ":0" + copyTime.ToString("f0");
            }
            else
            {
                timeText.text = "0" + i + ":" + copyTime.ToString("f0");
            }
            i = 0;
        }
        else
        {
            if(maxTime <= 9.5)
            {
                timeText.text = "00:0" + maxTime.ToString("f0");
            }
            else
            {
                timeText.text = "00:" + maxTime.ToString("f0");
            }
        }

        if (maxTime <= 0)
        {
            timeText.text = "00:00";
            if(levelNumber == 9)
            {
                if(AlienBoss.resistance <= 0)
                {
                    GameCompleted();
                }
                else
                {
                    GameObject player = GameObject.Find("Player");
                    Player other = (Player)player.GetComponent(typeof(Player));
                    other.Die();
                }
            }
            else
            {
                if (!levelFinished)
                {
                    Debug.Log("Nivel " + levelNumber + " terminado");
                    GameControl.instance.Save(levelNumber);
                    GameObject player = GameObject.Find("Player");
                    Player other = (Player)player.GetComponent(typeof(Player));
                    other.DieFinish();
                    mPanelGameOver.gameObject.SetActive(true);
                    mTxtGameOver.text = "NIVEL PASADO";
                    LeftButton.gameObject.SetActive(true);
                    RightButton.gameObject.SetActive(false);
                    NextLevelButton.gameObject.SetActive(true);
                    levelFinished = true;
                }
            }
        }
        //setProgress(currentTime/maxTime);
    }

    void GameCompleted()
    {
        levelFinished = true;
        Debug.Log("Nivel " + levelNumber + " terminado");
        GameControl.instance.Save(levelNumber);
        GameObject player = GameObject.Find("Player");
        Player other = (Player)player.GetComponent(typeof(Player));
        other.DieFinish();
        mPanelGameOver.gameObject.SetActive(true);
        mTxtGameOver.text = "NIVEL PASADO";
        LeftButton.gameObject.SetActive(true);
        RightButton.gameObject.SetActive(false);
        NextLevelButton.Find("Text").gameObject.GetComponent<Text>().text = "Créditos";
        NextLevelButton.gameObject.SetActive(true);
        LeftButton.gameObject.SetActive(true);
    }

    /*
    void setProgress(float p)
    {
        slider.value = p;
    }*/
}
