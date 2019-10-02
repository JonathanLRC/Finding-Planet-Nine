using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOvertext;

    private int score = 0;
    public bool gameOver = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {

    }

    public void UpdateScore(int new_score)
    {
        if(new_score > score)
        {
            score = new_score;
        }
    }

    public void PlayerDied()
    {
        Debug.Log("PlayerDied");
        //gameOvertext.SetActive(true);
        SceneManager.LoadScene("LevelSelectionScreen");
        gameOver = true;
    }
}