using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOvertext;
    
    public bool gameOver = false;

    private float delay = 1.1f;

    private AudioSource audio2;

    /// 


    public event EventHandler GameOverEvent;

    public void OnGameOver()
    {
        if (GameOverEvent != null)
            GameOverEvent(this, EventArgs.Empty);
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public bool IsWon
    {
        get
        {
            if (gameOver.Equals(true))
                return false;

            return true;
        }
    }

    public int status
    {
        get
        {
            return DataBase.ins.XmlDataBase.gameDB.status;
        }
        set
        {
            DataBase.ins.XmlDataBase.gameDB.status = value;
        }
    }

    public int score
    {
        get
        {
            return DataBase.ins.XmlDataBase.gameDB.score;
        }
        set
        {
            DataBase.ins.UpdateScore(value);
        }
    }

    public void Save(int new_score)
    {
        DataBase.ins.UpdateScore(new_score);
    }

    public void PlayerDied()
    {
        audio2 = GetComponent<AudioSource>();
        audio2.volume = PlayerPrefs.GetFloat("SfxVolume", 0.75f);
        Debug.Log("PlayerDied");
        //gameOvertext.SetActive(true);
        audio2.PlayOneShot(GetComponent<AudioSource>().clip);
        
        //StartCoroutine(DelayLoad());

        gameOver = true;
        OnGameOver();

    }

    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LevelSelectionScreen");
    }

    public void PlayerDiedFinish()
    {
        audio2 = GetComponent<AudioSource>();
        audio2.volume = PlayerPrefs.GetFloat("SfxVolume", 0.75f);
        Debug.Log("PlayerDied");
        //gameOvertext.SetActive(true);
        audio2.PlayOneShot(GetComponent<AudioSource>().clip);

        //StartCoroutine(DelayLoadFinish());

        gameOver = true;
        OnGameOver();

    }

    IEnumerator DelayLoadFinish()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LevelSelectionScreen");
    }
}